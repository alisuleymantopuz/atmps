using atmps.domain.appServices.tests.Fake;
using atmps.domain.appServices.Utilities;
using FluentAssertions;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Card number helper tests.
    /// </summary>
    public class CardNumberHelperTests
    {
        /// <summary>
        /// Checks the chard number is valid should return true when card number is correct.
        /// </summary>
        [Fact]
        public void CheckChardNumberIsValid_ShouldReturnTrueWhenCardNumberIsCorrect()
        {
            var fakeCard = FakeData.FakeCardInfoMasterCard;

            var cardNumberHelper = new CardNumberHelper();

            var result = cardNumberHelper.CheckChardNumberIsValid(fakeCard.Number);

            result.Should().BeTrue();
        }

        /// <summary>
        /// Checks the chard number is valid should return false when card number is wrong.
        /// </summary>
        [Fact]
        public void CheckChardNumberIsValid_ShouldReturnFalseWhenCardNumberIsWrong()
        {
            var fakeCard = FakeData.WrongCardInfo;

            var cardNumberHelper = new CardNumberHelper();

            var result = cardNumberHelper.CheckChardNumberIsValid(fakeCard.Number);

            result.Should().BeFalse();
        }

        /// <summary>
        /// Checks the chard number is valid should return false when card number is empty.
        /// </summary>
        [Fact]
        public void CheckChardNumberIsValid_ShouldReturnFalseWhenCardNumberIsEmpty()
        {
            var fakeCard = FakeData.EmptyCardInfo;

            var cardNumberHelper = new CardNumberHelper();

            var result = cardNumberHelper.CheckChardNumberIsValid(fakeCard.Number);

            result.Should().BeFalse();
        }

        [Fact]
        public void GetBIN_ShouldReturnBinNumberForCardNumber()
        {
            var fakeCard = FakeData.FakeCardInfoMasterCard;

            var cardNumberHelper = new CardNumberHelper();

            var result = cardNumberHelper.GetBIN(fakeCard.Number);

            result.Should().NotBeNull();

            result.Length.Should().Be(6);
        }
    }
}
