using System;
using System.Text.RegularExpressions;
using atmps.domain.appServices.Exceptions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Card type checker.
    /// </summary>
    public class CardTypeChecker : ICardTypeChecker
    {
        public const string VisaRegex = @"^4[0-9]{6,}$";
        public const string MasterCardRegex = @"^5[1-5][0-9]{5,}|222[1-9][0-9]{3,}|22[3-9][0-9]{4,}|2[3-6][0-9]{5,}|27[01][0-9]{4,}|2720[0-9]{3,}$";
        public const string AmexRegex = @"^3[47][0-9]{5,}$";

        /// <summary>
        /// Gets the type of the card number.
        /// </summary>
        /// <returns>The card type.</returns>
        /// <param name="cardNumber">Card number.</param>
        public CardType GetCardType(string cardNumber)
        {
            if (Regex.IsMatch(cardNumber, AmexRegex))
                return CardType.AMEX;

            if (Regex.IsMatch(cardNumber, VisaRegex))
                return CardType.VISA;

            if (Regex.IsMatch(cardNumber, MasterCardRegex))
                return CardType.MASTERCARD;

            throw new CardTypeNotFoundException("Card type can't be found!");
        }
    }
}
