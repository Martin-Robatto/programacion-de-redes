using System.Net.Sockets;
using Protocol;

namespace FunctionInterface
{
    public interface IServerFunction
    {
        void Execute(NetworkStream stream, Header header = null);
    }
}