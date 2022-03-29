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
        public BoxPushEventArgs(Direction dir) { this.Dir = dir; }
        public Direction Dir { get; }
    }

    internal class Player
    {
        public delegate void BoxPushEventHandler(object sender, BoxPushEventArgs e); // some event stuff

        private readonly Level level; // in case collision breaks after adding in box moving, try adding a pointer here and see if it fixes anything

        public event BoxPushEventHandler BoxPushed;

        public int Y;
        public int X;

        public Player(Level level)
        {
            this.level = level;
            Y = this.level.startX;
            X = this.level.startY;
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    if (level.level[X, Y - 1] == Tile.Box || level.level[X, Y - 1] == Tile.BoxDroppedoff) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[X, Y - 1] == Tile.BoxDropoff || level.level[X, Y - 1] == Tile.Floor) Y--;
                    break;

                case Direction.East:
                    if (level.level[X + 1, Y] == Tile.Box || level.level[X + 1, Y] == Tile.BoxDroppedoff) BoxPushed?.Invoke(this, new(dir));
                    if (level.level[X + 1, Y] == Tile.BoxDropoff || level.level[X + 1, Y] == Tile.Floor) X++;
                    break;

                case Direction.South:
                    if (level.level[X, Y + 1] == Tile.Box || level.level[X, Y + 1] == Tile.BoxDroppedoff) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[X, Y + 1] == Tile.BoxDropoff || level.level[X, Y + 1] == Tile.Floor) Y++;
                    break;

                case Direction.West:
                    if (level.level[X - 1, Y] == Tile.Box || level.level[X - 1, Y] == Tile.BoxDroppedoff) BoxPushed?.Invoke(this, new (dir));
                    if (level.level[X - 1, Y] == Tile.BoxDropoff || level.level[X - 1, Y] == Tile.Floor) X--;
                    break;

                default:
                    Console.WriteLine("WAT");
                    break;
            }
        }
    }
}
