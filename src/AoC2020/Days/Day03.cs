using System;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day03 : IDay
    {
        private string[] input;

        public Day03(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            var nTrees = 0;
            var xPosn = 0;
            foreach (var line in input)
            {
                var tile = line[xPosn];
                if (tile == '#')
                    nTrees += 1;

                xPosn += 3;
                xPosn %= line.Length;
            }
            return nTrees.ToString();
        }

        public string PartTwo()
        {
            var xPosns = new int[] {0, 0, 0, 0, 0};
            var xIncrements = new int[]  {1, 3, 5, 7, 1};

            var yPosns = new int[] {0, 0, 0, 0, 0};
            var yIncrements = new int[] {1, 1, 1, 1, 2};

            var nTrees = new int[] {0, 0, 0, 0, 0};

            while (yPosns.Min() < input.Length)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (yPosns[i] < input.Length)
                    {
                        if (input[yPosns[i]][xPosns[i]] == '#')
                            nTrees[i] += 1;

                        xPosns[i] += xIncrements[i];
                        xPosns[i] %= input[0].Length;

                        yPosns[i] += yIncrements[i];
                    }
                }
            }

            Int64 total = 1;
            foreach (var count in nTrees)
                total *= (Int64)count;

            return total.ToString();
        }
    }
}