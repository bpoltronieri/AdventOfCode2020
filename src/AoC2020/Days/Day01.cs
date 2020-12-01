using System;
using System.IO;

namespace AoC2020.Days
{
    class Day01 : IDay
    {
        private string[] input;

        public Day01(string file)
            {
                LoadInput(file);
            }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            throw new NotImplementedException();
        }

        public string PartTwo()
        {
            throw new NotImplementedException();
        }
    }
}