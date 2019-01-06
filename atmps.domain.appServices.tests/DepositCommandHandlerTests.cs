using System;
using atmps.domain.appServices.tests.Fake;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Deposit command handler tests.
    /// </summary>
    public class DepositCommandHandlerTests
    {
        Mock<IDepositCommunication> _depositCommunication = new Mock<IDepositCommunication>();
        Mock<ICardNumberValidationService> _cardNumberValidationService = new Mock<ICardNumberValidationService>();
        Mock<IAuditLogProcessor> _auditLogProcessor = new Mock<IAuditLogProcessor>();
        Mock<ITransactionProcessor> _transactionProcessor = new Mock<ITransactionProcessor>();

        /// <summary>
        /// Handlers the should process transaction and audit.
        /// </summary>
        [Fact]
        public void Handler_ShouldProcessTransactionAndAudit()
        {
            var card = FakeData.FakeCardInfoMasterCard;

            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);

            _depositCommunication.Setup(x => x.LoadMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());

            var commandHandler = new DepositCommandHandler(_depositCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object);

            var command = new DepositCommand(card.Number, 10);

            var result = commandHandler.Handle(command);

            _transactionProcessor.Verify(x => x.Process(It.IsAny<Transaction>()), Times.Once);

            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);
        }

        /// <summary>
        /// Handlers the should be ok for operated cards.
        /// </summary>
        [Fact]
        public void Handler_ShouldBeOkForOperatedBanks()
        {
            var card = FakeData.FakeCardInfoMasterCard;

            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);

            _depositCommunication.Setup(x => x.LoadMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());

            var commandHandler = new DepositCommandHandler(_depositCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object);

            var command = new DepositCommand(card.Number, 10);

            var result = commandHandler.Handle(command);

            result.Should().NotBeNull();
            
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Handlers the should not be ok for non operated cards.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotBeOkForNonOperatedBanks()
        {
            var card = FakeData.FakeCardInfoMasterCardButNotOperated;
            
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);

            _depositCommunication.Setup(x => x.LoadMoney(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Result.Ok());

            var commandHandler = new DepositCommandHandler(_depositCommunication.Object,
                _cardNumberValidationService.Object,
                _auditLogProcessor.Object,
                _transactionProcessor.Object);

            var command = new DepositCommand(card.Number, 10);

            var result = commandHandler.Handle(command);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeFalse();
        }
    }
}
