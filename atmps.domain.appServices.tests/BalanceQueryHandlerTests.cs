using atmps.domain.appServices.tests.Fake;
using atmps.domain.infrastructure;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Balance query handler tests.
    /// </summary>
    public class BalanceQueryHandlerTests
    {
        Mock<IBalanceCommunication> _balanceCommunication = new Mock<IBalanceCommunication>();
        Mock<ICardNumberValidationService> _cardNumberValidationService = new Mock<ICardNumberValidationService>();
        Mock<IAuditLogProcessor> _auditLogProcessor = new Mock<IAuditLogProcessor>();
        Mock<ITransactionProcessor> _transactionProcessor = new Mock<ITransactionProcessor>();
        Mock<AppDbContext> _appDbContext = new Mock<AppDbContext>();

        /// <summary>
        /// Handlers the should process transaction and audit.
        /// </summary>
        [Fact]
        public void Handler_ShouldProcessTransactionAndAudit()
        {
            var card = FakeData.FakeCardInfoMasterCard;
            decimal expectedResult = 20;

            _balanceCommunication.Setup(x => x.GetBalance(It.IsAny<string>(), It.IsAny<string>())).Returns(Result.Ok<decimal>(expectedResult));
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);

            var queryHandler = new BalanceQueryHandler(_appDbContext.Object,
                _auditLogProcessor.Object,
                _balanceCommunication.Object,
                _cardNumberValidationService.Object,
                _transactionProcessor.Object);

            var query = new BalanceQuery(card.Number);

            var result = queryHandler.Handle(query);

            _transactionProcessor.Verify(x => x.Process(It.IsAny<Transaction>()), Times.Once);

            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Handlers the should not be ok with empty card.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotBeOkWithEmptyCard()
        {
            var card = FakeData.EmptyCardInfo;

            _balanceCommunication.Setup(x => x.GetBalance(It.IsAny<string>(), It.IsAny<string>())).Returns(Result.Ok<decimal>(20));

            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);

            var queryHandler = new BalanceQueryHandler(_appDbContext.Object,
                _auditLogProcessor.Object,
                _balanceCommunication.Object,
                _cardNumberValidationService.Object,
                _transactionProcessor.Object);

            var query = new BalanceQuery(card.Number);

            var result = queryHandler.Handle(query);


            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeFalse();
        }
    }
}
