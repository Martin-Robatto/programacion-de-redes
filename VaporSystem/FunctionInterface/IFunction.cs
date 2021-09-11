using System.Net.Sockets;
using Protocol;

namespace FunctionInterface
{
    public interface IFunction
    {
        public void Execute(Socket socket, Header header = null);
    }
}