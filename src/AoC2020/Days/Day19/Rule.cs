using System;
using System.Collections.Generic;

namespace AoC2020.Days.Day19Utils
{
    class Rule
    {
        public char Letter { get; }
        public List<int> Subrules1 { get; }
        public List<int> Subrules2 { get; }

        public Rule( char letter )
        {
            Letter = letter;
            Subrules1 = null;
            Subrules2 = null;
        }
        public Rule( List<List<int>> Subrules )
        {
            Letter = '\0';
            if (Subrules.Count == 1)
            {
                Subrules1 = Subrules[0];
                Subrules2 = null;
            }
            else if (Subrules.Count == 2)
            {
                Subrules1 = Subrules[0];
                Subrules2 = Subrules[1];
            }
            else
                throw new ArgumentException();
        }
        public IEnumerable<List<int>> SubRules()
        {
            if (Subrules1 != null) yield return Subrules1;
            if (Subrules2 != null) yield return Subrules2;
        }
        public bool IsCharRule()
        {
            return Letter != '\0';
        }
    }
}