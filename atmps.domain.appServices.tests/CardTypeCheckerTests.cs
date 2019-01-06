using atmps.domain.appServices.Exceptions;
using atmps.domain.appServices.tests.Fake;
using FluentAssertions;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Card type checker tests.
    /// </summary>
    public class CardTypeCheckerTests
    {
        /// <summary>
        /// Gets the card type should return correct card type for card number.
        /// </summary>
        [Fact]
        public void GetCardType_ShouldReturnCorrectCardTypeForCardNumber()
        {
            var masterCardNumber = FakeData.FakeCardInfoMasterCard.Number;

            var visaCardNumber = FakeData.FakeCardInfoVisa.Number;

            var amexCardNumber = FakeData.FakeCardInfoAMEX.Number;

            var cardTypeChecker = new CardTypeChecker();

            var amexCardType = cardTypeChecker.GetCardType(amexCardNumber);

            var masterCardType = cardTypeChecker.GetCardType(masterCardNumber);

            var visaCardType = cardTypeChecker.GetCardType(visaCardNumber);

            amexCardType.Should().Be(CardType.AMEX);
            masterCardType.Should().Be(CardType.MASTERCARD);
            visaCardType.Should().Be(CardType.VISA);
        }

        /// <summary>
        /// Gets the card type should return card type not found exception for wrong card number.
        /// </summary>
        [Fact]
        public void GetCardType_ShouldReturnCardTypeNotFoundExceptionForEmptyCardNumber()
        {
            var wrongCardNumber = FakeData.EmptyCardInfo.Number;

            var cardTypeChecker = new CardTypeChecker();

            CardTypeNotFoundException ex = Assert.Throws<CardTypeNotFoundException>(() => cardTypeChecker.GetCardType(wrongCardNumber));

            ex.Should().NotBeNull();
        }
    }
}
