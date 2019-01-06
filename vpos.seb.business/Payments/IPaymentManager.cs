using System;
using CSharpFunctionalExtensions;

namespace vpos.seb.business.Payments
{
    /// <summary>
    /// Payment manager.
    /// </summary>
    public interface IPaymentManager
    {
        Result Withdraw(WithdrawOperation withDrawOperation);
        Result<decimal> Balance(BalanceOperation balanceOperation);
        Result Deposit(DepositOperation depositOperation);
    }
}
