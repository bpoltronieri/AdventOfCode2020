using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day02 : IDay
    {
        private string[] input;

        public Day02(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            var nValidPasswords = 0;
            foreach (var line in input)
            {
                var words = line.Split(' ');
                var minLetterCount = int.Parse(words[0].Split('-')[0]);
                var maxLetterCount = int.Parse(words[0].Split('-')[1]);
                var letter = words[1][0];
                var password = words[2];

                var count = password.Count(f => f == letter);

                if (count >= minLetterCount && count <= maxLetterCount)
                    nValidPasswords += 1;
            }
            return nValidPasswords.ToString();
        }

        public string PartTwo()
        {
            var nValidPasswords = 0;
            foreach (var line in input)
            {
                var words = line.Split(' ');
                var position1 = int.Parse(words[0].Split('-')[0]) - 1;
                var position2 = int.Parse(words[0].Split('-')[1]) - 1;
                var letter = words[1][0];
                var password = words[2];

                if ((password[position1] == letter) != (password[position2] == letter))
                    nValidPasswords += 1;
            }
            return nValidPasswords.ToString();
        }
    }
}