using System;
namespace AoC2020.Days.Day23Utils
{
    internal struct HexTile
    {
        public int X { get; }
        public int Y { get; }
        
        public HexTile(int x, int y)
        {
            if (x % 2 != y % 2)
                throw new ArgumentException();
        
            X = x;
            Y = y;
        }
        public HexTile(HexTile startTile, string direction)
        {
            switch (direction)
            {
                case "e":
                    X = startTile.X + 2;
                    Y = startTile.Y;
                    break;
                case "se":
                    X = startTile.X + 1;
                    Y = startTile.Y - 1;
                    break;
                case "sw":
                    X = startTile.X - 1;
                    Y = startTile.Y - 1;
                    break;
                case "w":
                    X = startTile.X - 2;
                    Y = startTile.Y;
                    break;
                case "nw":
                    X = startTile.X - 1;
                    Y = startTile.Y + 1;
                    break;
                case "ne":
                    X = startTile.X + 1;
                    Y = startTile.Y + 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}