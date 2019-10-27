using System;
using System.Runtime.Serialization;

namespace FundConnRec.Models.Exceptions
{
    [Serializable]
    public class ChangeConflictException : Exception
    {
        public ChangeConflictException()
        {
        }

        public ChangeConflictException(string message) : base(message)
        {
        }

        public ChangeConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChangeConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}