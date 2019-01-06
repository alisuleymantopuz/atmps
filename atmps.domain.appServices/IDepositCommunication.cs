using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Deposit communication.
    /// </summary>
    public interface IDepositCommunication
    {
        Result LoadMoney(string accountNumber, int amount, string bankVposUrl);
    }
}
