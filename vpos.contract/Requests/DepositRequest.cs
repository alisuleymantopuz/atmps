namespace vpos.contract.Requests
{
    /// <summary>
    /// Deposit request contract.
    /// </summary>
    public class DepositRequest
    {
        public string AccountNumber { get; set; }
        public decimal DepositAmount { get; set; }
    }
}
