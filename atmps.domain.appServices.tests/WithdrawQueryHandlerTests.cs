using atmps.domain.appServices.tests.Fake;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Withdraw query handler tests.
    /// </summary>
    public class WithdrawQueryHandlerTests
    {
        Mock<IWithdrawCommunication> _withdrawCommunication = new Mock<IWithdrawCommunication>();
        Mock<ICardNumberValidationService> _cardNumberValidationService = new Mock<ICardNumberValidationService>();
        Mock<IAuditLogProcessor> _auditLogProcessor = new Mock<IAuditLogProcessor>();
        Mock<ITransactionProcessor> _transactionProcessor = new Mock<ITransactionProcessor>();
        Mock<IMoneyGenerator> _moneyGenerator = new Mock<IMoneyGenerator>();

        /// <summary>
        /// Handlers the should process transaction and audit.
        /// </summary>
        [Fact]
        public void Handler_ShouldProcessTransactionAndAudit()
        {
            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);
            _moneyGenerator.Setup(x => x.GenerateMoney(It.IsAny<int>())).Returns(new Money());

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                _moneyGenerator.Object);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, 10);

            var result = queryHandler.Handle(withDrawQuery);

            _transactionProcessor.Verify(x => x.Process(It.IsAny<Transaction>()), Times.Once);

            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Handlers the should return money set.
        /// </summary>
        [Fact]
        public void Handler_ShouldReturnMoneySet()
        {
            var amount = 10;
            var moneyGenerator = new MoneyGenerator();
            var moneySet = moneyGenerator.GenerateMoney(amount);

            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                moneyGenerator);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, amount);

            var result = queryHandler.Handle(withDrawQuery);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();

            result.Value.Amount.Should().Be(moneySet.Amount);

            result.Value.Notes.Count.Should().Be(moneySet.Notes.Count);
        }

        /// <summary>
        /// Handlers the should be ok with correct amount.
        /// </summary>
        [Fact]
        public void Handler_ShouldBeOkWithCorrectAmount()
        {
            var amount = 10;

            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);
            _moneyGenerator.Setup(x => x.GenerateMoney(It.IsAny<int>())).Returns(new Money());

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                _moneyGenerator.Object);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, amount);

            var result = queryHandler.Handle(withDrawQuery);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Handlers the should not be ok with wrong amount.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotBeOkWithWrongAmount()
        {
            var amount = 13;

            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);
            _moneyGenerator.Setup(x => x.GenerateMoney(It.IsAny<int>())).Returns(new Money());

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                _moneyGenerator.Object);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, amount);

            var result = queryHandler.Handle(withDrawQuery);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeFalse();
        }

        /// <summary>
        /// Handlers the should not be ok with wrong amount 2.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotBeOkWithWrongAmount_2()
        {
            var amount = -10;

            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);
            _moneyGenerator.Setup(x => x.GenerateMoney(It.IsAny<int>())).Returns(new Money());

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                _moneyGenerator.Object);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, amount);

            var result = queryHandler.Handle(withDrawQuery);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeFalse();
        }

        /// <summary>
        /// Handlers the should return ok with correct card number when vpos returns failed with response code.
        /// </summary>
        [Fact]
        public void Handler_ShouldReturnOkWithCorrectCardNumberWhenVposReturnsFailedWithResponseCode()
        {
            string responseCode = "A1001";

            _withdrawCommunication.Setup(x => x.WithdrawMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Fail(responseCode));
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(FakeData.FakeCardInfoMasterCard);
            _moneyGenerator.Setup(x => x.GenerateMoney(It.IsAny<int>())).Returns(new Money());

            var queryHandler = new WithdrawQueryHandler(_withdrawCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object,
                _moneyGenerator.Object);

            var withDrawQuery = new WithdrawQuery(FakeData.FakeCardInfoMasterCard.Number, 10);

            var result = queryHandler.Handle(withDrawQuery);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeFalse();

            result.Error.Should().Be(responseCode);
        }
    }
}
