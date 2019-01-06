using System;
using atmps.domain.appServices.Exceptions;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    public class WithdrawQuery : IQuery<Result<Money>>
    {
        public WithdrawQuery(string cardNumber, int amount)
        {
            CardNumber = cardNumber;
            Amount = amount;
        }

        public string CardNumber { get; }
        public int Amount { get; }
    }

    public class WithdrawQueryHandler : IQueryHandler<WithdrawQuery, Result<Money>>
    {
        public WithdrawQueryHandler(IWithdrawCommunication withdrawCommunication,
        ICardNumberValidationService cardNumberValidationService,
        IAuditLogProcessor auditLogProcessor,
        ITransactionProcessor transactionProcessor,
        IMoneyGenerator moneyGenerator)
        {
            WithdrawCommunication = withdrawCommunication;
            CardNumberValidationService = cardNumberValidationService;
            AuditLogProcessor = auditLogProcessor;
            TransactionProcessor = transactionProcessor;
            MoneyGenerator = moneyGenerator;
        }

        public IWithdrawCommunication WithdrawCommunication { get; }
        public ICardNumberValidationService CardNumberValidationService { get; }
        public IAuditLogProcessor AuditLogProcessor { get; }
        public ITransactionProcessor TransactionProcessor { get; }
        public IMoneyGenerator MoneyGenerator { get; }

        public Result<Money> Handle(WithdrawQuery query)
        {
            var cardInfo = CardNumberValidationService.PopulateCardInfo(query.CardNumber);

            if (query.Amount <= 0)
                return Result.Fail<Money>("Withdraw amount should be greater than 0");

            if (query.Amount % 5 != 0)
                return Result.Fail<Money>("Machine allows to withdraw with 5E, 10E, 20E and 50E");

            var moneySet = MoneyGenerator.GenerateMoney(query.Amount);

            int transactionFee = 1;

            int totalAmount = query.Amount + transactionFee;

            var withdrawRequestResult = WithdrawCommunication.WithdrawMoney(cardInfo.AccountNumber, totalAmount, cardInfo.BankInfo.VposUrl);

            AuditLogProcessor.Process(query.CardNumber, OperationType.Withdraw, withdrawRequestResult.IsSuccess);

            TransactionProcessor.Process(new Transaction
            {
                BankInfo = cardInfo.BankInfo,
                CardNumber = cardInfo.Number,
                CardType = cardInfo.CardType,
                BankInfoId = cardInfo.BankInfo.Id,
                DepositAmount = null,
                TransactionFee = transactionFee,
                WithdrawAmount = totalAmount,
                BalanceAmount = null,
                TransactionType = TransactionType.WITHDRAW,
                TransactionDate = DateTime.Now,
                OperationResult = withdrawRequestResult.IsSuccess
            });

            if (withdrawRequestResult.IsFailure)
                return Result.Fail<Money>(withdrawRequestResult.Error);

            return Result.Ok<Money>(moneySet);
        }
    }
}
