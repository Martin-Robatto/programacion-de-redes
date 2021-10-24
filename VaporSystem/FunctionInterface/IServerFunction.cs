using Protocol;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FunctionInterface
{
    public interface IServerFunction
    {
        Task ExecuteAsync(Socket socket, Header header = null);
    }
}