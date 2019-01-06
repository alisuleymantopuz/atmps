using System;
namespace atmps.web.Models
{
    /// <summary>
    /// Deposit request.
    /// </summary>
    public class DepositRequest
    {
        public int Amount { get; set; }
        public string CardNumber { get; set; }
    }
}
