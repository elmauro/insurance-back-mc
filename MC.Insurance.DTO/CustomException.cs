using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.DTO
{
    public class CustomException : Exception
    {
        public Enumerations.StatusCode statusCode { get; }

        protected CustomException() { }

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
