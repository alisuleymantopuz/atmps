using CSharpFunctionalExtensions;
using RestSharp;
using vpos.contract.Requests;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Deposit communication.
    /// </summary>
    public class DepositCommunication : IDepositCommunication
    {
        public Result LoadMoney(string accountNumber, int amount, string bankVposUrl)
        {
            // can be injected
            var client = new RestClient(bankVposUrl);

            var request = new RestRequest("Deposit", Method.POST, DataFormat.Json);
            request.AddJsonBody(new DepositRequest { AccountNumber = accountNumber, DepositAmount = amount });

            var response = client.Execute<decimal>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return Result.Fail(response.Content);

            //It could behave differently based on response codes which comes from bank.

            return Result.Ok();
        }
    }
}
