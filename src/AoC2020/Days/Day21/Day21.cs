using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoC2020.Days.Day21Utils;

namespace AoC2020.Days
{
    public class Day21 : IDay
    {
        private List<Food> Foods;

        public Day21(string file)
        {
            Foods = new List<Food>();
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            foreach (var line in File.ReadAllLines(file))
            {
                var ingredients = new List<string>();
                var allergens = new List<string>();

                var ingsAndAlgs = line.Split(" (contains ");

                ingredients.AddRange(ingsAndAlgs[0].Split());

                if (ingsAndAlgs.Length > 1) // contains allergens
                    allergens.AddRange(ingsAndAlgs[1].TrimEnd(')').Split(", "));

                Foods.Add(new Food(ingredients, allergens));
            }
        }

        // Finds ingredients which can't contain any of the allergens in our list.
        // Returns their number of occurences in our list of foods.
        public string PartOne()
        {
            var algsToPossibleIngs = SetUpAllergensMap();
            var safeIngredients = Foods
                .SelectMany(f => f.Ingredients)
                .Where(i => IngNotPossibleForAnyAlgs(algsToPossibleIngs, i));

            // safeIngredients contains duplicates but that's what we want since we need the number of occurences
            return safeIngredients.Count().ToString(); 
        }

        private bool IngNotPossibleForAnyAlgs(Dictionary<string, HashSet<string>> algsToPossibleIngs, string ingredient)
        {
            return !algsToPossibleIngs.Values.Any(P => P.Contains(ingredient));
        }

        // Maps allergens to possible ingredients, and tries to reduce it to a 1-1 map.
        // If an ingredient contains an allergen, it should be present in every food that contains the allergen.
        private Dictionary<string, HashSet<string>> SetUpAllergensMap()
        {
            var algsToIngs = new Dictionary<string, HashSet<string>>();
            var reservedIngs = new HashSet<string>();
            foreach (var food in Foods)
                foreach (var allergen in food.Allergens)
                {
                    var newPossibleIngs = food.Ingredients.Where(i => !reservedIngs.Contains(i)).ToHashSet();
                    if (!algsToIngs.ContainsKey(allergen)) // first time  
                        algsToIngs[allergen] = newPossibleIngs;
                    else if (algsToIngs[allergen].Count > 1) // not already figured it out
                        algsToIngs[allergen].IntersectWith(newPossibleIngs);
                    
                    if (algsToIngs[allergen].Count == 1)
                    {
                        var onlyIngredient = algsToIngs[allergen].First();
                        reservedIngs.Add(onlyIngredient);
                        RemoveIngFromOtherPossibleLists(algsToIngs, reservedIngs, onlyIngredient, allergen);
                    }
                }
            return algsToIngs;
        }

        private void RemoveIngFromOtherPossibleLists(Dictionary<string, HashSet<string>> algsToIngs, HashSet<string> reservedIngs, string onlyIngredient, string onlyAllergen)
        {
            foreach (var allergen in algsToIngs.Keys)
            {
                if (allergen == onlyAllergen) continue;
                else
                {
                    var removed = algsToIngs[allergen].Remove(onlyIngredient);
                    if (removed && algsToIngs[allergen].Count == 1)
                    {
                        var newOnlyIng = algsToIngs[allergen].First();
                        reservedIngs.Add(newOnlyIng);
                        RemoveIngFromOtherPossibleLists(algsToIngs, reservedIngs, newOnlyIng, allergen);
                    }
                }
                    
            }
        }

        // Returns list of dangerous ingredients in alphabetical order of allergen name.
        // I already have the list of dangerous ingredients from part one.
        public string PartTwo()
        {
            var algsToPossibleIngs = SetUpAllergensMap(); // Should be one to one map at this point

            var algsList = algsToPossibleIngs.Keys.ToList();
            algsList.Sort();

            var dangerousIngs = "";
            foreach (var alg in algsList)
            {
                if (algsToPossibleIngs[alg].Count != 1)
                    throw new InvalidDataException();
                dangerousIngs += algsToPossibleIngs[alg].First() + ",";
            }
            return dangerousIngs.TrimEnd(',');
        }
    }
}