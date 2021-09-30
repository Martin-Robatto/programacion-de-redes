using System;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerHandler.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}