using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Bank info is not found exception.
    /// </summary>
    public class BankInfoIsNotFoundException : Exception
    {
        public BankInfoIsNotFoundException(string message) : base(message)
        {
        }

        public BankInfoIsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankInfoIsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
