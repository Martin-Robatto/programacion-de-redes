using FunctionInterface;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleClient.Function.Menu
{
    public class BackToMainMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        private FunctionDictionary _functionDictionary = new FunctionDictionary();
        
        public Task ExecuteAsync(Socket socket, string session = null)
        {
            ClientHandler.Instance.SetActualFunctions(_functionDictionary.Main());
            return Task.CompletedTask;
        }

        public BackToMainMenuFunction()
        {
            Name = "Volver al Menu";
        }
    }
}