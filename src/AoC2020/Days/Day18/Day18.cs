using System;
using System.IO;

namespace AoC2020.Days
{
    public class Day18 : IDay
    {
        private string[] input;

        public Day18(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            Int64 res = 0;
            foreach (var line in input)
                res += EvaluateLine1(line);
            return res.ToString();
        }

        private Int64 EvaluateLine1(string line)
        {
            var firstNumberAndIndex = GetFirstNumber(line);
            Int64 res = firstNumberAndIndex[0];

            var i = Convert.ToInt32(firstNumberAndIndex[1]);
            while (i < line.Length)
            {
                var op = line[i];
                i += 2;
                var nextNumberAndIndex = GetFirstNumber(line.Substring(i));
                res = ApplyOp(op, res, nextNumberAndIndex[0]);
                i += Convert.ToInt32(nextNumberAndIndex[1]);
            }                      
            return res;
        }

        private Int64 ApplyOp(char op, Int64 a, Int64 b)
        {
            switch (op)
            {
                case '*':
                    return a * b;
                case '+':
                    return a + b;
            }
            throw new System.NotImplementedException();
        }

        // Finds first number in the line and return the index to carry on from
        private Int64[] GetFirstNumber(string line)
        {
            var firstWord = line.Split()[0];
            var first = firstWord[0];
            if (first == '(')
            {
                var closingBracketI = FindClosingBracketIndex(line);
                return new Int64[] {EvaluateLine1(line.Substring(1, closingBracketI - 1)), closingBracketI + 2};
            }
            else 
            {
                var index = -1;
                if (line.Length == 1) index = 1;
                else index = line.IndexOf(' ') + 1;
                return new Int64[] {Int64.Parse(firstWord), index};
            }
        }

        private int FindClosingBracketIndex(string line)
        {
            var nOpen = 0;
            var nClosed = 0;
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '(') nOpen += 1;
                else if (line[i] == ')') nClosed += 1;
                if (nOpen == nClosed) return i;
            }
            throw new InvalidDataException();
        }

        public string PartTwo()
        {
            Int64 res = 0;
            foreach (var line in input)
                res += EvaluateLine2(line);
            return res.ToString();
        }

        private Int64 EvaluateLine2(string line)
        {
            var firstNumberAndIndex = GetFirstNumber2(line);
            Int64 res = firstNumberAndIndex[0];

            var i = Convert.ToInt32(firstNumberAndIndex[1]);
            while (i < line.Length)
            {
                var op = line[i];
                i += 2;
                var nextNumberAndIndex = GetFirstNumber2(line.Substring(i));

                if (op == '+')
                {
                    res = ApplyOp(op, res, nextNumberAndIndex[0]);
                    i += Convert.ToInt32(nextNumberAndIndex[1]);
                }
                else if (op == '*')
                {
                    var nextTimesIndex = GetFirstTimesOutsideBrackets(line.Substring(i));
                    if (nextTimesIndex == -1)
                        nextTimesIndex = line.Substring(i).Length + 1;
                    
                    var newNextNumber = EvaluateLine2(line.Substring(i, nextTimesIndex - 1));
                    res = ApplyOp(op, res, newNextNumber);
                    i += nextTimesIndex;
                }
                else throw new InvalidOperationException();
                 
            }                      
            return res;
        }

        private Int64[] GetFirstNumber2(string line)
        {
            var firstWord = line.Split()[0];
            var first = firstWord[0];
            if (first == '(')
            {
                var closingBracketI = FindClosingBracketIndex(line);
                return new Int64[] {EvaluateLine2(line.Substring(1, closingBracketI - 1)), closingBracketI + 2};
            }
            else 
            {
                var index = -1;
                if (line.Length == 1) index = 1;
                else index = line.IndexOf(' ') + 1;
                return new Int64[] {Int64.Parse(firstWord), index};
            }
        }

        private int GetFirstTimesOutsideBrackets(string v)
        {
            var i = 0;
            while (i < v.Length)
            {
                if (v[i] == '*') return i;
                if (v[i] == '(')
                    i += FindClosingBracketIndex(v.Substring(i));
                i += 1;
            }
            return -1;
        }
    }
}