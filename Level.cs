using System;
using System.IO;

namespace ASCIIban
{
    internal enum Tile
    {
        Wall = '#', Floor = '.', PlayerSpawn, Box = 'O', BoxDropoff = '&', BoxDroppedoff = '*'
    }

    internal class Level
    {
        public void HandleBoxPush(object sender, BoxPushEventArgs e)
        {
            Player player = (Player)sender;

            int BoxX = player.Y;
            int BoxY = player.X;

            switch (e.dir)
            {
                case Direction.North:
                    BoxX--;
                    break;

                case Direction.East:
                    BoxY++;
                    break;

                case Direction.South:
                    BoxX++;
                    break;

                case Direction.West:
                    BoxY--;
                    break;

                default:
                    break;
                    
            }

            int PossibleDropoffX = BoxX;
            int PossibleDropoffY = BoxY;

            switch (e.dir)
            {
                case Direction.North:
                    PossibleDropoffX--;
                    break;

                case Direction.East:
                    PossibleDropoffY++;
                    break;

                case Direction.South:
                    PossibleDropoffX++;
                    break;

                case Direction.West:
                    PossibleDropoffY--;
                    break;

                default:
                    break;

            }

            if ((level[PossibleDropoffX, PossibleDropoffY] == Tile.Floor))
            {
                level[BoxX, BoxY] = Tile.Floor;
                level[PossibleDropoffX, PossibleDropoffY] = Tile.Box;
            }

            return;
        }

        public Level(string filePath)
        {
            string[] txtlevel = File.ReadAllLines(filePath);
            level = new Tile[txtlevel[0].Length, txtlevel.Length];

            for (int y = 0; y < txtlevel.Length; y++)
            {
                for (int x = 0; x < txtlevel[y].Length; x++)
                {
                    switch (txtlevel[y][x])
                    {
                        case '#':
                            level[x, y] = Tile.Wall;
                            break;

                        case ' ':
                            level[x, y] = Tile.Floor;
                            break;

                        case '@':
                            startY = y; startX = x;
                            level[x, y] = Tile.Floor;
                            break;
                            
                        case 'O':
                            level[x, y] = Tile.Box;
                            break;

                        case 'D':
                            level[x, y] = Tile.BoxDropoff;
                            break;
                    }
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < level.GetLength(0); y++)
            {
                for (int x = 0; x < level.GetLength(1); x++)
                {
                    Console.Write((char)level[y, x]);
                }
                Console.WriteLine();
            }
        }

        // public void Update(Player player)

        public readonly Tile[,] level;

        public readonly int startX;
        public readonly int startY;
    }
}
