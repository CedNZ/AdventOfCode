using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day1 : IDay<(List<int> left, List<int> right)>
    {
        public long A(List<(List<int> left, List<int> right)> inputs)
        {
            var (left, right) = inputs[0];
            left = left.Order().ToList();
            right = right.Order().ToList();
            var num = 0;
            for (int i = 0; i < left.Count; i++)
            {
                num += Math.Abs(left[i] - right[i]);
            }
            return num;
        }

        public long B(List<(List<int> left, List<int> right)> inputs)
        {
            var (left, right) = inputs[0];
            var num = left.Aggregate(0, (agg, next) => agg + next * (right.Count(x => x == next)));
            return num;
        }

        public List<(List<int> left, List<int> right)> SetupInputs(string[] inputs)
        {
            List<int> left = [];
            List<int> right = [];
            foreach (var s in inputs)
            {
                var p = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                left.Add(int.Parse(p[0]));
                right.Add(int.Parse(p[1]));
            }
            return [(left, right)];
        }
    }
}
