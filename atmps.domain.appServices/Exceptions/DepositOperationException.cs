using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Deposit operation exception.
    /// </summary>
    public class DepositOperationException:Exception
    {
        public DepositOperationException(string message) : base(message)
        {
        }

        public DepositOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepositOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
