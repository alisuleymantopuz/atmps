using System;
using System.Linq;
using atmps.domain.appServices.Exceptions;
using atmps.domain.appServices.Utilities;
using atmps.domain.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Card number validation service.
    /// </summary>
    public class CardNumberValidationService : ICardNumberValidationService
    {
        public CardNumberValidationService(AppDbContext appDbContext, ICardNumberHelper cardNumberHelper, ICardTypeChecker cardTypeChecker)
        {
            AppDbContext = appDbContext;
            CardNumberHelper = cardNumberHelper;
            CardTypeChecker = cardTypeChecker;
        }

        public AppDbContext AppDbContext { get; }
        public ICardNumberHelper CardNumberHelper { get; }
        public ICardTypeChecker CardTypeChecker { get; }

        /// <summary>
        /// Populates the card info model for validated credit cards.
        /// </summary>
        /// <returns>The card info.</returns>
        /// <param name="cardNumber">Card number.</param>
        public CardInfo PopulateCardInfo(string cardNumber)
        {
            var cardInfo = new CardInfo();

            BankIdentificationNumber bankIdentificationNumber = GetIdentificationNumber(cardNumber);
            cardInfo.BankIdentificationNumber = bankIdentificationNumber;
            cardInfo.BankInfo = bankIdentificationNumber.BankInfo;
            cardInfo.Number = cardNumber;
            cardInfo.AccountNumber = GetAccountNumber(cardNumber);
            cardInfo.MaskedCardNumber = CardNumberHelper.MaskCardNumber(cardNumber);
            cardInfo.CardType = GetCardType(cardNumber);

            return cardInfo;
        }

        /// <summary>
        /// Validates the card number and returns the account number.
        /// </summary>
        /// <returns>The account number.</returns>
        /// <param name="cardNumber">Card number.</param>
        public string GetAccountNumber(string cardNumber)
        {
            if (!CardNumberHelper.CheckChardNumberIsValid(cardNumber))
                throw new CardNumberNotValidException("Card number is not valid!");

            //6 digits for BIN, 1 digit for cheksum.
            int accountNumberLength = cardNumber.Length - 7;

            string accountNumber = cardNumber.Substring(6, accountNumberLength);
            
            return accountNumber;
        }

        /// <summary>
        /// Gets the identification number for validated cardNumbers.
        /// </summary>
        /// <returns>The identification number.</returns>
        /// <param name="cardNumber">Card number.</param>
        public BankIdentificationNumber GetIdentificationNumber(string cardNumber)
        {
            var bin = CardNumberHelper.GetBIN(cardNumber);

            if(string.IsNullOrEmpty(bin))
                throw new BankIdentificationNumberIsNotFoundException("Bank identification number is not found!");

            var bankIdentificationNumber = AppDbContext.BankIdentificationNumbers.Include(x => x.BankInfo).FirstOrDefault(x => x.BIN == bin);

            if (bankIdentificationNumber == null)
                throw new BankIdentificationNumberIsNotFoundException("Bank identification number is not found!");

            return bankIdentificationNumber;
        }

        /// <summary>
        /// Validates the card number.
        /// </summary>
        /// <returns><c>true</c>, if card number was validated, <c>false</c> otherwise.</returns>
        /// <param name="cardNumber">Card number.</param>
        public bool ValidateCardNumber(string cardNumber)
        {
            if (!CardNumberHelper.CheckChardNumberIsValid(cardNumber))
                throw new CardNumberNotValidException("Card number is not valid!");

            return true;
        }

        /// <summary>
        /// Gets the type of the validated card number.
        /// </summary>
        /// <returns>The card type.</returns>
        /// <param name="cardNumber">Card number.</param>
        public CardType GetCardType(string cardNumber)
        {
            if (!CardNumberHelper.CheckChardNumberIsValid(cardNumber))
                throw new CardNumberNotValidException("Card number is not valid!");

            return CardTypeChecker.GetCardType(cardNumber);
        }
    }
}
