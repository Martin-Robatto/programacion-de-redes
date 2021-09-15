using System;
using ConsoleClient.Function;

namespace ConsoleClient
{
    public class ClientDisplay
    {
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

        public static void MainMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            var dynamicOptions = FunctionDictionary.Get();
            foreach (var option in dynamicOptions)
            {
                Console.WriteLine($"{option.Key}) {option.Value.Name}");
            }
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