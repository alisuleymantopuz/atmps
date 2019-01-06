using System.Collections.Generic;
using atmps.domain;
using atmps.domain.appServices;
using atmps.domain.appServices.Utilities;
using atmps.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace atmps.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        public ATMController(Messages messages)
        {
            Messages = messages;
        }

        public Messages Messages { get; }

        // GET api/values
        [HttpGet("GetManufacturer")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult GetManufacturer()
        {
            var manufacturer = Messages.Dispatch(new ManufacturerQuery());

            return Ok(manufacturer);
        }

        [HttpGet("GetSerialNumber")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult GetSerialNumber()
        {
            var serialNumber = Messages.Dispatch(new SerialNumberQuery());

            return Ok(serialNumber);
        }

        [HttpPost("InsertCard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult InsertCard([FromBody]string cardNumber)
        {
            var result = Messages.Dispatch(new InsertCardCommand(cardNumber));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpGet("GetCardBalance")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult GetCardBalance([FromBody]string cardNumber)
        {
            var result = Messages.Dispatch(new BalanceQuery(cardNumber));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("WithdrawMoney")]
        [ProducesResponseType(200, Type = typeof(Money))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult WithdrawMoney([FromBody]WithdrawRequest withdrawRequest)
        {
            var result = Messages.Dispatch(new WithdrawQuery(withdrawRequest.CardNumber, withdrawRequest.Amount));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("ReturnCard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult ReturnCard([FromBody]string cardNumber)
        {
            var result = Messages.Dispatch(new ReturnCardCommand(cardNumber));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("LoadMoney")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult LoadMoney([FromBody]DepositRequest depositRequest)
        {
            var result = Messages.Dispatch(new DepositCommand(depositRequest.CardNumber, depositRequest.Amount));

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpGet("RetrieveChargedFees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fee>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult RetrieveChargedFees()
        {
            var result = Messages.Dispatch(new FeeTransactionListQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
