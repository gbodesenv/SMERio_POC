using System;
using System.Runtime.Serialization;

namespace Rio.SME.Domain.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
            : base()
        {
        }

        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}