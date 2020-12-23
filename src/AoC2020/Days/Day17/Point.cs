using System.Collections.Generic;

namespace AoC2020.Days.Day17Utils
{
    // Point can be a 3 or 4 dimensional vector depending on how it was constructed.
    // This is important for the Neighbours method
    struct Point
    {
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 0;
            dimension = 3;
        }

        public Point(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            dimension = 4;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int W { get; }
        private int dimension;

        public IEnumerable<Point> Neighbours()
        {   
            var dirs = new int[] {-1, 0, 1};
            if (dimension == 3)
            {
                foreach (var d1 in dirs)
                foreach (var d2 in dirs)
                foreach (var d3 in dirs)
                {
                    if (d1 == 0 && d2 == 0 && d3 == 0) continue;
                    var neighbour = new Point(X + d1, Y + d2, Z + d3);
                    yield return neighbour;
                }
            }
            if (dimension == 4)
            {
                foreach (var d1 in dirs)
                foreach (var d2 in dirs)
                foreach (var d3 in dirs)
                foreach (var d4 in dirs)
                {
                    if (d1 == 0 && d2 == 0 && d3 == 0 && d4 == 0) continue;
                    var neighbour = new Point(X + d1, Y + d2, Z + d3, W + d4);
                    yield return neighbour;
                }
            }
        }
    }
}