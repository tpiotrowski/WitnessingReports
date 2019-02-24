using System;
using System.Runtime.Serialization;

namespace Witnessing.Client
{
    public class WitnessingServiceException : Exception
    {
        public WitnessingServiceException()
        {
        }

        protected WitnessingServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public WitnessingServiceException(string message) : base(message)
        {
        }

        public WitnessingServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}