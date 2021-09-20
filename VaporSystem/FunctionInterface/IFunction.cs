using System.Net.Sockets;
using Protocol;

namespace FunctionInterface
{
    public interface IFunction
    {
        string Name { get; set; }
        bool RequiredCredentials { get; set; }
        void Execute(NetworkStream stream, Header header = null, string session = null);
    }
}