using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    static class ConsoleCharacters
    {
        public static char Background { get; } = '.';
        public static char SnakeBody { get; } = 'o';
        public static char SnakeHead { get; } = '0';
        public static char Bonus { get; } = '@';
        public static char Apple { get; } = '*';
        public static char Wall { get; } = '#';
        public static char Enter { get; } = '\n';
    }
}
