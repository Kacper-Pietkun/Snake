using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class MainMenu
    {
        public MainMenu(int consoleWidth, int consoleHeight)
        {
            this.consoleWidth = consoleWidth;
            this.consoleHeight = consoleHeight;
        }

        public void DrawMainMenu()
        {
            Console.Clear();
            DrawTitle();
            DrawMessage();
        }

        public void DrawGameOverMenu()
        {
            Console.Clear();
            DrawGameOver();
            DrawMessage();
        }

        private int consoleWidth;
        private int consoleHeight;

        private void DrawTitle()
        {
            string[] title =
            {
                @" _____             _        ",
                @"/  ___|           | |       ",
                @"\ `--. _ __   __ _| | _____ ",
                @" `--. \ '_ \ / _` | |/ / _ \",
                @"/\__/ / | | | (_| |   <  __/",
                @"\____/|_| |_|\__,_|_|\_\___|"
            };
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(consoleWidth / 2 - title[i].Length / 2, 5 + i);
                Console.WriteLine(title[i]);
            }
        }

        private void DrawGameOver()
        {
            string[] title =
            {
                @" _____                        _____                ",
                @"|  __ \                      |  _  |               ",
                @"| |  \/ __ _ _ __ ___   ___  | | | |_   _____ _ __ ",
                @"| | __ / _` | '_ ` _ \ / _ \ | | | \ \ / / _ \ '__|",
                @"| |_\ \ (_| | | | | | |  __/ \ \_/ /\ V /  __/ |   ",
                @" \____/\__,_|_| |_| |_|\___|  \___/  \_/ \___|_|   "
            };
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(consoleWidth / 2 - title[i].Length / 2, 5 + i);
                Console.WriteLine(title[i]);
            }
        }

        private void DrawMessage()
        {
            string title = "Press to Play";
            Console.SetCursorPosition(consoleWidth / 2 - title.Length / 2, consoleHeight / 2);
            Console.WriteLine(title);
            string[] difficultyMessage =
            {
                "1. Easy",
                "2. Medium",
                "3. Hard"
            };
            int averageWidth = 0;
            foreach (string msg in difficultyMessage)
                averageWidth += msg.Length;
            averageWidth /= difficultyMessage.Length;
            for (int i = 0; i < difficultyMessage.Length; i++)
            {
                Console.SetCursorPosition(consoleWidth / 2 - averageWidth / 2, consoleHeight / 2 + i + 2);
                Console.WriteLine(difficultyMessage[i]);
            }
            
        }
    }
}
