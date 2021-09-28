using System;
using Protocol;

namespace Exceptions
{
    public class InvalidInputException : AppException
    {
        public InvalidInputException(string entity) : base($"Invalid {entity}", StatusCodeConstants.BAD_REQUEST)
        { }
    }
}