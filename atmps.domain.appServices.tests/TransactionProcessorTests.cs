using System.Collections.Generic;
using System.Linq;
using atmps.domain.infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Transaction processor tests.
    /// </summary>
    public class TransactionProcessorTests
    {
        Mock<AppDbContext> _appDbContext = new Mock<AppDbContext>();

        /// <summary>
        /// Processes the should work with correct parameters.
        /// </summary>
        [Fact]
        public void Process_ShouldWorkWithCorrectParameters()
        {
            var transactions = new List<Transaction>();
            var transactionsQueryable = transactions.AsQueryable();
            var transactionsDbSet = new Mock<DbSet<Transaction>>();
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(transactionsQueryable.Provider);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(transactionsQueryable.Expression);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(transactionsQueryable.ElementType);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(transactionsQueryable.GetEnumerator());
            _appDbContext.Setup(m => m.Transactions).Returns(transactionsDbSet.Object);

            var transactionProcessor = new TransactionProcessor(_appDbContext.Object);
            transactionProcessor.Process(It.IsAny<Transaction>());

            transactionsDbSet.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Once);
            _appDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
