using CSharpFunctionalExtensions;
using RestSharp;
using vpos.contract.Requests;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Balance communication.
    /// </summary>
    public class BalanceCommunication : IBalanceCommunication
    {
        Result<decimal> IBalanceCommunication.GetBalance(string accountNumber, string bankVposUrl)
        {
            // can be injected
            var client = new RestClient(bankVposUrl);

            var request = new RestRequest("Balance", Method.POST, DataFormat.Json);

            request.AddJsonBody(new BalanceRequest { AccountNumber = accountNumber });

            var response = client.Execute<decimal>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return Result.Fail<decimal>(response.Content);

            //It could behave differently based on response codes which comes from bank.

            return Result.Ok<decimal>(response.Data);
        }
    }
}
