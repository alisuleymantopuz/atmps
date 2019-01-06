using System;
using System.Runtime.Serialization;

namespace atmps.domain.appServices.Exceptions
{
    public class CardTypeNotFoundException : Exception
    {
        public CardTypeNotFoundException()
        {
        }

        public CardTypeNotFoundException(string message) : base(message)
        {
        }

        public CardTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CardTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
