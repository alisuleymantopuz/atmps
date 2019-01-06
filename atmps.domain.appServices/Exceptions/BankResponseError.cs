using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Bank response error.
    /// </summary>
    public class BankResponseError : Exception
    {
        public BankResponseError(string message) : base(message)
        {
        }

        public BankResponseError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankResponseError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
