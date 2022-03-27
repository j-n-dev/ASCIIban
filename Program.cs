using System;

namespace ASCIIban
{
    class Program
    {
        public static void Init()
        {
            Console.Title = "ASCIIban! - Swep";
        }

        public static void Main()
        {
            // initialize stuff
            Init();

            // level and player initialization
            Level lvl = new Level("./Swap.txt");

            Player player = new(lvl);
            player.BoxPushed += new Player.BoxPushEventHandler(lvl.HandleBoxPush);

            bool running = true;
            while (running)
            {
                // rendering
                Console.Clear();
                lvl.Draw();
                ConUtil.DrawCharacter('@', ConsoleColor.Blue, player.X, player.Y);

                // game updates
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("HALT!");
                        running = false;
                        break;

                    case ConsoleKey.UpArrow:
                        player.Move(Direction.North);
                        break;

                    case ConsoleKey.RightArrow:
                        player.Move(Direction.East);
                        break;

                    case ConsoleKey.DownArrow:
                        player.Move(Direction.South);
                        break;

                    case ConsoleKey.LeftArrow:
                        player.Move(Direction.West);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}