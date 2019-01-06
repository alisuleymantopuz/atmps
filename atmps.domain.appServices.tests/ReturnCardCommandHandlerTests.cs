using atmps.domain.appServices.tests.Fake;
using FluentAssertions;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Return card command handler tests.
    /// </summary>
    public class ReturnCardCommandHandlerTests
    {
        Mock<ICardNumberValidationService> _cardNumberValidationService = new Mock<ICardNumberValidationService>();
        Mock<IAuditLogProcessor> _auditLogProcessor = new Mock<IAuditLogProcessor>();

        /// <summary>
        /// Handlers the should allow to return card.
        /// </summary>
        [Fact]
        public void Handler_ShouldAllowToReturnCard()
        {
            var card = FakeData.FakeCardInfoMasterCard;
            _cardNumberValidationService.Setup(x => x.PopulateCardInfo(It.IsAny<string>())).Returns(card);
            var commandHandler = new ReturnCardCommandHandler(_auditLogProcessor.Object);
            var result = commandHandler.Handle(new ReturnCardCommand(card.Number));
            _auditLogProcessor.Verify(x => x.Process(It.IsAny<string>(), It.IsAny<OperationType>(), It.IsAny<bool>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
        }
    }
}
