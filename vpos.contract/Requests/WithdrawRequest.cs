namespace vpos.contract.Requests
{
    /// <summary>
    /// Withdraw request contract.
    /// </summary>
    public class WithdrawRequest
    {
        public string AccountNumber { get; set; }
        public decimal WithdrawAmount { get; set; }
    }
}
