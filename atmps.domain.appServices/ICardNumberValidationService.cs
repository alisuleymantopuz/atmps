namespace atmps.domain.appServices
{
    /// <summary>
    /// Card number validation service.
    /// </summary>
    public interface ICardNumberValidationService
    {
        bool ValidateCardNumber(string cardNumber);
        CardInfo PopulateCardInfo(string cardNumber);
        string GetAccountNumber(string cardNumber);
        CardType GetCardType(string cardNumber);
        BankIdentificationNumber GetIdentificationNumber(string cardNumber);
    }
}
