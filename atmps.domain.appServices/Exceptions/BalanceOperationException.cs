using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Balance operation exception.
    /// </summary>
    public class BalanceOperationException : Exception
    {
        public BalanceOperationException(string message) : base(message)
        {
        }

        public BalanceOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalanceOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
