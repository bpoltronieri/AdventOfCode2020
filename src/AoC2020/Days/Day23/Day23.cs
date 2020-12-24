using System;
using System.IO;
using System.Linq;
using AoC2020.Days.Day23Utils;

namespace AoC2020.Days
{
    public class Day23 : IDay
    {
        private int[] CupLabels;

        public Day23(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            CupLabels = File.ReadAllLines(file)[0]
                .Select(c => int.Parse(c.ToString())) // not sure why you can't just parse a char without making it a string
                .ToArray();
        }

        public string PartOne()
        {   
            var cupCircle = new CupCircle(CupLabels);
            PlayNMoves(cupCircle, 100, cupCircle.CupWithLabel(CupLabels[0]), CupLabels.Max());

            var cup = cupCircle.CupWithLabel(1);
            var labels = "";
            for (var i = 0; i < CupLabels.Length - 1; i++)
            {
                cup = cup.Next;
                labels += cup.Label.ToString();
            }
            return labels;
        }

        private void PlayNMoves(CupCircle cupCircle, int nMoves, Cup firstCup, int highestCupLabel)
        {
            var cup = firstCup;
            for (var move = 0; move < nMoves; move++)
            {
                var pickedUpCups = cupCircle.PickUpNextThreeCups(cup);
                var pickedUpLabels = new int[3];

                var currentCup = pickedUpCups;
                for (var i = 0; i < 3; i++)
                {
                    pickedUpLabels[i] = currentCup.Label;
                    currentCup = currentCup.Next;
                }

                var destination = cup.Label > 1 ? cup.Label - 1 : highestCupLabel;
                while (pickedUpLabels.Contains(destination))
                    destination = destination > 1 ? destination - 1 : highestCupLabel;
                
                var destinationCup = cupCircle.CupWithLabel(destination);
                cupCircle.PlaceCupsAfter(pickedUpCups, destinationCup);
                
                cup = cup.Next;
            }
        }

        public string PartTwo()
        {
            var N = 1000000;
            var cups = new int[N];
            var highestCup = CupLabels.Max();

            for (var i = 0; i < N; i++)
            {
                if (i < highestCup) cups[i] = CupLabels[i];
                else cups[i] = i + 1;
            }

            var cupCircle = new CupCircle(cups);
            PlayNMoves(cupCircle, N*10, cupCircle.CupWithLabel(CupLabels[0]), N);

            var cup1 = cupCircle.CupWithLabel(1).Next;
            var cup2 = cup1.Next;

            var result = (Int64)cup1.Label * (Int64)cup2.Label;
            return result.ToString();
        }
    }
}