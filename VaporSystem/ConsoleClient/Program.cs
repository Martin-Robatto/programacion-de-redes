using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClientHandler.Instance.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}