using Microsoft.AspNetCore.Mvc;
using vpos.contract.Requests;

namespace vpos.contract.Controllers
{
    public interface IPaymentController
    {
        IActionResult Withdraw([FromBody]WithdrawRequest withdrawRequest);

        IActionResult Balance([FromBody]BalanceRequest balanceRequest);

        IActionResult Deposit([FromBody]DepositRequest depositRequest);
    }
}
