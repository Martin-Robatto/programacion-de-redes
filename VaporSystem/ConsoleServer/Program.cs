using System;
using ConsoleDisplay;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerHandler.Exit = false;
                ServerHandler.InitializeSocket();
                Print.MainServerMenu();
                while (!ServerHandler.Exit)
                {
                    var userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "exit":
                            ServerHandler.ShutDown();
                            break;
                        default:
                            Console.WriteLine("Opción inválida");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ServerHandler.ShutDown();
            }
        }
    }
}