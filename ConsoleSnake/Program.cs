using System;

namespace ConsoleSnake
{
    class Program
    {
        private static int windowHeight = 40;
        private static int windowWidth = 150;

        private static int mapHeigth = 12;
        private static int mapWidth = 18;

        private static double gameTimeDelay;

        static void Main(string[] args)
        {
            Program program = new Program();
            Console.Title = "Console Snake";
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;
            Console.CursorVisible = false;


            MainMenu menu = new MainMenu(windowWidth, windowHeight);
            menu.DrawMainMenu();
            while(true)
            {
                gameTimeDelay = program.ChooseDifficulty();
                GameManager gameManager = new GameManager(mapWidth, mapHeigth, gameTimeDelay);
                gameManager.StartGame();
                menu.DrawGameOverMenu();
            }
            
        }

        private int ChooseDifficulty()
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)              
                {
                    case ConsoleKey.D1:
                        return 500;
                    case ConsoleKey.D2:
                        return 300;
                    case ConsoleKey.D3:
                        return 100;
                    default:
                        break;
                }
            }
        }
    }
}
