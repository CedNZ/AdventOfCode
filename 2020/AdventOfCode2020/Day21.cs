using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day21 : IDay<(List<string> Allergens, List<string> Ingredients)>
    { 
        public List<(List<string> Allergens, List<string> Ingredients)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x =>
            {
                var parts = x.Replace("(", "").Replace(")", "").Replace(",", "").Split(" contains ");

                return (parts[1].Split(' ').ToList(), parts[0].Split(' ').ToList());
            }).ToList();
        }

        public long A(List<(List<string> Allergens, List<string> Ingredients)> inputs)
        {
            var allAllergens = inputs.SelectMany(x => x.Item1).Distinct();
            //var allIngredients = inputs.SelectMany(x => x.Item2).Distinct();
            Dictionary<string, List<string>> possibleMatching = new Dictionary<string, System.Collections.Generic.List<string>>();

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

            return inputs.SelectMany(x => x.Item2).Count(x => !possibleMatching.Any(a => a.Value.Contains(x)));
        }

        public long B(List<(List<string> Allergens, List<string> Ingredients)> inputs)
        {
            return -1;
        }
    }
}
