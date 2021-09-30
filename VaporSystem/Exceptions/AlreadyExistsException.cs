using Protocol;

namespace Exceptions
{
    public class AlreadyExistsException : AppException
    {
        public AlreadyExistsException(string entity) : base($"The {entity} Already Exists", StatusCodeConstants.BAD_REQUEST) { }
    }
}