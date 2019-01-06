namespace atmps.domain.appServices
{
    /// <summary>
    /// Card type checker.
    /// </summary>
    public interface ICardTypeChecker
    {
        CardType GetCardType(string cardNumber);
    }
}
