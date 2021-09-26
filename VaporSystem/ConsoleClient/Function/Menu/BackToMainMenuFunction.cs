using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleClient.Function.Menu
{
    public class BackToMainMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(NetworkStream stream, string session = null)
        {
            ClientHandler.SetActualFunctions(FunctionDictionary.Main());
        }

        public BackToMainMenuFunction()
        {
            Name = "Volver al Menu";
        }
    }
}