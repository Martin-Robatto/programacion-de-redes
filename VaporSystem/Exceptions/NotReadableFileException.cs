namespace Exceptions
{
    public class NotReadableFileException : AppException
    {
        public NotReadableFileException() : base("Couldn't not read file", 500) { }
    }
}