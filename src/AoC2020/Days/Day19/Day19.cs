using System;
using System.IO;
using System.Collections.Generic;
using AoC2020.Days.Day19Utils;
using System.Linq;

namespace AoC2020.Days
{
    public class Day19 : IDay
    {
        private Dictionary<int, Rule> Rules;
        private List<string> Messages;
        private Dictionary<int, HashSet<string>> Cache;

        public Day19(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            string[] input = File.ReadAllLines(file);
            Rules = new Dictionary<int, Rule>();
            Messages = new List<string>();

            var separatingLineIndex = Array.FindIndex(input, x => x.Length == 0);

            for (var i = 0; i < separatingLineIndex; i++)
            {
                var IDandRule = input[i].Split(": ");
                var ruleID = int.Parse(IDandRule[0]);
                Rule rule;

                if (IDandRule[1][0] == '"')
                    rule = new Rule(IDandRule[1][1]);
                else
                {
                    var subrules = IDandRule[1]
                        .Split(" | ")
                        .Select(s => s.Split()
                                      .Select(x => int.Parse(x))
                                      .ToList())
                        .ToList();    
                    rule = new Rule(subrules);
                }
                Rules.Add(ruleID, rule);
            }

            for (var i = separatingLineIndex + 1; i < input.Length; i++)
                Messages.Add(input[i]);
        }

        public string PartOne()
        {
            Cache = new Dictionary<int, HashSet<string>>();
            var possibleMessages = GeneratePossibleStrings(0, null, 0);
            return Messages.Count(m => possibleMessages.Contains(m)).ToString();
        }

        // Generates possible strings using the given rule ID.
        private HashSet<string> GeneratePossibleStrings(int ruleID, int? maxLength, int minStartIndex)
        {
            if (Cache.ContainsKey(ruleID))
                return Cache[ruleID].Where(x => maxLength == null || x.Length <= maxLength)
                                    .ToHashSet();

            var possibleStrings = new HashSet<string>();
            if (maxLength != null && maxLength == 0) 
                return possibleStrings; // can this happen?

            var rule = Rules[ruleID];
            if (rule.IsCharRule())
                {
                possibleStrings.Add(rule.Letter.ToString());
                return possibleStrings;
                }

            foreach (var subrules in rule.SubRules())
            {
                if (maxLength != null && subrules.Count > maxLength) // will get at least one char per rule
                    continue; 
                var ok = true;
                var stringsSoFar = new List<string>();
                stringsSoFar.Add("");
                var smallestLengthSoFar = 0;

                var i = 0;
                foreach (var subrule in subrules)
                {
                    i += 1;
                    var newMaxLength = maxLength == null ? null : maxLength - smallestLengthSoFar - (subrules.Count - i);

                    var subStrings = GeneratePossibleStrings(subrule, newMaxLength, minStartIndex + smallestLengthSoFar);
                    if (subStrings.Count == 0)
                    {
                        ok = false;
                        break;
                    }

                    var newStringsSoFar = new List<string>();
                    foreach (var s in stringsSoFar)
                        newStringsSoFar.AddRange(subStrings.Where(x => maxLength == null || x.Length <= maxLength - s.Length)
                                                           .Select(x => s + x));
                    
                    stringsSoFar = newStringsSoFar;
                    smallestLengthSoFar = stringsSoFar.Min(s => s.Length);
                }
                if (ok) possibleStrings.UnionWith(stringsSoFar.ToHashSet());
            }

            FilterByMessages(possibleStrings, minStartIndex);

            if (maxLength == null) // if no maxLength we can cache as we go along as result is always the same
                Cache[ruleID] = possibleStrings;

            return possibleStrings;
        }

        // Removes from possibleStrings all strings not contained in any of our 
        // messages starting after the given minStartIndex in the message.
        private void FilterByMessages(HashSet<string> possibleStrings, int minStartIndex)
        {
            var messages = Messages.Where(m => m.Length > minStartIndex);
            possibleStrings.RemoveWhere(s => !messages.Any(m => m.Substring(minStartIndex).Contains(s)));
        }

        // WARNING: This is a bad solution. I stubbornly tried to use the same solution as in
        // part one to generate all possible strings and then check which messages are in the
        // set of all strings. Of course we can now have infinitely many strings so we have to
        // stop at strings of a certain length. This is very slow but with a few more optimisations
        // I got it to finish running overnight. Takes somewhere between 2 and 6 hours.
        public string PartTwo()
        {
            UpdateRules8And11();

            var maxLength = Messages.Max(s => s.Length);
            
            // Cache results of values that don't lead to infinite recursion.
            Cache = new Dictionary<int, HashSet<string>>();
            Rules.Keys
                .Where(r => r != 8 && r != 11 && r != 0)
                .ToList()
                .ForEach(r => Cache[r] = GeneratePossibleStrings(r, maxLength, 0));
            
            var possibleMessages = GeneratePossibleStrings(0, maxLength, 0);
            return Messages.Count(m => possibleMessages.Contains(m)).ToString();
        }
        private void UpdateRules8And11()
        {
            var subRules8_1 = new List<int>(new int[] {42});
            var subRules8_2 = new List<int>(new int[] {42, 8});
            var subrules8 = new List<List<int>>(new List<int>[] {subRules8_1, subRules8_2});
            var newRule8 = new Rule(subrules8);
            Rules[8] = newRule8;

            var subRules11_1 = new List<int>(new int[] {42, 31});
            var subRules11_2 = new List<int>(new int[] {42, 11, 31});
            var subrules11 = new List<List<int>>(new List<int>[] {subRules11_1, subRules11_2});
            var newRule11 = new Rule(subrules11);
            Rules[11] = newRule11;
        }
    }
}