using System;

namespace ASCIIban
{
    internal static class ConUtil
    {
        public static void DrawCharacter(char ch, ConsoleColor col, int x, int y)
        {
            ConsoleColor oldCol = Console.ForegroundColor;
            (int oldX, int oldY) = Console.GetCursorPosition();

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = col;
            Console.Write(ch);


            Console.ForegroundColor = oldCol;
            Console.SetCursorPosition(oldX, oldY);
        }
    }
}
