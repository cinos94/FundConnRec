using System;
using System.Runtime.Serialization;

namespace FundConnRec.Models.Exceptions
{
    [Serializable]
    public class ToleranceOfOutRangeException : Exception
    {
        public ToleranceOfOutRangeException()
        {
        }

        public ToleranceOfOutRangeException(string message) : base(message)
        {
        }

        public ToleranceOfOutRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ToleranceOfOutRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}