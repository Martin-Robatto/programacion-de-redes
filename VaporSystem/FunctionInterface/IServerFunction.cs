using Protocol;
using System.Net.Sockets;

namespace FunctionInterface
{
    public interface IServerFunction
    {
        void Execute(Socket socket, Header header = null);
    }
}