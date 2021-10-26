using FunctionInterface;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleClient.Function
{
    public class ExitFunction : IClientFunction
    {
        public const string NAME = "Salir";

        public string Name { get; set; }

        public Task ExecuteAsync(Socket socket = null, string session = null)
        {
            ClientHandler.Instance.ShutDown();
            return Task.CompletedTask;
        }

        public ExitFunction()
        {
            Name = NAME;
        }
    }
}