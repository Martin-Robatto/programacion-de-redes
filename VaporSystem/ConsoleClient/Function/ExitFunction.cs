using FunctionInterface;
using System.Net.Sockets;

namespace ConsoleClient.Function
{
    public class ExitFunction : IClientFunction
    {
        public const string NAME = "Salir";

        public string Name { get; set; }
        public void Execute(NetworkStream stream = null, string session = null)
        {
            ClientHandler.ShutDown();
        }

        public ExitFunction()
        {
            Name = NAME;
        }
    }
}