using System.IO;

namespace AoC2020.Days
{
    public class Day01 : IDay
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
            for (var i = 0; i < input.Length-1; i++)
            {
                var val1 = int.Parse(input[i]);
                for (var j = i+1; j < input.Length; j++)
                {
                    var val2 = int.Parse(input[j]);
                    if (val1 + val2 == 2020)
                        return (val1*val2).ToString();
                }
            }
            return "";
        }

        public string PartTwo()
        {
            for (var i = 0; i < input.Length-2; i++)
            {
                var val1 = int.Parse(input[i]);
                for (var j = i+1; j < input.Length-1; j++)
                {
                    var val2 = int.Parse(input[j]);
                    for (var k = j+1; k < input.Length; k++)
                        {
                        var val3 = int.Parse(input[k]);
                        if (val1 + val2 + val3 == 2020)
                            return (val1*val2*val3).ToString();
                        }
                }
            }
            return "";
        }
    }
}