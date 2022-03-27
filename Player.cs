using System;

// ☺ man

namespace ASCIIban
{
    enum Direction
    {
        North = 0, East = 2, South = 3, West = 4
    }

    internal class BoxPushEventArgs
    {
        public BoxPushEventArgs(Direction dir) { this.dir = dir; }
        public Direction dir { get; }
    }

    internal class Player
    {
        public delegate void BoxPushEventHandler(object sender, BoxPushEventArgs e); // some event stuff

        private Level level; // in case collision breaks after adding in box moving, try adding a pointer here and see if it fixes anything

        public event BoxPushEventHandler BoxPushed;

        public int X;
        public int Y;

        public Player(Level level)
        {
            this.level = level;
            X = this.level.startX;
            Y = this.level.startY;
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    if (level.level[Y - 1, X] == Tile.Box) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[Y - 1, X] == Tile.BoxDropoff || level.level[Y - 1, X] == Tile.Floor) Y--;
                    break;

                case Direction.East:
                    if (level.level[Y, X + 1] == Tile.Box) BoxPushed?.Invoke(this, new(dir));
                    if (level.level[Y, X + 1] == Tile.BoxDropoff || level.level[Y, X + 1] == Tile.Floor) X++;
                    break;

                case Direction.South:
                    if (level.level[Y + 1, X] == Tile.Box) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[Y + 1, X] == Tile.BoxDropoff || level.level[Y + 1, X] == Tile.Floor) Y++;
                    break;

                case Direction.West:
                    if (level.level[Y, X - 1] == Tile.Box) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[Y, X - 1] == Tile.BoxDropoff || level.level[Y, X - 1] == Tile.Floor) X--;
                    break;

                default:
                    Console.WriteLine("WAT");
                    break;
            }
        }
    }
}
