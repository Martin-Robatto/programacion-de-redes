using System;
using System.Collections.Generic;
using ConsoleClient.Function;
using FunctionInterface;

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
        
        public static void Menu(Dictionary<int, IClientFunction> functions)
        {
            Console.WriteLine();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            foreach (var function in functions)
            {
                Console.WriteLine($"{function.Key}) {function.Value.Name}");
            }
        }

        public static void ChooseOption()
        {
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
        }
        
        public static void Closing()
        {
            Console.WriteLine("El cliente se está cerrando ");
        }
        
        public static void ConnectionInterrupted()
        {
            Console.WriteLine("El servidor está fuera de servicio");
        }
        
        public static void Reconnecting()
        {
            Console.WriteLine("Intentando conectarse al servidor");
        }
    }
}