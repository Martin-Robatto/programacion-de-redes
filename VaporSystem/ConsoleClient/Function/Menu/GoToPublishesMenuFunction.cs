using FunctionInterface;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleClient.Function.Menu
{
    public class GoToPublishesMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        private FunctionDictionary _functionDictionary = new FunctionDictionary();
        
        public Task ExecuteAsync(Socket socket, string session = null)
        {
            ClientHandler.Instance.SetActualFunctions(_functionDictionary.Publishes());
            return Task.CompletedTask;
        }

        public GoToPublishesMenuFunction()
        {
            Name = "Publicaciones";
        }
    }
}