using System.Linq;
using CSharpFunctionalExtensions;
using vpos.messages;
using vpos.seb.domain.infrastructure;

namespace vpos.seb.business.Payments
{
    ///https://support.payjunction.com/hc/en-us/articles/213388398-Full-List-of-Approval-Codes-and-Decline-Codes
    /// <summary>
    /// Payment manager on Vpos system. 
    /// All messages need to be collected as codes and it needs to be shown as message with some convertions/translations. Didn't implemented like that but I know!
    /// </summary>
    public class PaymentManager : IPaymentManager
    {
        public PaymentManager(
        AppDbContext appDbContext,
        BalanceOperationValidator balanceOperationValidator,
        DepositOperationValidator depositOperationValidator,
        WithdrawOperationValidator withdrawOperationValidator)
        {
            AppDbContext = appDbContext;
            BalanceOperationValidator = balanceOperationValidator;
            DepositOperationValidator = depositOperationValidator;
            WithdrawOperationValidator = withdrawOperationValidator;
        }

        public AppDbContext AppDbContext { get; }
        public BalanceOperationValidator BalanceOperationValidator { get; }
        public DepositOperationValidator DepositOperationValidator { get; }
        public WithdrawOperationValidator WithdrawOperationValidator { get; }

        /// <summary>
        /// Balance the specified balanceOperation.
        /// </summary>
        /// <returns>The balance.</returns>
        /// <param name="balanceOperation">Balance operation.</param>
        public Result<decimal> Balance(BalanceOperation balanceOperation)
        {
            BalanceOperationValidator.SetupRules();

            var validationResult = BalanceOperationValidator.Validate(balanceOperation);

            if (!validationResult.IsValid)
                return Result.Fail<decimal>(validationResult.Errors[0].ErrorMessage);

            var account = AppDbContext.BankAccounts.FirstOrDefault(x => x.Number == balanceOperation.AccountNumber);

            if (account == null)
                return Result.Fail<decimal>(ResponseCodes.A1001.ToString());

            return Result.Ok<decimal>(account.Balance);
        }

        /// <summary>
        /// Deposit the specified depositOperation.
        /// </summary>
        /// <returns>The deposit.</returns>
        /// <param name="depositOperation">Deposit operation.</param>
        public Result Deposit(DepositOperation depositOperation)
        {
            DepositOperationValidator.SetupRules();
            var validationResult = DepositOperationValidator.Validate(depositOperation);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.Errors[0].ErrorMessage);

            var account = AppDbContext.BankAccounts.FirstOrDefault(x => x.Number == depositOperation.AccountNumber);

            if (account == null)
                return Result.Fail(ResponseCodes.A1001.ToString());

            account.Balance += depositOperation.DepositAmount;
            AppDbContext.Update(account);
            AppDbContext.SaveChanges();

            return Result.Ok();
        }
        /// <summary>
        /// Withdraw the specified withDrawOperation.
        /// </summary>
        /// <returns>The withdraw.</returns>
        /// <param name="withDrawOperation">With draw operation.</param>
        public Result Withdraw(WithdrawOperation withDrawOperation)
        {
            WithdrawOperationValidator.SetupRules();
            var validationResult = WithdrawOperationValidator.Validate(withDrawOperation);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.Errors[0].ErrorMessage);

            var account = AppDbContext.BankAccounts.FirstOrDefault(x => x.Number == withDrawOperation.AccountNumber);

            if (account == null)
                return Result.Fail(ResponseCodes.A1001.ToString());
            
            if (account.Balance < withDrawOperation.WithdrawAmount)
                return Result.Fail(ResponseCodes.W1003.ToString());

            account.Balance -= withDrawOperation.WithdrawAmount;
            AppDbContext.Update(account);
            AppDbContext.SaveChanges();

            return Result.Ok();
        }
    }
}
