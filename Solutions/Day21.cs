using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day21 : Day
    {
        public Day21(string input) : base(input)
        {
        }

        public record Food(HashSet<string> Allergens, HashSet<string> Ingredients);

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var foods = new List<Food>();
            var allIngredients = new Dictionary<string, string>();
            var allAllergens = new Dictionary<string, List<Food>>();
            foreach (var line in lines)
            {
                var parts = Regex.Match(line, "(.*) \\(contains (.*)\\)");
                var ingredients = parts.Groups[1].Value.Split(" ");
                var allergens = parts.Groups[2].Value.Split(", ");
                foreach (var ingredient in ingredients)
                {
                    allIngredients[ingredient] = null;
                }

                var food = new Food(new HashSet<string>(allergens), new HashSet<string>(ingredients));
                foreach (var allergen in allergens)
                {
                    if (allAllergens.ContainsKey(allergen))
                        allAllergens[allergen].Add(food);
                    else
                        allAllergens[allergen] = new List<Food>() { food };
                }
                foods.Add(food);
            }

            foreach (var allergen in allAllergens.OrderByDescending(k => k.Value.Count))
            {
                var foodsWithAllergen = allergen.Value;
                var firstIngredients =
                    new HashSet<string>(foodsWithAllergen[0].Ingredients.Where(i => allIngredients[i] == null));
                foreach (var food in foodsWithAllergen.Skip(1))
                {
                    firstIngredients.IntersectWith(food.Ingredients.Where(i => allIngredients[i] == null));
                }

                foreach (var ingredient in firstIngredients)
                {
                    allIngredients[ingredient] = allergen.Key;

                }
            }

            var results = 0;
            foreach (var kvp in allIngredients.Where(i => i.Value == null))
            {
                foreach (var food in foods)
                {
                    if (food.Ingredients.Contains(kvp.Key))
                        results += 1;
                }
            }

            return results.ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var foods = new List<Food>();
            var allIngredients = new Dictionary<string, List<string>>();
            var allAllergens = new Dictionary<string, List<Food>>();
            foreach (var line in lines)
            {
                var parts = Regex.Match(line, "(.*) \\(contains (.*)\\)");
                var ingredients = parts.Groups[1].Value.Split(" ");
                var allergens = parts.Groups[2].Value.Split(", ");
                foreach (var ingredient in ingredients)
                {
                    allIngredients[ingredient] = new List<string>();
                }

                var food = new Food(new HashSet<string>(allergens), new HashSet<string>(ingredients));
                foreach (var allergen in allergens)
                {
                    if (allAllergens.ContainsKey(allergen))
                        allAllergens[allergen].Add(food);
                    else
                        allAllergens[allergen] = new List<Food>() { food };
                }

                foods.Add(food);
            }

            var allergensToCheck = new Queue<string>(allAllergens.Keys);
            while (allergensToCheck.Count > 0)
            {
                var allergen = allergensToCheck.Dequeue();
                var foodsWithAllergen = allAllergens[allergen];
                var firstIngredients =
                    new HashSet<string>(foodsWithAllergen[0].Ingredients.Where(i => allIngredients[i].Count == 0));
                foreach (var food in foodsWithAllergen.Skip(1))
                {
                    firstIngredients.IntersectWith(food.Ingredients.Where(i => allIngredients[i].Count == 0));
                }

                if (firstIngredients.Count != 1)
                {
                    allergensToCheck.Enqueue(allergen);
                    continue;
                }

                allIngredients[firstIngredients.First()].Add(allergen);
            }


            var results = allIngredients
                    .Where(i => i.Value.Count > 0)
                    .OrderBy(n => n.Value[0])
                    .Select(i => i.Key)
                    .ToArray();

            return string.Join(",", results);
        }
    }
}
