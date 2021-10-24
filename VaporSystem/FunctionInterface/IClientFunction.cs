using System.Net.Sockets;
using System.Threading.Tasks;

namespace FunctionInterface
{
    public interface IClientFunction
    {
        string Name { get; set; }
        Task ExecuteAsync(Socket socket = null, string session = null);
    }
}