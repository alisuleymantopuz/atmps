using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    /// <summary>
    /// Bank is not operated in this ATME xception.
    /// </summary>
    public class BankIsNotOperatedInThisATMException:Exception
    {
        public BankIsNotOperatedInThisATMException(string message) : base(message)
        {
        }

        public BankIsNotOperatedInThisATMException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BankIsNotOperatedInThisATMException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
