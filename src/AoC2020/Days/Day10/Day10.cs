using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day10 : IDay
    {
        private List<int> Adapters;

        public Day10(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           Adapters = Array.ConvertAll(File.ReadAllLines(file), int.Parse).ToList();
           Adapters.Add(0); // easier for part 2 if 0 in the list
           Adapters.Sort(); // sorted adapters makes first part trivial
        }     

        // Using all adapters in a chain, count number of 1-jolt jumps and 3-jolt jumps.
        public string PartOne()
        {
            var n1JoltDiffs = 0;
            var n3JoltDiffs = 0;

            var previousAdapter = 0;
            foreach (var adapter in Adapters)
            {
                if (adapter - previousAdapter == 1)
                    n1JoltDiffs += 1;
                else if (adapter - previousAdapter == 3)
                    n3JoltDiffs += 1;
                previousAdapter = adapter;
            }
            n3JoltDiffs += 1; // device's built-in adapter

            return (n1JoltDiffs * n3JoltDiffs).ToString();
        }

        // Finds the number of distinct ways to arrange the adapters from 0 to the device.
        // Uses the fact that the number of paths to a given adapter is the sum of the 
        // number of paths to its (up to) 3 possible parent adapters.
        public string PartTwo()
        {
            var adaptersToNPaths = new Dictionary<int, Int64>(); // caching dictionary
            adaptersToNPaths[Adapters.First()] = 1; // only 1 path to initial charging outlet (0 adapter)
            return nPathsToAdapter(Adapters.Last(), adaptersToNPaths).ToString();      
        }

        private Int64 nPathsToAdapter(int adapter, Dictionary<int, Int64> adaptersToNPaths)
        {
            if (adaptersToNPaths.ContainsKey(adapter))
                return adaptersToNPaths[adapter];

            var parents = Adapters.Where(x => x < adapter && x >= adapter - 3);
            Int64 nPaths = 0;
            foreach (var parent in parents)
                nPaths += nPathsToAdapter(parent, adaptersToNPaths);
                
            adaptersToNPaths[adapter] = nPaths;
            return nPaths;
        }
    }
}