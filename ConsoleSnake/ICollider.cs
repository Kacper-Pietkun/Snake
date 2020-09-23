using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    interface ICollider
    {
        bool Collision(Snake snake);
    }


}
