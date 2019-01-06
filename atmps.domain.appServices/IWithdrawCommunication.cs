using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Withdraw communication.
    /// </summary>
    public interface IWithdrawCommunication
    {
        Result WithdrawMoney(string accountNumber, int amount, string bankVposUrl);
    }
}
