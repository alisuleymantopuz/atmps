using System.Text.RegularExpressions;

namespace atmps.domain.appServices.Utilities
{
    /// <summary>
    /// Card number helper.
    /// </summary>
    public class CardNumberHelper : ICardNumberHelper
    {
        /// <summary>
        /// Checks the chard number is valid.
        /// </summary>
        /// <returns><c>true</c>, if chard number is valid was checked, <c>false</c> otherwise.</returns>
        /// <param name="cardNumber">Card number.</param>
        public bool CheckChardNumberIsValid(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return false;

            var regex = new Regex(@"^[0-9]*$");

            if (!regex.IsMatch(cardNumber))
                return false;

            int nDigits = cardNumber.Length;

            int nSum = 0;
            bool isSecond = false;
            for (int i = nDigits - 1; i >= 0; i--)
            {
                int d = cardNumber[i] - '0';

                if (isSecond == true)
                    d = d * 2;

                nSum += d / 10;
                nSum += d % 10;

                isSecond = !isSecond;
            }

            return (nSum % 10 == 0);
        }

        /// <summary>
        /// Masks the card number.
        /// </summary>
        /// <returns>The card number.</returns>
        /// <param name="cardNumber">Card number.</param>
        public string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return string.Empty;

            if (cardNumber.Length < 6)
                return string.Empty;

            return string.Format("xxxxxxxxxxxx{0}", cardNumber.Substring(cardNumber.Length - 4, 4));
        }

        /// <summary>
        /// Gets the bin.
        /// </summary>
        /// <returns>The bin.</returns>
        /// <param name="cardNumber">Card number.</param>
        public string GetBIN(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return string.Empty;

            if (cardNumber.Length < 6)
                return string.Empty;

            return cardNumber.Substring(0, 6);
        }
    }
}
