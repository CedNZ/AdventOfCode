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

            var results = Substitute(formula, 0, 10).OrderByDescending(x => x.Value).ToList();

            return results.First().Value - results.Last().Value;
        }

        public Dictionary<char, long> Substitute(string formula, int step, int limit)
        {
            if (step == limit)
            {
                return formula.GroupBy(c => c).ToDictionary(g => g.Key, g => (long)g.Count());
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

            return pairs.Aggregate(new Dictionary<char, long>(), (dict, pair) =>
            {
                var subDict = Substitute(pair, step + 1, limit);
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
            var formula = inputs[0];

            substitutions = inputs.Skip(2).ToList();

            Dictionary<string, long> characterCount = formula.GroupBy(c => c).ToDictionary(g => g.Key.ToString(), g => (long)g.Count());
            Dictionary<string, long> pairCount = formula.OverlappingSets(2).Where(x => x.Count() == 2).GroupBy(s => new string(s.ToArray())).ToDictionary(g => g.Key, g => (long)g.Count());
            Dictionary<string, long> pairCountNew;

            for (int i = 0; i < 40; i++)
            {
                pairCountNew = new();
                foreach (var rule in substitutions)
                {
                    var pair = rule.Substring(0, 2);
                    var result = rule.Last().ToString();

                    if (pairCount.ContainsKey(pair))
                    {
                        var pairMulti = pairCount[pair];
                        if (characterCount.ContainsKey(result))
                        {
                            characterCount[result] += pairMulti;
                        }
                        else
                        {
                            characterCount[result] = pairMulti;
                        }

                        var left = $"{pair[0]}{result}";
                        var right = $"{result}{pair[1]}";

                        if (pairCountNew.ContainsKey(left))
                        {
                            pairCountNew[left] += pairMulti;
                        }
                        else
                        {
                            pairCountNew[left] = pairMulti;
                        }

                        if (pairCountNew.ContainsKey(right))
                        {
                            pairCountNew[right] += pairMulti;
                        }
                        else
                        {
                            pairCountNew[right] = pairMulti;
                        }
                    }
                }
                pairCount = pairCountNew.ToDictionary(pair => pair.Key, pair => pair.Value);
            }

            return characterCount.Max(v => v.Value) - characterCount.Min(v => v.Value);
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
