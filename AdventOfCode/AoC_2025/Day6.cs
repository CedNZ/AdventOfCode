using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day6 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            return inputs.SelectMany((line, rowNum) => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select((input, column) => (problem: column, position: rowNum, input)))
                .GroupBy(x => x.problem)
                .Sum(g =>
                {
                    var group = g.OrderByDescending(x => x.position)
                        .Select(x => x.input)
                        .ToList();
                    var op = group[0];
                    var nums = group.Skip(1).Select(long.Parse);
                    return op == "*"
                        ? nums.Aggregate((agg, next) => agg * next)
                        : nums.Aggregate((agg, next) => agg + next);
                });
        }

        public long B(List<string> inputs)
        {
            return SetupProblemsB(inputs)
                .Sum(p => p.op == '*'
                    ? p.nums.Aggregate((agg, next) => agg * next)
                    : p.nums.Aggregate((agg, next) => agg + next));
        }

        public IEnumerable<(List<long> nums, char op)> SetupProblemsB(List<string> inputs)
        {
            List<long> nums = [];
            var maxX = inputs.Max(x => x.Length);
            var maxY = inputs.Count;
            for (int x = maxX - 1; x >= 0; x--)
            {
                var num = 0L;
                for (int y = 0; y < maxY; y++)
                {
                    var s = inputs[y];
                    if (s.Length > x)
                    {
                        var c = s[x];
                        if (c is '*' or '+')
                        {
                            nums.Add(num);
                            num = 0;
                            yield return (nums, c);
                            nums = [];
                        }
                        else
                        {
                            if (long.TryParse(c.ToString(), out var res))
                            {
                                num *= 10;
                                num += res;
                            }
                        }
                    }
                }
                if (num > 0)
                {
                    nums.Add(num);
                }
            }
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
