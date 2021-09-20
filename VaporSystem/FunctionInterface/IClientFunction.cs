using System.Net.Sockets;
using Protocol;

namespace FunctionInterface
{
    public interface IClientFunction
    {
        string Name { get; set; }
        void Execute(NetworkStream stream, string session = null);
    }
}