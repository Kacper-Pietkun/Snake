using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConsoleSnake
{
    class Snake : IPrintable
    {
        public Directions MyDirection { get; set; }
        public char MyCharacter { get; set; }

        public Snake(GameManager gameManager, Position headPosition, char character)
        {
            myGameManager = gameManager;
            snakePositions = new List<Position>();
            snakePositions.Add(headPosition);
            MyCharacter = character;
        }

        public class SnakePart : ICollider
        {
            public bool Collision(Snake snake)
            {
                return ConstBools.GameOver;
            }
        }

        public bool Move()
        {
            Position newSnakeHeadPosition = new Position(snakePositions[0]);
            snakeTailOldPosition = new Position(snakePositions[snakePositions.Count - 1]);

            UpdateNewSnakePosition(newSnakeHeadPosition);
            if(newSnakeHeadPosition.Equals(snakePositions[0])) // Snake hasn't moved
                return ConstBools.GameNotOver;
            WrapUpSnakePosition(newSnakeHeadPosition); // Wrapping if player wwent outside of the map
            ICollider collisionObject = myGameManager.GetColliderAtPosition(newSnakeHeadPosition);

            myGameManager.ChangeColliderPosition(snakePositions[snakePositions.Count - 1], newSnakeHeadPosition);
            snakePositions.Insert(0, newSnakeHeadPosition);
            snakePositions.RemoveAt(snakePositions.Count - 1);

            if (collisionObject != null && collisionObject.Collision(this) == ConstBools.GameOver)
            {
                numberofSnakeParts = 1;
                return ConstBools.GameOver;
            }

            return ConstBools.GameNotOver;
        }

        public void AddSnakePart()
        {
            SnakePart snakePart = new SnakePart();
            snakePositions.Add(snakeTailOldPosition);
            myGameManager.AddColliderToGame(snakePart, snakeTailOldPosition);
            wasSnakePartAdded = true;
            numberofSnakeParts++;
        }

        public void RefreshOnScreen(int mapOffsetX, int mapOffsetY)
        {
            Console.SetCursorPosition(mapOffsetX + snakePositions[0].PosX, mapOffsetY + snakePositions[0].PosY);
            Console.Write(MyCharacter);

            //if (numberofSnakeParts > 1)
            //{
            //    Console.SetCursorPosition(mapOffsetX + snakePositions[1].PosX, mapOffsetY + snakePositions[1].PosY);
            //    Console.Write(ConsoleCharacters.SnakeHead);
            //}

            if (!wasSnakePartAdded && MyDirection != Directions.Idle)
            {
                Console.SetCursorPosition(mapOffsetX + snakeTailOldPosition.PosX, mapOffsetY + snakeTailOldPosition.PosY);
                Console.Write(ConsoleCharacters.Background);
            }

            wasSnakePartAdded = false;
        }

        private List<Position> snakePositions;
        private Position snakeTailOldPosition;
        private static int numberofSnakeParts = 1;
        private bool wasSnakePartAdded;
        private GameManager myGameManager;

        private void UpdateNewSnakePosition(Position position)
        {
            switch (MyDirection)
            {
                case Directions.Up:
                    position.PosY--;
                    break;
                case Directions.Left:
                    position.PosX--;
                    break;
                case Directions.Down:
                    position.PosY++;
                    break;
                case Directions.Right:
                    position.PosX++;
                    break;
                case Directions.Idle:
                default:
                    break;
            }
        }

        // If new position of snake is out of the border then we make sure that this position will appear at the other side of the map
        private void WrapUpSnakePosition(Position position)
        {
            if (position.PosX < 0) position.PosX += myGameManager.sizeX;
            if (position.PosY < 0) position.PosY += myGameManager.sizeY;
            if (position.PosX >= myGameManager.sizeX) position.PosX -= myGameManager.sizeX;
            if (position.PosY >= myGameManager.sizeY) position.PosY -= myGameManager.sizeY;
        }
    }
}
