namespace Exceptions
{
    public class ConnectionClosedException : AppException
    {
        public ConnectionClosedException() : base("Connection closed") { }
    }
}