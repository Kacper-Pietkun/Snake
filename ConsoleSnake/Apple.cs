using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Apple : ICollider, IPrintable
    {
        public char MyCharacter { get; set; }

        public Apple(GameManager gameManager, Position position, char character)
        {
            myGameManager = gameManager;
            myPosition = position;
            MyCharacter = character;
        }

        public bool Collision(Snake snake)
        {
            snake.AddSnakePart();
            myGameManager.AppleCollected();
            return ConstBools.GameNotOver;
        }

        public void RefreshOnScreen(int mapOffsetX, int mapOffsetY)
        {
            Console.SetCursorPosition(mapOffsetX + myPosition.PosX, mapOffsetY + myPosition.PosY);
            Console.Write(MyCharacter);
        }

        private Position myPosition;
        private GameManager myGameManager;
    }
}
