using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = 0;
            while (day == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Enter day: ");
                var command = Console.ReadLine();

                if (!int.TryParse(command, out day) || day < 1 || day > 25)
                    {
                        day = 0;
                        Console.WriteLine("Invalid Day");
                    }
            }

            IDay solution;
            try
            {
                var dayName = $"Day{day:d2}";
                var typeName = "AoC2020.Days." + dayName;
                var dayType = Type.GetType(typeName);

                var inputFile = Directory.GetFiles(@"../../Input", dayName + ".txt")[0];

                solution = (IDay)Activator.CreateInstance(dayType, inputFile);
            }
            catch
            {
                Console.WriteLine("Error loading solution");
                return;
            }

            var part = 0;
            while (part == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Which part would you like to solve?");
                var command = Console.ReadLine();

                if (!int.TryParse(command, out part) || (part != 1 && part != 2))
                    {
                        part = 0;
                        Console.WriteLine("Invalid Part");
                    }
            }

            string answer = "";
            switch (part)
            {
                case 1:
                    answer = solution.PartOne();
                    break;
                case 2:
                    answer = solution.PartTwo();
                    break;
            }
            Console.WriteLine(answer);
        }
    }
}
