using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Xml.Schema;

namespace ConsoleSnake
{
    class GameManager
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        public GameManager(int sizeX, int sizeY, double timeDelay)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            mapOffsetX = Console.WindowWidth / 2 - sizeX / 2;
            mapOffsetY = Console.WindowHeight / 2 - sizeY / 2;
            spots = new ICollider[sizeY, sizeX];
            for (int i = 0; i < sizeY; i++)
                for (int j = 0; j < sizeX; j++)
                    spots[i, j] = null;

            this.timeDelay = timeDelay;
            isRoundDone = true;
        }

        public void ChangeColliderPosition(Position oldPosition, Position newPosition)
        {
            spots[newPosition.PosY, newPosition.PosX] = spots[oldPosition.PosY, oldPosition.PosX];
            spots[oldPosition.PosY, oldPosition.PosX] = null;
        }

        public void AddColliderToGame(ICollider obj, Position position)
        {
            spots[position.PosY, position.PosX] = obj;
        }

        public ICollider GetColliderAtPosition(Position position)
        {
            return spots[position.PosY, position.PosX];
        }

        public void InstantiateApple()
        {
            Position applePosition = GetRandomFreePosition();
            apple = new Apple(this, applePosition, ConsoleCharacters.Apple);
            AddColliderToGame(apple, applePosition);
        }

        public void AppleCollected()
        {
            InstantiateApple();
            apple.RefreshOnScreen(mapOffsetX, mapOffsetY);
        }

        public void SetFlagToGameOver()
        {
            timer.Enabled = false;
            isGameOver = true;
        }

        public void StartGame()
        {
            isGameOver = false;
            InstantiateGameObjects();
            SetTimerOn();
            DrawInitialMap();
            CatchPlayerInput();
        }

        private Timer timer;
        private Snake snake;
        private Apple apple;
        private double timeDelay;
        private int mapOffsetX;
        private int mapOffsetY;
        private bool isGameOver;
        private ICollider[,] spots;
        private bool isRoundDone;

        // Create Snake and one object to eat
        private void InstantiateGameObjects()
        {
            InstantiateWalls();
            InstantiateApple();
            InstantiateSnake();
        }

        private void InstantiateSnake()
        {
            Position snakePosition = GetRandomFreePosition();
            snake = new Snake(this, snakePosition, ConsoleCharacters.SnakeBody);
            snake.MyDirection = Directions.Idle;
            AddColliderToGame(new Snake.SnakePart(), snakePosition);
        }

        private void InstantiateWalls()
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if ((i == 0 || i == sizeY - 1) && (j < Wall.WallWidth || sizeX - j <= Wall.WallWidth) ||
                        (j == 0 || j == sizeX - 1) && (i < Wall.WallHeight|| sizeY - i <= Wall.WallHeight))
                        spots[i, j] = new Wall(ConsoleCharacters.Wall);
                }
            }
        }

        // timer.Elapsed delegate will trigger GameRoudn method every timeDelay miliseconds
        private void SetTimerOn()
        {
            timer = new Timer();
            ElapsedEventHandler everyRoundFunc = new ElapsedEventHandler(GameRound);
            timer.Elapsed += everyRoundFunc;
            timer.Interval = timeDelay;
            timer.Enabled = true;
        }

        // It is triggered every timeDelay miliseconds
        private void GameRound(object source, ElapsedEventArgs e)
        {
            if (!isRoundDone)
                return;
            isRoundDone = false;
            if (snake.Move() == ConstBools.GameOver)
            {
                SetFlagToGameOver();
                return;
            }
            snake.RefreshOnScreen(mapOffsetX, mapOffsetY);
            isRoundDone = true;
        }

        private void DrawInitialMap()
        {
            Console.Clear();
            for (int i = 0; i < sizeY; i++)
            {
                Console.SetCursorPosition(mapOffsetX, mapOffsetY + i);
                for (int j = 0; j < sizeX; j++)
                {
                    if(spots[i, j] is IPrintable)
                        Console.Write(((IPrintable)spots[i, j]).MyCharacter);
                    else
                        Console.Write(ConsoleCharacters.Background);
                }
                Console.Write(ConsoleCharacters.Enter);
            }
            Console.SetCursorPosition(0, 0);
        }

        private void CatchPlayerInput()
        {
            while (!isGameOver)
            {
                if (Console.KeyAvailable)
                {
                    switch(Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            snake.MyDirection = Directions.Up;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.MyDirection = Directions.Left;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.MyDirection = Directions.Down;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.MyDirection = Directions.Right;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Position GetRandomFreePosition()
        {

            Random random = new Random();
            Position newPosition = new Position();
            do
            {
                newPosition.PosX = random.Next(0, sizeX);
                newPosition.PosY = random.Next(0, sizeY);
            } 
            while (spots[newPosition.PosY, newPosition.PosX] != null);

            return newPosition;
        }
    }
}
