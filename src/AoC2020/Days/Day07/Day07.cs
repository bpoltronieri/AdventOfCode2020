using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoC2020.Days.Day07Utils;

namespace AoC2020.Days
{
    public class Day07 : IDay
    {
        private string[] input;

        public Day07(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }     

        // Counts how many bag colours can eventually contain at least one shiny gold bag.
        // Start at shiny gold and go up the tree of possible containers.
        public string PartOne()
        {
            var BagToContainers = MapBagsToContainers();
            var shinyGoldContainers = new HashSet<string>();
            var bagStack = new Stack<string>();
            bagStack.Push("shiny gold");

            while (bagStack.Count > 0)
            {
                var currentBag = bagStack.Pop();
                if (BagToContainers.ContainsKey(currentBag))
                {
                    foreach (var container in BagToContainers[currentBag])
                    {
                        shinyGoldContainers.Add(container);
                        bagStack.Push(container);
                    }
                }
            }
            return (shinyGoldContainers.Count).ToString();
        }

        private Dictionary<string, List<string>> MapBagsToContainers()
        {
            var BagToContainers = new Dictionary<string, List<string>>();
            foreach (var line in input)
            {
                var words = line.Split();
                var container = words[0] + " " + words[1];

                if (words[4] == "no") continue; // empty container

                var i = 5;
                var word = words[i];
                var currentBag = word;
                while (true)
                {
                    i += 1;
                    word = words[i];
                    
                    if (word.All(char.IsLetter)) // part of bag name
                        currentBag += " " + word.Substring(0, word.Length);
                    else if (word.Last() == ',') // "bags," end of current bag but more to come
                    {
                        AddToBagToContainersMap(currentBag, container, BagToContainers);
                        i += 2;
                        word = words[i];
                        currentBag = word;
                    }
                    else if (word.Last() == '.') // "bags." end of current bag and end of line
                    {
                        AddToBagToContainersMap(currentBag, container, BagToContainers);
                        break;
                    }
                }
            }
            return BagToContainers;
        }

        private void AddToBagToContainersMap(string currentBag, string container, Dictionary<string, List<string>> bagToContainers)
        {
            if (bagToContainers.ContainsKey(currentBag))
                bagToContainers[currentBag].Add(container);
            else
            {
                var containers = new List<string>();
                containers.Add(container);
                bagToContainers.Add(currentBag, containers);
            }
        }

        // Counts how many individual bags are required inside a shiny gold bag.
        // Starts at shiny gold bag and go down the tree of contents.
        public string PartTwo()
        {
            var BagsToContents = MapBagsToContents();            
            var nBags = 0;
            var bagStack = new Stack<BagRequirement>();
            foreach (var bag in BagsToContents["shiny gold"])
                bagStack.Push(bag);

            while (bagStack.Count > 0)
            {
                var currentBag = bagStack.Pop();
                nBags += currentBag.nRequired;
                
                if (BagsToContents.ContainsKey(currentBag.Bag))
                {
                    foreach (var bag in BagsToContents[currentBag.Bag])
                    {
                        var newBag = new BagRequirement();
                        newBag.Bag = bag.Bag;
                        newBag.nRequired = bag.nRequired * currentBag.nRequired;
                        bagStack.Push(newBag);
                    }
                }
            }
            return nBags.ToString();
        }

        private Dictionary<string, List<BagRequirement>> MapBagsToContents()
        {
            var BagsToContents = new Dictionary<string, List<BagRequirement>>();
            foreach (var line in input)
            {
                var words = line.Split();
                var container = words[0] + " " + words[1];

                if (words[4] == "no") continue;
                
                var currentNumber = int.Parse(words[4]);
                var word = words[5];
                var i = 5;
                var currentBag = word;
                while (true)
                {
                    i += 1;
                    word = words[i];
                    
                    if (word.All(char.IsLetter))
                        currentBag += " " + word.Substring(0, word.Length);
                    else if (word.Last() == ',')
                    {
                        AddToBagsToContentsMap(container, currentBag, currentNumber, BagsToContents);
                        currentNumber = int.Parse(words[i+1]);
                        i += 2;
                        word = words[i];
                        currentBag = word;
                    }
                    else if (word.Last() == '.')
                    {
                        AddToBagsToContentsMap(container, currentBag, currentNumber, BagsToContents);
                        break;
                    }
                }
            }
            return BagsToContents;
        }

        private void AddToBagsToContentsMap(string bag, string content, int nRequired, Dictionary<string, List<BagRequirement>> bagsToContents)
        {
            var newBag = new BagRequirement();
            newBag.Bag = content;
            newBag.nRequired = nRequired;

            if (bagsToContents.ContainsKey(bag))
                bagsToContents[bag].Add(newBag);
            else
            {
                var bags = new List<BagRequirement>();
                bags.Add(newBag);
                bagsToContents.Add(bag, bags);
            }
        }
    }
}