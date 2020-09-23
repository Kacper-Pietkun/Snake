using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    interface IPrintable
    {
        public char MyCharacter { get; set; }
        public void RefreshOnScreen(int mapOffsetX, int mapOffsetY);
    }
}
