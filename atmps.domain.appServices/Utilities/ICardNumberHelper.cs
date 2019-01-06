namespace atmps.domain.appServices.Utilities
{
    /// <summary>
    /// Card number helper.
    /// </summary>
    public interface ICardNumberHelper
    {
        bool CheckChardNumberIsValid(string cardNumber);
        string MaskCardNumber(string cardNumber);
        string GetBIN(string cardNumber);
    }
}
