using Microsoft.AspNetCore.Mvc;
using vpos.contract.Controllers;
using vpos.contract.Requests;
using vpos.contract.Utils;
using vpos.seb.business.Payments;

namespace vpos.seb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase, IPaymentController
    {
        public IPaymentManager PaymentManager { get; }

        public PaymentController(IPaymentManager paymentManager)
        {
            PaymentManager = paymentManager;
        }

        [HttpPost("Withdraw")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Withdraw([FromBody] WithdrawRequest withdrawRequest)
        {
            var result = PaymentManager.Withdraw(new WithdrawOperation
            {
                AccountNumber = withdrawRequest.AccountNumber,
                WithdrawAmount = withdrawRequest.WithdrawAmount
            });

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("Balance")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Balance([FromBody] BalanceRequest balanceRequest)
        {
            var result = PaymentManager.Balance(new BalanceOperation
            {
                AccountNumber = balanceRequest.AccountNumber
            });

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("Deposit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Deposit([FromBody] DepositRequest depositRequest)
        {
            var result = PaymentManager.Deposit(new DepositOperation
            {
                AccountNumber = depositRequest.AccountNumber,
                DepositAmount = depositRequest.DepositAmount
            });

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
