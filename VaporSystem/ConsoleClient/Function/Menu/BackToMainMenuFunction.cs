using FunctionInterface;
using System.Net.Sockets;

namespace ConsoleClient.Function.Menu
{
    public class BackToMainMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(Socket socket, string session = null)
        {
            ClientHandler.Instance.SetActualFunctions(FunctionDictionary.Main());
        }

        public BackToMainMenuFunction()
        {
            Name = "Volver al Menu";
        }
    }
}