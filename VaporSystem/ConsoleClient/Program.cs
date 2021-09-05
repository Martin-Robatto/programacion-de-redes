using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string mainMenuOption = string.Empty;
                while (!mainMenuOption.Equals("0"))
                {
                    PrintMainMenu();
                    mainMenuOption = Console.ReadLine();
                    MainMenuRedirectTo(mainMenuOption);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        
        private static void PrintMainMenu()
        {
            ClearConsole();
            Console.WriteLine("VAPOR");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1) Ver catalogo");
            Console.WriteLine("2) Adquirir un juego");
            Console.WriteLine("3) Calificar un juego");
            Console.WriteLine("4) Gestionar un juego");
            Console.WriteLine("------------------------------");
            Console.WriteLine("0) Salir");
            Console.WriteLine("");
        }

        private static void PrintGameManagementMenu()
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
        }

        private static void ClearConsole()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine();
            }
        }
        
        private static void MainMenuRedirectTo(string mainMenuOption)
        {
            if (mainMenuOption.Equals("4"))
            {
                string gameManagementOption = string.Empty;
                while (!gameManagementOption.Equals("0"))
                {
                    PrintGameManagementMenu();
                    gameManagementOption = Console.ReadLine();
                    GameManagementRedirectTo(gameManagementOption);
                }
            }
        }
        
        private static void GameManagementRedirectTo(string gameManagementOption) { }
    }
}