using System.Net.Sockets;
using Protocol;

namespace IFunction
{
    public interface IFunction
    {
        string Name { get; set; }
        void Execute(Socket socket, Header header = null);
    }
}