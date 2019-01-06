using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Account number is not valid exception.
    /// </summary>
    public class AccountNumberIsNotValidException : Exception
    {
        public AccountNumberIsNotValidException(string message) : base(message)
        {
        }

        public AccountNumberIsNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountNumberIsNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
