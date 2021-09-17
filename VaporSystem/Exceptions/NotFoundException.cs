namespace Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string entity) : base($"{entity} Not Found", 404) { }
    }
}