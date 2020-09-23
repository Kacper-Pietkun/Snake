using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Wall : ICollider, IPrintable
    {
        public static int WallWidth = 6;
        public static int WallHeight = 4;
        public char MyCharacter { get; set; }

        public Wall(char character)
        {
            MyCharacter = character;
        }

        public bool Collision(Snake snake)
        {
            return ConstBools.GameOver;
        }

        public void RefreshOnScreen(int mapOffsetX, int mapOffsetY)
        {
            throw new NotImplementedException();
        }
    }
}
