using System;
using System.Collections.Generic;
using System.Linq;
using atmps.domain.appServices.tests.Fake;
using atmps.domain.infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Fee transaction list query handler tests.
    /// </summary>
    public class FeeTransactionListQueryHandlerTests
    {
        Mock<AppDbContext> _appDbContext = new Mock<AppDbContext>();

        /// <summary>
        /// Handlers the should return fee list in correct model.
        /// </summary>
        [Fact]
        public void Handler_ShouldReturnFeeListInCorrectModel()
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                OperationResult = true,
                TransactionType = TransactionType.WITHDRAW,
                CardNumber = FakeData.FakeCardInfoVisa.Number,
                TransactionDate = DateTime.UtcNow,
                TransactionFee = 1,
                WithdrawAmount = 101
            };

            var transactions = (new List<Transaction>() { transaction }).AsQueryable();

            var transactionsDbSet = new Mock<DbSet<Transaction>>();
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(transactions.Provider);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(transactions.Expression);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(transactions.ElementType);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(transactions.GetEnumerator());
            _appDbContext.Setup(m => m.Transactions).Returns(transactionsDbSet.Object);


            var queryHandler = new FeeTransactionListQueryHandler(_appDbContext.Object);

            var result = queryHandler.Handle(new FeeTransactionListQuery());

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();

            result.Value.Count().Should().Be(1);

            result.Value.First().CardNumber.Should().Be(transaction.CardNumber);
        }

        /// <summary>
        /// Handlers the should not return return fee list if there is no with draw transaction.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotReturnReturnFeeListIfThereIsNoWithDrawTransaction()
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                OperationResult = true,
                TransactionType = TransactionType.DEPOSIT,
                CardNumber = FakeData.FakeCardInfoVisa.Number,
                TransactionDate = DateTime.UtcNow,
                TransactionFee = 1,
                WithdrawAmount = 101
            };

            var transactions = (new List<Transaction>() { transaction }).AsQueryable();

            var transactionsDbSet = new Mock<DbSet<Transaction>>();
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Provider).Returns(transactions.Provider);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.Expression).Returns(transactions.Expression);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.ElementType).Returns(transactions.ElementType);
            transactionsDbSet.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(transactions.GetEnumerator());
            _appDbContext.Setup(m => m.Transactions).Returns(transactionsDbSet.Object);


            var queryHandler = new FeeTransactionListQueryHandler(_appDbContext.Object);

            var result = queryHandler.Handle(new FeeTransactionListQuery());

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();

            result.Value.Count().Should().Be(0);
        }
    }
}
