using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day21
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<Food> _foods;
        
        public Solution(IEnumerable<string> input)
        {
            _foods = input.Select(ParseFood).ToList();
        }

        public int PartOne()
        {
            var ingredientsByAllergen = GetIngredientsByAllergen();
            return _foods
                .SelectMany(food => food.Ingredients)
                .Count(ingredient => ingredientsByAllergen.All(pair => !pair.Value.Contains(ingredient)));
        }

        public string PartTwo()
        {
            var allergenToPossibleIngredients = GetIngredientsByAllergen();
            
            var ingredientToAllergen = new Dictionary<string, string>();
            while (allergenToPossibleIngredients.Count > 0)
            {
                foreach (var (allergen, ingredients) in allergenToPossibleIngredients)
                {
                    var ingredientsLeft = ingredients.Except(ingredientToAllergen.Keys).ToList();
                    
                    if (ingredientsLeft.Count != 1) 
                        continue;
                    
                    ingredientToAllergen[ingredientsLeft.Single()] = allergen;
                    allergenToPossibleIngredients.Remove(allergen);
                    break;
                }
            }

            var result = ingredientToAllergen.OrderBy(x => x.Value).Select(x => x.Key);
            return string.Join(",", result);
        }

        private static Food ParseFood(string line)
        {
            var ingredientsAndAllergens = line.Split(" (contains ");
            var ingredients = ingredientsAndAllergens[0].Split(" ");
            var allergens = ingredientsAndAllergens[1].TrimEnd(')').Split(", ");

            return new Food(ingredients, allergens);
        }

        private IDictionary<string, ISet<string>> GetIngredientsByAllergen()
        {
            var result = new Dictionary<string, ISet<string>>();

            foreach (var food in _foods)
            {
                foreach (var allergen in food.Allergens)
                {
                    if (!result.ContainsKey(allergen))
                        result[allergen] = food.Ingredients.ToHashSet();
                    else
                        result[allergen].IntersectWith(food.Ingredients);
                }
            }

            return result;
        }
    }

    internal class Food
    {
        public Food(IReadOnlyCollection<string> ingredients, IReadOnlyCollection<string> allergens)
        {
            Ingredients = ingredients;
            Allergens = allergens;
        }

        public IReadOnlyCollection<string> Ingredients { get; }
        public IReadOnlyCollection<string> Allergens { get; }
    }
}