using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Card number not valid exception.
    /// </summary>
    public class CardNumberNotValidException:Exception
    {
        public CardNumberNotValidException(string message) : base(message)
        {

        }

        public CardNumberNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CardNumberNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
