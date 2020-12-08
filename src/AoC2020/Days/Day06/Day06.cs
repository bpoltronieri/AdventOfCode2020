using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day06 : IDay
    {
        private string[] input;

        public Day06(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }     

        public string PartOne()
        {
            var total = 0;
            var currentQuestions = new HashSet<char>();
            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    total += currentQuestions.Count;
                    currentQuestions.Clear();
                }
                else
                {
                    foreach (var question in line) currentQuestions.Add(question);
                }
            }
            total += currentQuestions.Count; // last group in file not followed by a new line.
            return total.ToString();
        }

        public string PartTwo()
        {
            var total = 0;
            var currentAnswers = new HashSet<char>();
            var firstPerson = true;
            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    total += currentAnswers.Count;
                    currentAnswers.Clear();
                    firstPerson = true;
                }
                else if (firstPerson)
                {
                    currentAnswers = line.ToHashSet<char>();
                    firstPerson = false;
                }
                else
                {
                    var newAnswers = line.ToHashSet<char>();
                    currentAnswers.IntersectWith(newAnswers);
                } 
            }
            total += currentAnswers.Count;
            return total.ToString(); // last group in file not followed by a new line.
        }
    }
}