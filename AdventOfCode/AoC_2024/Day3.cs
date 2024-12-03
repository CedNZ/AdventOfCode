using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day3 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            return inputs.Sum(input =>
            {
                var matches = Regex.Matches(input, @"mul\(\d+,\d+\)");

                return matches
                    .Select(m =>
                    {
                        var match = Regex.Matches(m.Value, @"\d+");
                        return match.Aggregate(1, (agg, next) => agg * int.Parse(next.Value));
                    })
                    .Sum();
            });
        }

        public long B(List<string> inputs)
        {
            bool on = true;
            return inputs.Sum(input =>
            {
                List<Match> matches = [];

                matches.AddRange(Regex.Matches(input, @"mul\(\d+,\d+\)"));
                matches.AddRange(Regex.Matches(input, @"do\(\)"));
                matches.AddRange(Regex.Matches(input, @"don't\(\)"));

                matches = matches.OrderBy(x => x.Index).ToList();
                
                var sum = 0;

                foreach (var match in matches)
                {
                    if (match.Value == "do()")
                    {
                        on = true;
                    }
                    else if (match.Value == "don't()")
                    {
                        on = false;
                    }
                    else
                    {
                        if (on)
                        {
                            var m = Regex.Matches(match.Value, @"\d+");
                            sum += m.Aggregate(1, (agg, next) => agg * int.Parse(next.Value));
                        }
                    }
                }

                return sum;
            });
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
