using System;

namespace ConsoleServer
{
    public class ServerDisplay
    {
        public static void ClearConsole()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine();
            }
        }
        
        public static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine();
            Console.WriteLine("Ingrese su opcion: ");
        }
        
        public static void Starting(string serverIpAddress, string serverPort)
        {
            Console.WriteLine($"El servidor inicializó en IP {serverIpAddress} y puerto {serverPort}");
        }
        
        public static void Closing()
        {
            Console.WriteLine("El servidor se está cerrando ");
        }
        
        public static void Listening()
        {
            Console.WriteLine("El servidor comenzó a escuchar conexiones");
        }
        
        public static void NewConnection()
        {
            Console.WriteLine($" > Nueva conexión aceptada");
        }
        
        public static void ConnectionInterrupted()
        {
            Console.WriteLine(" > Se cerró la conexión del cliente");
        }
        
        public static void InvalidOption()
        {
            Console.WriteLine("Opción inválida");
        }
    }
}