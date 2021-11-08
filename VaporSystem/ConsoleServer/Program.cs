using System;
using System.Threading.Tasks;

namespace ConsoleServer
{
    class Program
    {
        private static ServerHandler _serverHandler;
        
        static async Task Main(string[] args)
        {
            try
            {
                _serverHandler = new ServerHandler();
                _serverHandler.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}