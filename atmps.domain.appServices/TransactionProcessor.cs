using System;
using atmps.domain.infrastructure;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Transaction processor.
    /// </summary>
    public class TransactionProcessor : ITransactionProcessor
    {
        public TransactionProcessor(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        /// <summary>
        /// Process the specified transaction.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        public void Process(Transaction transaction)
        {
            AppDbContext.Transactions.Add(transaction);
            AppDbContext.SaveChanges();
        }
    }
}
