using FunctionInterface;
using System;
using System.Collections.Generic;

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

        public static void Continue()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para continuar");
        }

        public static void ChooseOption()
        {
            Console.WriteLine();
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void Connecting()
        {
            Console.WriteLine("Intentando conectarse al servidor");
        }

        public static void Connected()
        {
            ClearConsole();
            Console.WriteLine("Conectado exitosamente al servidor");
        }

        public static void ConnectionInterrupted()
        {
            ClearConsole();
            Console.WriteLine("El servidor está fuera de servicio");
        }
    }
}