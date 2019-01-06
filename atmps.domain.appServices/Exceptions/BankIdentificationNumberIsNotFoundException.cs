using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Bank identification number is not found exception.
    /// </summary>
    public class BankIdentificationNumberIsNotFoundException : Exception
    {
        public BankIdentificationNumberIsNotFoundException(string message) : base(message)
        {
        }

        public BankIdentificationNumberIsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankIdentificationNumberIsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
