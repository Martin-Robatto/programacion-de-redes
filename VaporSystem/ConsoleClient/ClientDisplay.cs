using System;
using System.Collections.Generic;
using ConsoleClient.Function;
using Protocol;

namespace ConsoleClient
{
    public class ClientDisplay
    {
        public static void ClearConsole()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine();
            }
        }
        
        public static void LoginMenu(IList<string> optionsToDisplay)
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            var functions = FunctionDictionary.Get();
            foreach (var option in optionsToDisplay)
            {
                var key = Int32.Parse(option);
                Console.WriteLine($"{key}) {functions[key].Name}");
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine($"{FunctionConstants.EXIT}) {functions[FunctionConstants.EXIT].Name}");
            Console.WriteLine();
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void MainMenu(IList<string> optionsToDisplay)
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            var functions = FunctionDictionary.Get();
            foreach (var option in optionsToDisplay)
            {
                var key = Int32.Parse(option);
                Console.WriteLine($"{key}) {functions[key].Name}");
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine($"{FunctionConstants.EXIT}) {functions[FunctionConstants.EXIT].Name}");
            Console.WriteLine();
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void Starting(string clientIpAddress, int clientPort)
        {
            Console.WriteLine($"El cliente se inicializo en IP {clientIpAddress} y puerto {clientPort}");
        }

        public static void Connected()
        {
            Console.WriteLine("Conectado exitosamente al servidor");
        }
        
        public static void Continue()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para continuar");
            Console.ReadLine();
        }
        
        public static void Closing()
        {
            Console.WriteLine("El cliente se está cerrando ");
        }
    }
}