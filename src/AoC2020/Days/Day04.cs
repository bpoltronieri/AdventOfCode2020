using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day04 : IDay
    {
        private string[] input;

        private readonly static IReadOnlyList<string> requiredFields = new List<string> {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        public Day04(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        private void AddLineToPassport(string line, Dictionary<string,string> passport)
        {
            foreach (var entry in line.Split(' '))
                {
                var fieldAndValue = entry.Split(':');
                passport.Add(fieldAndValue[0], fieldAndValue[1]);
                }
        }

        public string PartOne()
        {
            var nValidPassports = 0;

            var passport = new Dictionary<string,string>();
            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    if (PassportHasAllFields(passport))
                        nValidPassports += 1;
                    passport.Clear();
                }
                else
                    AddLineToPassport(line, passport);
            }
            
            if (PassportHasAllFields(passport)) //final passport which doesn't end with new line in input file.
                nValidPassports += 1;

            return nValidPassports.ToString();
        }

        private bool PassportHasAllFields(Dictionary<string,string> passport)
        {
            foreach (var field in requiredFields)
            {
                if (!passport.ContainsKey(field))
                    return false;
            }
            return true;
        }

        public string PartTwo()
        {
            var nValidPassports = 0;

            var passport = new Dictionary<string,string>();
            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                if (PassportHasAllValidFields(passport))
                    nValidPassports += 1;
                passport.Clear();
                }
                else
                    AddLineToPassport(line, passport);          
            }
            if (PassportHasAllValidFields(passport)) //final passport which doesn't end with new line in input file.
                nValidPassports += 1;

            return nValidPassports.ToString();
        }

        private bool PassportHasAllValidFields(Dictionary<string,string> passport)
        {
            foreach (var field in requiredFields)
            {
                if (!passport.ContainsKey(field) || !ValidFieldValue(field, passport[field]))
                    return false;                    
            }
            return true;
        }

        private bool ValidFieldValue(string field, string value)
        {
            bool valid = true;
            switch (field)
            {
                case "byr":
                    valid = value.Length == 4 && int.Parse(value) >= 1920 && int.Parse(value) <= 2002;
                    break;
                case "iyr":
                    valid = value.Length == 4 && int.Parse(value) >= 2010 && int.Parse(value) <= 2020;
                    break;
                case "eyr":
                    valid = value.Length == 4 && int.Parse(value) >= 2020 && int.Parse(value) <= 2030;
                    break;
                case "hgt":
                    valid = value.Length > 2;
                    if (valid && value.Substring(value.Length-2).Equals("cm"))
                    {
                        var height = int.Parse(value.Substring(0, value.Length-2));
                        valid = height >= 150 && height <= 193;
                    }
                    else if (valid && value.Substring(value.Length-2).Equals("in"))
                    {
                        var height = int.Parse(value.Substring(0, value.Length-2));
                        valid = height >= 59 && height <= 76;
                    }
                    else valid = false;
                    break;
                case "hcl":
                    valid = value.Length == 7 && value[0].Equals('#') && value.Substring(1).All(char.IsLetterOrDigit);
                    break;
                case "ecl":
                    var EyeColours = new HashSet<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                    valid = EyeColours.Contains(value);
                    break;
                case "pid":
                    valid = value.Length == 9 && value.All(char.IsDigit);
                    break;
                default:
                    valid = true;
                    break;
            }
            return valid;
        }
    }
}