using System.Net.Sockets;

namespace FunctionInterface
{
    public interface IClientFunction
    {
        string Name { get; set; }
        void Execute(Socket socket = null, string session = null);
    }
}