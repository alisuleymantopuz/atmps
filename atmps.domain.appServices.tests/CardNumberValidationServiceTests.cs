using System.Collections.Generic;
using System.Linq;
using atmps.domain.appServices.Exceptions;
using atmps.domain.appServices.tests.Fake;
using atmps.domain.appServices.Utilities;
using atmps.domain.infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Card number validation service tests.
    /// </summary>
    public class CardNumberValidationServiceTests
    {
        Mock<ICardTypeChecker> _cardTypeChecker = new Mock<ICardTypeChecker>();
        Mock<ICardNumberHelper> _cardNumberHelper = new Mock<ICardNumberHelper>();
        Mock<AppDbContext> _appDbContext = new Mock<AppDbContext>();

        /// <summary>
        /// Populates the card info should not populate card info with wrong card number and throw bank identification
        /// number is not found exception.
        /// </summary>
        [Fact]
        public void PopulateCardInfo_ShouldNotPopulateCardInfoWithWrongCardNumberAndThrowBankIdentificationNumberIsNotFoundException()
        {
            _cardNumberHelper.Setup(x => x.GetBIN(It.IsAny<string>())).Returns(string.Empty);

            var service = new CardNumberValidationService(_appDbContext.Object, _cardNumberHelper.Object, _cardTypeChecker.Object);

            var wrongCardNumber = FakeData.WrongCardInfo.Number;

            BankIdentificationNumberIsNotFoundException ex = Assert.Throws<BankIdentificationNumberIsNotFoundException>(() => service.PopulateCardInfo(wrongCardNumber));

            ex.Should().NotBeNull();
        }

        /// <summary>
        /// Populates the card info should populate card info with correct card number.
        /// </summary>
        [Fact]
        public void PopulateCardInfo_ShouldPopulateCardInfoWithCorrectCardNumber()
        {
            var masterCard = FakeData.FakeCardInfoMasterCard;
            _cardNumberHelper.Setup(x => x.CheckChardNumberIsValid(It.IsAny<string>())).Returns(true);
            _cardNumberHelper.Setup(x => x.GetBIN(It.IsAny<string>())).Returns(masterCard.BankIdentificationNumber.BIN);
            _cardTypeChecker.Setup(x => x.GetCardType(It.IsAny<string>())).Returns(masterCard.CardType);

            var bankIdentifications = new List<BankIdentificationNumber>() { masterCard.BankIdentificationNumber }.AsQueryable();
            var bankIdentificationNumberDbSet = new Mock<DbSet<BankIdentificationNumber>>();
            bankIdentificationNumberDbSet.As<IQueryable<BankIdentificationNumber>>().Setup(m => m.Provider).Returns(bankIdentifications.Provider);
            bankIdentificationNumberDbSet.As<IQueryable<BankIdentificationNumber>>().Setup(m => m.Expression).Returns(bankIdentifications.Expression);
            bankIdentificationNumberDbSet.As<IQueryable<BankIdentificationNumber>>().Setup(m => m.ElementType).Returns(bankIdentifications.ElementType);
            bankIdentificationNumberDbSet.As<IQueryable<BankIdentificationNumber>>().Setup(m => m.GetEnumerator()).Returns(bankIdentifications.GetEnumerator());
            _appDbContext.Setup(m => m.BankIdentificationNumbers).Returns(bankIdentificationNumberDbSet.Object);

            var cardNumberValidationService = new CardNumberValidationService(_appDbContext.Object, _cardNumberHelper.Object, _cardTypeChecker.Object);

            var populatedCardInfo = cardNumberValidationService.PopulateCardInfo(masterCard.Number);

            populatedCardInfo.Should().NotBeNull();

            populatedCardInfo.Number.Should().Be(masterCard.Number);
        }
    }
}
