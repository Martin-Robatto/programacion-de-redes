using System;

namespace ConsoleServer
{
    public class ServerDisplay
    {
        public static void MainMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine("");
            Console.WriteLine("Ingrese su opcion: ");
        }
        public static void ClearConsole()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine();
            }
        }
    }
}