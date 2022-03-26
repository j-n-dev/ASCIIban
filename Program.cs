using System;

namespace ASCIIban
{
    class Program
    {
        public static void Main()
        {
            Level lvl = new Level("./Swap.txt");
            lvl.Draw();

            Console.Write("X: " + lvl.startX + " Y: " + lvl.startY);
        }
    }
}