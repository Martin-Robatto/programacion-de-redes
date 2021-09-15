using System;
using ConsoleDisplay;
using Protocol;

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
                    ClientDisplay.MainMenu();
                    var userInput = Console.ReadLine();
                    ClientHandler.Execute(userInput);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ClientHandler.Execute(FunctionConstants.Exit.ToString());
            }
        }
    }
}