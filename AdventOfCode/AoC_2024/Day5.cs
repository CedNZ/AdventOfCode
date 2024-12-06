using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day5 : IDay<(List<(int, int)> rules, List<List<int>> updates)>
    {
        public long A(List<(List<(int, int)> rules, List<List<int>> updates)> inputs)
        {
            var (rules, updates) = inputs[0];

            var result = 0;

            foreach (var update in updates)
            {
                HashSet<int> seen = [];
                var good = true;
                foreach (var number in update)
                {
                    var rs = rules.Where(x => x.Item1 == number).ToList();
                    if (rs.Any(r => seen.Contains(r.Item2)))
                    {
                        good = false;
                        break;
                    }
                    seen.Add(number);
                }
                if (good && update.Count % 2 == 1)
                {
                    result += update[update.Count / 2];
                }
            }

            return result;
        }

        public long B(List<(List<(int, int)> rules, List<List<int>> updates)> inputs)
        {
            var (rules, updates) = inputs[0];

            var result = 0;

            foreach (var update in updates)
            {
                HashSet<int> seen = [];
                var good = true;
                foreach (var number in update)
                {
                    var rs = rules.Where(x => x.Item1 == number).ToList();
                    if (rs.Any(r => seen.Contains(r.Item2)))
                    {
                        good = false;
                        break;
                    }
                    seen.Add(number);
                }
                if (!good && update.Count % 2 == 1)
                {
                    var nums = update.ToList();
                    nums = nums.GroupJoin(rules,
                        n => n,
                        r => r.Item1,
                        (n, rs) => new RuleSet
                        {
                            Parent = n,
                            Children = rs.Select(r => r.Item2).ToList(),
                        })
                        .OrderByDescending(r => r)
                        .Select(g => g.Parent)
                        .ToList();

                    result += nums[update.Count / 2];
                }
            }

            return result;
        }

        internal class RuleSet : IComparable<RuleSet>
        {
            public int Parent { get; set; }
            public List<int> Children { get; set; } = [];

            public int CompareTo(RuleSet other)
            {
                if (Children.Contains(other.Parent))
                {
                    return 1;
                }
                if (other.Children.Contains(Parent))
                {
                    return -1;
                }
                return 0;
            }
        }

        public List<(List<(int, int)> rules, List<List<int>> updates)> SetupInputs(string[] inputs)
        {
            var ruleLines = inputs.TakeWhile(x => x != "").ToList();
            var updateLines = inputs.SkipWhile(x => x != "").Skip(1).ToList();

            var rules = ruleLines.ConvertAll(x =>
            {
                var parts = x.Split("|");
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            });

            var updates = updateLines.ConvertAll(x => x.Split(",").Select(int.Parse).ToList());

            return new List<(List<(int, int)> rules, List<List<int>> updates)> { (rules, updates) };
        }
    }
}
