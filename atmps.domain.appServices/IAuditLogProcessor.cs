namespace atmps.domain.appServices
{
    /// <summary>
    /// Audit log processor contract.
    /// </summary>
    public interface IAuditLogProcessor
    {
        void Process(string cardNumber, OperationType operationType, bool operationResult);
    }
}
