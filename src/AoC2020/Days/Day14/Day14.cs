using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day14 : IDay
    {
        private string[] input;
        private Dictionary<ulong, ulong> memory;
        private ulong bitMaskAND;
        private ulong bitMaskOR;

        public Day14(string file)
        {
            LoadInput(file);
            memory = new Dictionary<ulong, ulong>();
        }

        private void LoadInput(string file)
        {
           input = File.ReadAllLines(file);
        }     

        public string PartOne()
        {
            foreach (var line in input)
            {
                var splitLine = line.Split();
                if (splitLine[0] == "mask")
                    LoadBitMask(splitLine[2].ToList());
                else
                {
                    var memAddress = ulong.Parse(splitLine[0].Substring(4, splitLine[0].Length - 5));
                    var newValue = ulong.Parse(splitLine[2]);
                    newValue = ApplyBitMask(newValue);
                    memory[memAddress] = newValue;
                }
            }
            return SumOfMemory().ToString();
        }

        private ulong SumOfMemory()
        {
            return memory.Values.Aggregate((x,y) => x + y);
        }

        private ulong ApplyBitMask(ulong value)
        {
            return (value & bitMaskAND) | bitMaskOR;
        }

        // Converts bitMask string into bitMaskAND and bitMaskOR ulong values
        // to be used by ApplyBitMask to do some bitwise operations.
        private void LoadBitMask(List<char> bitMask)
        {
            bitMaskAND = bitMaskOR = 0;
            foreach (var bit in bitMask)
            {
                bitMaskAND *= 2;
                bitMaskOR *= 2;

                if (bit == '1')
                {
                    bitMaskOR += 1;
                    bitMaskAND += 1;
                }
                else if (bit == 'X')
                    bitMaskAND += 1;
            }
        }

        public string PartTwo()
        {
            for (var i = 0; i < input.Length; i++)
            {
                var splitLine = input[i].Split();
                var j = i+1;
                while (j < input.Length && input[j].Split()[0] != "mask")
                {
                    var newSplitLine = input[j].Split();
                    foreach (var bitMaskList in bitMaskGenerator(splitLine[2]))
                    {
                        LoadBitMask(bitMaskList);
                        var memAddress = ulong.Parse(newSplitLine[0].Substring(4, newSplitLine[0].Length - 5));
                        var newValue = ulong.Parse(newSplitLine[2]);
                        memAddress = ApplyBitMask(memAddress);
                        memory[memAddress] = newValue;
                    }
                    j += 1;
                }
                i = j-1;
            }
            return SumOfMemory().ToString();
        }

        // Generates bitmask strings to be used by LoadBitMask from the given starting bitMaskString.
        // Occurences of '1' can stay as they are. Occurences of '0' must become 'X' as '0'
        // now takes the role of 'X' in part one.
        // Occurences of 'X' can now take either value 0 or 1, which is why multiple bitmasks can be
        // generated from the same bitMaskString. We go through all binary numbers with the same 
        // number of digits as the number of 'X's (with leading zeroes) to find all possible 
        // bitmask strings.
        private IEnumerable<List<char>> bitMaskGenerator(string bitMaskString)
        {
            var bitMaskList = bitMaskString.ToList();
            var nFloatingBits = bitMaskList.Count(x => x == 'X');
            var nCombinations = (int)Math.Pow(2, nFloatingBits);

            for (var combination = 0; combination < nCombinations; combination++)
            {
                var combinationBinary = Convert.ToString(combination, 2).PadLeft(nFloatingBits, '0');
                var bin_index = 0;

                var currentBitMask = bitMaskList.Select(x => x == 'X' ? combinationBinary[bin_index++] : 
                                                             x == '0' ? 'X' : 
                                                             x)
                                                .ToList();
                yield return currentBitMask;   
            }
        }
    }
}