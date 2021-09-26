using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleClient.Function.Menu
{
    public class GoToPublishesMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(NetworkStream stream, string session = null)
        {
            ClientHandler.SetActualFunctions(FunctionDictionary.Publishes());
        }

        public GoToPublishesMenuFunction()
        {
            Name = "Publicaciones";
        }
    }
}