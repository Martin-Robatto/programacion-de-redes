using System;

namespace Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }

        public AppException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}