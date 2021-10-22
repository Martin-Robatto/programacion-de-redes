using FunctionInterface;
using System.Net.Sockets;

namespace ConsoleClient.Function.Menu
{
    public class GoToReviewsMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(Socket socket, string session = null)
        {
            ClientHandler.Instance.SetActualFunctions(FunctionDictionary.Reviews());
        }

        public GoToReviewsMenuFunction()
        {
            Name = "Reseñas";
        }
    }
}