namespace atmps.domain.appServices
{
    /// <summary>
    /// Money generator.
    /// </summary>
    public interface IMoneyGenerator
    {
        Money GenerateMoney(int amount);
    }
}
