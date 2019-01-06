namespace atmps.domain.appServices
{
    /// <summary>
    /// Transaction processor.
    /// </summary>
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}
