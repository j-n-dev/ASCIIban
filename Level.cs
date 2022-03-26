using System;
using System.IO;

namespace ASCIIban
{
    internal enum Tile
    {
        Wall, Floor, PlayerSpawn, Box, BoxDropoff, BoxDroppedoff
    }

    internal class Level
    {
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
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    char tdraw;
                    switch (level[x, y])
                    {
                        case Tile.Wall:
                            tdraw = '#'; break;

                        case Tile.Floor:
                            tdraw = '.'; break;

                        case Tile.Box:
                            tdraw = 'O'; break;

                        case Tile.BoxDropoff:
                            tdraw = ';'; break;

                        case Tile.BoxDroppedoff:
                            tdraw = '%'; break;

                        default:
                            tdraw = ' '; break;
                    }
                    Console.Write(level[x, y]); // this only outputs "Wall" and "Floor" tiles, confirming that something is broken
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
