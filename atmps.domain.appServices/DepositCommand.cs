using System;
using atmps.domain.appServices.Exceptions;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Deposit command.
    /// </summary>
    public class DepositCommand : ICommand
    {
        public DepositCommand(string cardNumber, int amount)
        {
            CardNumber = cardNumber;
            Amount = amount;
        }

        public string CardNumber { get; }
        public int Amount { get; }
    }

    /// <summary>
    /// Deposit command handler.
    /// </summary>
    public class DepositCommandHandler : ICommandHandler<DepositCommand>
    {
        public DepositCommandHandler(IDepositCommunication depositCommunication,
        ICardNumberValidationService cardNumberValidationService,
        IAuditLogProcessor auditLogProcessor,
        ITransactionProcessor transactionProcessor)
        {
            DepositCommunication = depositCommunication;
            CardNumberValidationService = cardNumberValidationService;
            AuditLogProcessor = auditLogProcessor;
            TransactionProcessor = transactionProcessor;
        }

        public IDepositCommunication DepositCommunication { get; }
        public ICardNumberValidationService CardNumberValidationService { get; }
        public IAuditLogProcessor AuditLogProcessor { get; }
        public ITransactionProcessor TransactionProcessor { get; }

        /// <summary>
        /// Handle the specified command.
        /// </summary>
        /// <returns>The handle of deposit operation.</returns>
        /// <param name="command">Command.</param>
        public Result Handle(DepositCommand command)
        {
            var cardInfo = CardNumberValidationService.PopulateCardInfo(command.CardNumber);

            if (!cardInfo.BankInfo.IsOperated)
            {
                AuditLogProcessor.Process(command.CardNumber, OperationType.Deposit, false);

                return Result.Fail("The bank is not operated in this ATM");
            }

            var depositRequestResult = DepositCommunication.LoadMoney(cardInfo.AccountNumber, command.Amount, cardInfo.BankInfo.VposUrl);

            AuditLogProcessor.Process(command.CardNumber, OperationType.Deposit, depositRequestResult.IsSuccess);

            TransactionProcessor.Process(new Transaction
            {
                BankInfo = cardInfo.BankInfo,
                CardNumber = cardInfo.Number,
                CardType = cardInfo.CardType,
                BankInfoId = cardInfo.BankInfo.Id,
                DepositAmount = command.Amount,
                TransactionFee = null,
                WithdrawAmount = null,
                BalanceAmount = null,
                TransactionType = TransactionType.DEPOSIT,
                TransactionDate = DateTime.Now,
                OperationResult = depositRequestResult.IsSuccess
            });

            if (depositRequestResult.IsFailure)
                return Result.Fail("Deposit operation isn't completed successfully.");

            return Result.Ok();
        }
    }
}
