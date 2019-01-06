namespace atmps.domain
{
    /// <summary>
    /// Card info.
    /// </summary>
    public class CardInfo
    {
        public string Number { get; set; }
        public BankInfo BankInfo { get; set; }
        public string AccountNumber { get; set; }
        public BankIdentificationNumber BankIdentificationNumber { get; set; }
        public string MaskedCardNumber { get; set; }
        public CardType CardType { get; set; }
    }
}
