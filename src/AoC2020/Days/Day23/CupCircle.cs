using System.Collections.Generic;

namespace AoC2020.Days.Day23Utils
{
    internal class CupCircle
    {
        public int Count { get; }

        private Dictionary<int, Cup> LabelsToCups;

        public CupCircle(int[] cupLabels)
        {
            var firstCup = new Cup(cupLabels[0]);
            LabelsToCups = new Dictionary<int, Cup>();
            LabelsToCups.Add(firstCup.Label, firstCup);

            var previousCup = firstCup;
            for (var i = 1; i < cupLabels.Length; i++)
            {
                var cup = new Cup(cupLabels[i]);
                previousCup.Next = cup;
                previousCup = cup;

                LabelsToCups.Add(cup.Label, cup);
            }
            previousCup.Next = firstCup;
        }

        public Cup CupWithLabel(int label)
        {
            if (LabelsToCups.ContainsKey(label))
                return LabelsToCups[label];
            else
                return null;
        }

        // Returns pointer to chain of three cups that were removed from the cup
        // circle, starting from the cup that was after the given cup.
        public Cup PickUpNextThreeCups(Cup cup)
        {
            var nextCup = cup.Next;

            var lastCup = cup;
            for (var i = 0; i < 3; i++) 
                lastCup = lastCup.Next;

            cup.Next = lastCup.Next;
            lastCup.Next = null;

            return nextCup;
        }
        
        public void PlaceCupsAfter(Cup newCups, Cup cup)
        {
            var nextCup = cup.Next;
            var lastCup = newCups;
            while (lastCup.Next != null) 
                lastCup = lastCup.Next;

            cup.Next = newCups;
            lastCup.Next = nextCup;
        }
    }
}