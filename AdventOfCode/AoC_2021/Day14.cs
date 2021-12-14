using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day14 : IDay<string>
    {
        List<string> substitutions;

        public long A(List<string> inputs)
        {
            var formula = inputs[0];

            substitutions = inputs.Skip(2).ToList();

            var results = Substitute(formula, 0).OrderByDescending(x => x.Value).ToList();

            return results.First().Value - results.Last().Value;
        }

        public Dictionary<char, int> Substitute(string formula, int step)
        {
            if (step == 10)
            {
                return formula.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            }

            var pairs = LinqExtensions.OverlappingSets(formula, 2).Select(x => new string(x.ToArray())).Where(x => x.Length == 2).ToList();

            for (int i = 0; i < pairs.Count(); i++)
            {
                if (substitutions.FirstOrDefault(s => s.StartsWith(pairs[i])) is string substitute)
                {
                    pairs[i] = $"{pairs[i][0]}{substitute.Last()}{pairs[i][1]}";
                }
            }

            char? lastChar = null;

            return pairs.Aggregate(new Dictionary<char, int>(), (dict, pair) =>
            {
                var subDict = Substitute(pair, step + 1);
                if (lastChar.HasValue)
                {
                    subDict[lastChar.Value]--;
                }
                foreach (var item in subDict)
                {
                    if (dict.ContainsKey(item.Key))
                    {
                        dict[item.Key] += item.Value;
                    }
                    else
                    {
                        dict.Add(item.Key, item.Value);
                    }
                }
                lastChar = pair.Last();
                return dict;
            });
        }


        public long B(List<string> inputs)
        {
            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
