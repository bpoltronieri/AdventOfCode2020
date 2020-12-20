using System.Collections.Generic;

namespace AoC2020.Days.Day08Utils
{
    class BootCodeRunner
    {
        public string[] BootCode { get; set; }
        public int Accumulator { get; set; }
        public int InstructionPointer { get; set; }

        public BootCodeRunner(string[] bootCode)
        {
            BootCode = bootCode;
            Accumulator = 0;
            InstructionPointer = 0;
        }

        // Runs bootcoe and returns true if terminated properly.
        // Avoids infinite loops.
        public bool Run()
        {
            var instructionsVisited = new HashSet<int>();
            while (!instructionsVisited.Contains(InstructionPointer) && InstructionPointer < BootCode.Length)
            {
                instructionsVisited.Add(InstructionPointer);
                var currentInstruction = BootCode[InstructionPointer];
                var operation = currentInstruction.Split()[0];
                var argument = int.Parse(currentInstruction.Split()[1]);
                RunInstruction(operation, argument);
            }
            return InstructionPointer == BootCode.Length; // condition for terminating normally
        }

        private void RunInstruction(string operation, int argument)
        {
            switch (operation)
            {
                case "acc":
                    Accumulator += argument;
                    InstructionPointer += 1;
                    break;
                case "jmp":
                    InstructionPointer += argument;
                    break;
                case "nop":
                default:
                    InstructionPointer += 1;
                    break;
            }
        }
    }
}