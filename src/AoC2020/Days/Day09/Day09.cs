using System;
using System.IO;

namespace AoC2020.Days
{
    public class Day09 : IDay
    {
        private Int64[] input;
        private int PreambleLength = 25;

        public Day09(string file)
        {
            LoadInput(file);
        }

        // constructor for the test which needs a preamble length of 5:
        public Day09(string file, int preambleLength)
        {
            PreambleLength = preambleLength;
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           input = Array.ConvertAll(File.ReadAllLines(file), Int64.Parse);
        }     

        // Find the first number in the list (after the preamble) which is NOT
        // the sum of two of the 25 (preambleLength) numbers before it.
        public string PartOne()
        {
            for (var i = PreambleLength; i < input.Length; i++)
            {
                var currentNumber = input[i];
                var goodNumber = false;

                for (var j = 0; j < PreambleLength - 1 && !goodNumber; j++)
                    for (var k = j+1; k < PreambleLength && !goodNumber; k++)
                    {
                        var n1 = input[i - PreambleLength + j];
                        var n2 = input[i - PreambleLength + k];
                        goodNumber = (n1 + n2 == currentNumber);
                    }

                if (!goodNumber)
                    return currentNumber.ToString();
            }
            throw new InvalidDataException(); // didn't find any bad number
        }

        // Find a contiguous set of at least two numbers which sum to the invalid number from part one.
        // Returns the sum of the min and the max numbers from this set.
        public string PartTwo()
        {
            var invalidNumber = Int64.Parse(PartOne());

            for (var i = 0; i < input.Length; i++)
            {
                Int64 sum = 0;
                var min = Int64.MaxValue;
                var max = Int64.MinValue;
                var j = 0;
                while (sum < invalidNumber)
                {
                    var currentNumber = input[i+j];
                    sum += currentNumber;
                    if (currentNumber < min) min = currentNumber;
                    if (currentNumber > max) max = currentNumber;
                    j += 1;
                }
                if (sum == invalidNumber)
                    return (min+max).ToString();
            }
            throw new InvalidDataException();
        }
    }
}