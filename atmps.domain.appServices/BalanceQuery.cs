using System;
using atmps.domain.appServices.Exceptions;
using atmps.domain.infrastructure;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Balance query.
    /// </summary>
    public class BalanceQuery : IQuery<Result<decimal>>
    {
        public BalanceQuery(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public string CardNumber { get; }
    }

    /// <summary>
    /// Balance query handler.
    /// </summary>
    public sealed class BalanceQueryHandler : IQueryHandler<BalanceQuery, Result<decimal>>
    {
        public BalanceQueryHandler(AppDbContext appDbContext,
        IAuditLogProcessor auditLogProcessor,
        IBalanceCommunication balanceCommunication,
        ICardNumberValidationService cardNumberValidationService,
        ITransactionProcessor transactionProcessor)
        {
            AppDbContext = appDbContext;
            AuditLogProcessor = auditLogProcessor;
            BalanceCommunication = balanceCommunication;
            CardNumberValidationService = cardNumberValidationService;
            TransactionProcessor = transactionProcessor;
        }

        public AppDbContext AppDbContext { get; }
        public IAuditLogProcessor AuditLogProcessor { get; }
        public IBalanceCommunication BalanceCommunication { get; }
        public ICardNumberValidationService CardNumberValidationService { get; }
        public ITransactionProcessor TransactionProcessor { get; }

        /// <summary>
        /// Handle the specified query.
        /// </summary>
        /// <returns>The handle.</returns>
        /// <param name="query">Query.</param>
        public Result<decimal> Handle(BalanceQuery query)
        {
            var cardInfo = CardNumberValidationService.PopulateCardInfo(query.CardNumber);

            if (string.IsNullOrEmpty(cardInfo.AccountNumber))
            {
                AuditLogProcessor.Process(query.CardNumber, OperationType.Balance, false);

                return Result.Fail<decimal>("Account number is not valid!");
            }

            var balanceRequestResult = BalanceCommunication.GetBalance(cardInfo.AccountNumber, cardInfo.BankInfo.VposUrl);

            AuditLogProcessor.Process(query.CardNumber, OperationType.Balance, balanceRequestResult.IsSuccess);

            TransactionProcessor.Process(new Transaction
            {
                BankInfo = cardInfo.BankInfo,
                CardNumber = cardInfo.Number,
                CardType = cardInfo.CardType,
                BankInfoId = cardInfo.BankInfo.Id,
                DepositAmount = null,
                TransactionFee = null,
                WithdrawAmount = null,
                BalanceAmount = balanceRequestResult.Value,
                TransactionType = TransactionType.BALANCE,
                TransactionDate = DateTime.Now,
                OperationResult = balanceRequestResult.IsSuccess
            });

            if (balanceRequestResult.IsFailure)
                return Result.Fail<decimal>(balanceRequestResult.Error);

            return balanceRequestResult;
        }
    }
}