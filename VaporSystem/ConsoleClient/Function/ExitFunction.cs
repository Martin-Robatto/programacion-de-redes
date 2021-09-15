using System.Net.Sockets;
using Protocol;

namespace ConsoleClient.Function
{
    public class ExitFunction : IFunction.IFunction
    {
        public string Name { get; set; }
        public const string NAME = "Exit";

        public void Execute(Socket socket, Header header = null)
        {
           ClientHandler.ShutDown();
        }

        public ExitFunction()
        {
            Name = NAME;
        }
    }
}