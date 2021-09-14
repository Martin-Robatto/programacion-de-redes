using System;
using ConsoleDisplay;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClientHandler.Exit = false;
                ClientHandler.InitializeSocket();
                ClientHandler.Connection();
                while (!ClientHandler.Exit)
                {
                    Print.MainClientMenu();
                    var userInput = Console.ReadLine();
                    if (userInput.Equals("exit"))
                    {
                        ClientHandler.ShutDown();
                    }
                    else
                    {
                        ClientHandler.Execute(userInput);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ClientHandler.ShutDown();
            }
        }
    }
}