using System.IO;
using AoC2020.Days.Day08Utils;

namespace AoC2020.Days
{
    public class Day08 : IDay
    {
        private string[] input;

        public Day08(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            var runner = new BootCodeRunner(input);
            runner.Run();
            return runner.Accumulator.ToString();
        }

        public string PartTwo()
        {
            // go through all instructions trying to change "nop" to "jmp" and vice versa until boot runner terminates properly.
            for (var i = 0; i < input.Length; i++)
            {
                var instruction = input[i].Split()[0];
                if (instruction == "acc") continue;
                var argument = input[i].Split()[1];

                if (instruction == "nop")
                    input[i] = "jmp " + argument;
                else if (instruction == "jmp")
                    input[i] = "nop " + argument;

                var runner = new BootCodeRunner(input);
                if (runner.Run()) // terminated successfully
                    return runner.Accumulator.ToString();

                input[i] = instruction + " " + argument; // put original instruction back
            }
            throw new InvalidDataException();       
        }
    }
}