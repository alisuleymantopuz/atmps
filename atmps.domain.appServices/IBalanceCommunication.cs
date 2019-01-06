using System;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Balance communication.
    /// </summary>
    public interface IBalanceCommunication
    {
        Result<decimal> GetBalance(string accountNumber, string bankVposUrl);
    }
}
