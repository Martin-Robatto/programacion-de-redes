using FunctionInterface;
using System.Net.Sockets;

namespace ConsoleClient.Function.Menu
{
    public class GoToGamesMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(Socket socket, string session = null)
        {
            ClientHandler.Instance.SetActualFunctions(FunctionDictionary.Games());
        }

        public GoToGamesMenuFunction()
        {
            Name = "Juegos";
        }
    }
}