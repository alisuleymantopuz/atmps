using System;
namespace atmps.domain
{
    public class Fee
    {
        public string CardNumber { get; set; }
        public decimal WithdrawalFeeAmount { get; set; }
        public decimal WithdrawalTotalAmount { get; set; }
        public DateTime WithdrawalDate { get; set; }
    }
}
