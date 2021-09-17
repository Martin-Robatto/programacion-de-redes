using System;

namespace Exceptions
{
    public class InvalidInputException : AppException
    {
        public InvalidInputException(string entity) : base($"Invalid {entity}", 400)
        { }
    }
}