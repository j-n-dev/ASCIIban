using System;
using System.IO;

namespace ASCIIban
{
    internal enum Tile
    {
        Wall = '#', Floor = '.', PlayerSpawn, Box = 'O', BoxDropoff = '&', BoxDroppedoff = '%'
    }

    internal class Level
    {
        public void HandleBoxPush(object sender, BoxPushEventArgs e)
        {
            Player player = (Player)sender;

            int BoxX = player.X;
            int BoxY = player.Y;

            switch (e.Dir)
            {
                case Direction.North:
                    BoxY--;
                    break;

                case Direction.East:
                    BoxX++;
                    break;

                case Direction.South:
                    BoxY++;
                    break;

                case Direction.West:
                    BoxX--;
                    break;

                default:
                    break;
                    
            }

            int PossibleDropoffX = BoxX;
            int PossibleDropoffY = BoxY;

            switch (e.Dir)
            {
                case Direction.North:
                    PossibleDropoffY--;
                    break;

                case Direction.East:
                    PossibleDropoffX++;
                    break;

                case Direction.South:
                    PossibleDropoffY++;
                    break;

                case Direction.West:
                    PossibleDropoffX--;
                    break;
                    
                default:
                    break;

            }

            if ((level[PossibleDropoffX, PossibleDropoffY] == Tile.Floor))
            {
                if (level[BoxX, BoxY] == Tile.BoxDroppedoff) currentBoxes--;
                level[BoxX, BoxY] = level[BoxX, BoxY] == Tile.BoxDroppedoff ? Tile.BoxDropoff : Tile.Floor;
                level[PossibleDropoffX, PossibleDropoffY] = Tile.Box;
            }
            else if ((level[PossibleDropoffX, PossibleDropoffY] == Tile.BoxDropoff))
            {
                if (level[BoxX, BoxY] == Tile.BoxDroppedoff) currentBoxes--;
                level[BoxX, BoxY] = level[BoxX, BoxY] == Tile.BoxDroppedoff ? Tile.BoxDropoff : Tile.Floor;
                level[PossibleDropoffX, PossibleDropoffY] = Tile.BoxDroppedoff;
                currentBoxes++;
            }

            return;
        }

        public Level(string filePath)
        {
            string[] txtlevel = File.ReadAllLines(filePath);
            level = new Tile[txtlevel[0].Length, txtlevel.Length];

            for (int x = 0; x < txtlevel.Length; x++)
            {
                for (int y = 0; y < txtlevel[x].Length; y++)
                {
                    switch (txtlevel[x][y])
                    {
                        case '#':
                            level[y, x] = Tile.Wall;
                            break;

                        case ' ':
                            level[y, x] = Tile.Floor;
                            break;

                        case '@':
                            startY = y; startX = x;
                            level[y, x] = Tile.Floor;
                            break;
                            
                        case 'O':
                            level[y, x] = Tile.Box;
                            requiredBoxes++;
                            break;

                        case 'D':
                            level[y, x] = Tile.BoxDropoff;
                            break;

                        case 'R':
                            level[y, x] = Tile.BoxDroppedoff;
                            break;
                    }
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < level.GetLength(1); y++)
            {
                for (int x = 0; x < level.GetLength(0); x++)
                {
                    ConsoleColor oldCol = Console.ForegroundColor;
                    switch (level[x, y])
                    {
                        case Tile.Floor:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;

                        case Tile.Wall:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;

                        case Tile.Box:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;

                        case Tile.BoxDropoff:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case Tile.BoxDroppedoff:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        default:
                            break;
                    }
                    Console.Write((char)level[x, y]);
                    Console.ForegroundColor = oldCol;
                }
                Console.WriteLine();
            }
        }

        // public void Update(Player player)

        public readonly Tile[,] level;

        public readonly int startX;
        public readonly int startY;

        public readonly int requiredBoxes;
        public int          currentBoxes;
    }
}
