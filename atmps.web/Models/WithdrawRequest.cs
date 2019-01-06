using System;
namespace atmps.web.Models
{
    /// <summary>
    /// Withdraw request.
    /// </summary>
    public class WithdrawRequest
    {
        public int Amount { get; set; }
        public string CardNumber { get; set; }
    }
}
