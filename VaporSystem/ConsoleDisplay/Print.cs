using System;

namespace ConsoleDisplay
{
    public abstract class Print
    {
        public static void MainServerMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine("");
            Console.WriteLine("Ingrese su opcion: ");
        }
        
        public static void LoginMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1) Iniciar sesion");
            Console.WriteLine("2) Registrarse");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine("");
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void MainClientMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1) Catalogo");
            Console.WriteLine("2) Adquirir");
            Console.WriteLine("3) Calificar");
            Console.WriteLine("4) Gestionar");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine("");
            Console.WriteLine("Ingrese su opcion: ");
        }

        public static void GameManagementMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR - Gestionar un juego");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1) Publicar");
            Console.WriteLine("2) Modificar");
            Console.WriteLine("3) Eliminar");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Volver");
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