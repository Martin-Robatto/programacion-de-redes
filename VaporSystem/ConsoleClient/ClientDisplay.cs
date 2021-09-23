using System;
using ConsoleClient.Function;

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
        
        public static void LoginMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            var functions = FunctionDictionary.NoRequiresCredentials();
            foreach (var function in functions)
            {
                Console.WriteLine($"{function.Key}) {function.Value.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void MainMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            var functions = FunctionDictionary.RequiresCredentials();
            foreach (var function in functions)
            {
                Console.WriteLine($"{function.Key}) {function.Value.Name}");
            }
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