using System.Net.Sockets;
using Protocol;

namespace FunctionInterface
{
    public interface IClientFunction
    {
        string Name { get; set; }
        void Execute(NetworkStream stream = null, string session = null);
    }
}