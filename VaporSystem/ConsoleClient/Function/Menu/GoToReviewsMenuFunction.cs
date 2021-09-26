using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleClient.Function.Menu
{
    public class GoToReviewsMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(NetworkStream stream, string session = null)
        {
            ClientHandler.SetActualFunctions(FunctionDictionary.Reviews());
        }

        public GoToReviewsMenuFunction()
        {
            Name = "Reseñas";
        }
    }
}