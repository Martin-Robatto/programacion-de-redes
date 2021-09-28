using Protocol;

namespace Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string entity) : base($"{entity} Not Found", StatusCodeConstants.NOT_FOUND) { }
    }
}