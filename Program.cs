using System;

namespace ASCIIban
{
    class Program
    {
        public static void Init()
        {
            Console.Title = "ASCIIban!";
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("ASCIIban 0.9.0B\n2022 j-n-dev\nThis software is not protected by copyright. Go crazy!\n------------------------------------------------------"); // little greeting

            Playlist playlist;
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the filepath of the level set you want to play.");
                return;
            }
            else if (File.Exists(args[0]))
            {
                playlist = new(args[0]);
            }
            else
            {
                Console.WriteLine(args[0] + " is not a valid file!");
                return;
            }

            ConsoleColor ffc = Console.ForegroundColor;
            ConsoleColor fbc = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            int moves = 0; // level-wide
            int totalMoves = 0; // playlist-wide

            // initialize stuff
            Init();

            // level and player initialization
            Level lvl = new(playlist.ReadNext());

            Player player = new(lvl);
            player.BoxPushed += new Player.BoxPushEventHandler(lvl.HandleBoxPush);

            bool running = true;
            while (running)
            {
                reload:
                // rendering
                Console.Clear();
                lvl.Draw();
                ConUtil.DrawCharacter('@', ConsoleColor.Blue, player.X, player.Y);
                Console.WriteLine("Moves: " + moves + "    Total Moves: " + totalMoves);
                Console.WriteLine("Boxes: " + lvl.currentBoxes + '/' + lvl.requiredBoxes);

                // win game logic
                if (lvl.currentBoxes == lvl.requiredBoxes)
                {
                    totalMoves += moves;

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(5, 5);
                    Console.Write("YOU WIN!");
                    Console.SetCursorPosition(5, 6);
                    Console.Write("Moves: " + moves);
                    Console.ReadKey();

                    if (playlist.PlaylistWon)
                    {
                        // oo this is where the fun begins
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Clear();
                        Console.SetCursorPosition(10, 10);
                        Console.Write("YOU HAVE WON THIS LEVELSET!");
                        Console.SetCursorPosition(10, 11);
                        Console.Write("Total amount of moves used up: " + totalMoves);
                        Console.SetCursorPosition(10, 12);
                        Console.Write("Press any key to close...");
                        Console.ReadKey();

                        Console.BackgroundColor = fbc;
                        Console.ForegroundColor = ffc;
                        Console.Clear();
                        return;
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                    lvl = new(playlist.ReadNext());
                    player = new(lvl);
                    player.BoxPushed += new Player.BoxPushEventHandler(lvl.HandleBoxPush);
                    moves = 0;
                    goto reload;
                }

                moves++;
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

                    case ConsoleKey.R:
                        // reset
                        lvl = new(playlist.ReadCurrent());
                        player = new(lvl);
                        player.BoxPushed += new Player.BoxPushEventHandler(lvl.HandleBoxPush);
                        moves = 0;
                        break;

                    default:
                        moves--;
                        break;
                }
            }
        }
    }
}