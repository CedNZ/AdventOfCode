using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day21 : IDayOut<(List<string> Allergens, List<string> Ingredients), string>
    { 
        public List<(List<string> Allergens, List<string> Ingredients)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x =>
            {
                var parts = x.Replace("(", "").Replace(")", "").Replace(",", "").Split(" contains ");

                return (parts[1].Split(' ').ToList(), parts[0].Split(' ').ToList());
            }).ToList();
        }

        public string A(List<(List<string> Allergens, List<string> Ingredients)> inputs)
        {
            var allAllergens = inputs.SelectMany(x => x.Item1).Distinct();
            Dictionary<string, List<string>> possibleMatching = new Dictionary<string, List<string>>();

            foreach(var allergen in allAllergens)
            {
                var allergenAsList = new List<string> { allergen };

                var allergenMatch = inputs
                    .Where(p => p.Item1.Contains(allergen))
                    .OrderBy(x => x.Item2.Count())
                    .Aggregate((curr, next) =>
                    {
                        return (allergenAsList, curr.Item2.Intersect(next.Item2).ToList());
                    });

                possibleMatching.Add(allergenMatch.Item1.Single(), allergenMatch.Item2);
            }

            return inputs.SelectMany(x => x.Item2).Count(x => !possibleMatching.Any(a => a.Value.Contains(x))).ToString();
        }

        public string B(List<(List<string> Allergens, List<string> Ingredients)> inputs)
        {
            var allAllergens = inputs.SelectMany(x => x.Item1).Distinct();
            Dictionary<string, List<string>> possibleMatching = new Dictionary<string, List<string>>();

            foreach(var allergen in allAllergens)
            {
                var allergenAsList = new List<string> { allergen };

                var allergenMatch = inputs
                    .Where(p => p.Item1.Contains(allergen))
                    .OrderBy(x => x.Item2.Count())
                    .Aggregate((curr, next) =>
                    {
                        return (allergenAsList, curr.Item2.Intersect(next.Item2).ToList());
                    });

                possibleMatching.Add(allergenMatch.Item1.Single(), allergenMatch.Item2);
            }

            List<(string Allergen, string Ingredient)> matchings = new List<(string Allergen, string Ingredient)>();

            while(possibleMatching.Any())
            {
                var match = possibleMatching.First(x => x.Value.Count() == 1);

                possibleMatching.Remove(match.Key);

                matchings.Add((match.Key, match.Value.Single()));

                foreach(var unmatchedAllergen in allAllergens.Where(a => !matchings.Any(m => m.Allergen == a)))
                {
                    possibleMatching[unmatchedAllergen] = possibleMatching[unmatchedAllergen].Where(x => !matchings.Any(m => m.Ingredient == x)).ToList();
                }
            }

            return string.Join(',', matchings.OrderBy(a => a.Allergen).Select(m => m.Ingredient));
        }
    }
}
