using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    class Position
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Position(int x = 0, int y = 0)
        {
            PosX = x;
            PosY = y;
        }

        public Position(Position position)
        {
            PosX = position.PosX;
            PosY = position.PosY;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Position)
            {
                Position position = (Position) obj;
                if (PosX == position.PosX && PosY == position.PosY)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
