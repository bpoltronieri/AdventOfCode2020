using System.Collections.Generic;

namespace AoC2020.Days.Day21Utils
{
    internal class Food
    {
        public List<string> Ingredients { get; }
        public List<string> Allergens { get; }
        public Food(List<string> ingredients, List<string> allergens)
        {
            Ingredients = ingredients;
            Allergens = allergens;
        }
    }
}