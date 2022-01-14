using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MC.Insurance.DTO
{
    [Serializable]
    public class CustomException : Exception
    {
        public Enumerations.StatusCode statusCode { get; }

        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public CustomException(Enumerations.StatusCode statusCode, string message)
            : base(message)
        {
            this.statusCode = statusCode;
        }
        public CustomException(Exception innerException, int code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        { }

        public CustomException(Exception innerException, Enumerations.StatusCode statusCode, string message)
            : base(message, innerException)
        {
            this.statusCode = statusCode;
        }
    }
}
