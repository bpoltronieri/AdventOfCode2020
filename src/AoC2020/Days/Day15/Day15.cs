using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day15 : IDay
    {
        private int[] input;

        public Day15(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           input = File.ReadAllLines(file)[0].Split(',').Select(c => int.Parse(c)).ToArray();
        }     

        public string PartOne()
        {
            return PlayNTurns(2020).ToString();
        }

        public string PartTwo()
        {
            return PlayNTurns(30000000).ToString();
        }

        private int PlayNTurns(int n)
        {
            var turn = 0;
            var numberToTurn = new Dictionary<int, int>();
            foreach (var i in input)
                numberToTurn[i] = turn++;

            var lastNumber = input.Last();
            var lastNumberWasNew = true;
            var timeSinceLastSpoken = -1; //only applies if !lastNumberWasNew

            while (turn < n)
            {
                var nextNumber = 0;
                if (!lastNumberWasNew) nextNumber = timeSinceLastSpoken;
                
                if (numberToTurn.ContainsKey(nextNumber)) // not 1st time speaking nextNumber
                {
                    lastNumberWasNew = false;
                    timeSinceLastSpoken = turn - numberToTurn[nextNumber];
                }
                else lastNumberWasNew = true;

                numberToTurn[nextNumber] = turn++;
                lastNumber = nextNumber;
            }
            return lastNumber;

        }
    }
}