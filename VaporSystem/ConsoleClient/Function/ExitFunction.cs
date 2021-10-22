using FunctionInterface;
using System.Net.Sockets;

namespace ConsoleClient.Function
{
    public class ExitFunction : IClientFunction
    {
        public const string NAME = "Salir";

        public string Name { get; set; }
        public void Execute(Socket socket = null, string session = null)
        {
            ClientHandler.Instance.ShutDown();
        }

        public ExitFunction()
        {
            Name = NAME;
        }
    }
}