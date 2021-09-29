using Protocol;
using System.Net.Sockets;

namespace FunctionInterface
{
    public interface IServerFunction
    {
        void Execute(NetworkStream stream, Header header = null);
    }
}