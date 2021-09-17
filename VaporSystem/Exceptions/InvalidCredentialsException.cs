using System;

namespace Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException(string entity) : base($"Invalid credentials for {entity}", 400)
        { }
    }
}