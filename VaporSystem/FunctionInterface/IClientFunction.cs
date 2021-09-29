using System.Net.Sockets;

namespace FunctionInterface
{
    public interface IClientFunction
    {
        string Name { get; set; }
        void Execute(NetworkStream stream = null, string session = null);
    }
}