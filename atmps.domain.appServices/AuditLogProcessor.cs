using System;
using atmps.domain.infrastructure;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Audit log processor.
    /// </summary>
    public class AuditLogProcessor : IAuditLogProcessor
    {
        public AuditLogProcessor(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public void Process(string cardNumber, OperationType operationType, bool operationResult)
        {
            var log = new AuditLog()
            {
                CardNumber = cardNumber,
                OperationType = operationType,
                LogDate = DateTime.Now,
                OperationResult = operationResult
            };

            AppDbContext.AuditLogs.Add(log);
            AppDbContext.SaveChanges();
        }
    }
}
