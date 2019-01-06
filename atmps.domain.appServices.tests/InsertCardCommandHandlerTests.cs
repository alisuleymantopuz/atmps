using atmps.domain.appServices.tests.Fake;
using FluentAssertions;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Insert card command handler tests.
    /// </summary>
    public class InsertCardCommandHandlerTests
    {
        Mock<ICardNumberValidationService> _cardNumberValidationService = new Mock<ICardNumberValidationService>();
        Mock<IAuditLogProcessor> _auditLogProcessor = new Mock<IAuditLogProcessor>();

        /// <summary>
        /// Handlers the should allow to insert card with correct card number.
        /// </summary>
        [Fact]
        public void Handler_ShouldAllowToInsertCardWithCorrectCardNumber()
        {
            var card = FakeData.FakeCardInfoMasterCard;
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);
            var commandHandler = new InsertCardCommandHandler(_auditLogProcessor.Object, _cardNumberValidationService.Object);
            var result = commandHandler.Handle(new InsertCardCommand(card.Number));
            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Handlers the should not allow to insert card with wrong card number.
        /// </summary>
        [Fact]
        public void Handler_ShouldNotAllowToInsertCardWithEmptyCardNumber()
        {
            var card = FakeData.EmptyCardInfo;
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);
            var commandHandler = new InsertCardCommandHandler(_auditLogProcessor.Object, _cardNumberValidationService.Object);
            var result = commandHandler.Handle(new InsertCardCommand(card.Number));
            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);
            result.IsSuccess.Should().BeFalse();
        }
    }
}
