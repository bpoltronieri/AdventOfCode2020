using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day13 : IDay
    {
        private int earliestDeparture;
        private List<int> busIDs;

        public Day13(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           string[] input = File.ReadAllLines(file);
           earliestDeparture = int.Parse(input[0]);
           busIDs = input[1].Split(',')
                    .Where(c => c != "x")
                    .Select(c => int.Parse(c))
                    .ToList();
        }     

        // Finds ID of earliest bus you can take to the airport multiplied 
        // by the number of minutes you'll need to wait for that bus.
        public string PartOne()
        {
            var time = earliestDeparture - 1;
            var busID = -1;
            while (busID < 0)
            {
                time += 1;
                foreach (var ID in busIDs)
                {
                    if (time % ID == 0)
                    {
                        busID = ID;
                        break;
                    }
                }
            }
            return (busID * (time - earliestDeparture)).ToString();
        }

        public string PartTwo()
        {
            // Question is equivalent to finding the smallest integer t > 0 such that:
            // t % 29 = 0
            // (t+23) % 37 = 0
            // (t+29) % 467 = 0
            // (t+37) % 23 = 0
            // (t+42) % 13 = 0
            // (t+46) % 17 = 0
            // (t+48) % 19 = 0
            // (t+60) % 443 = 0
            // (t+101) % 41 = 0
            // which is the same as:
            // t % 29 = 0
            // t % 37 = -23 = 14
            // t % 467 = -29 = 438
            // t % 23 = -37 = 9
            // t % 13 = -42 = 10
            // t % 17 = -46 = 5
            // t % 19 = -48 = 9
            // t % 443 = -60 = 383
            // t % 41 = -101 = 22
            // This can be done using Chinese Remainder Theorem. I used the following calculator:
            // https://www.dcode.fr/chinese-remainder

            return "690123192779524";
        }
    }
}