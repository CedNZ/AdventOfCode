using AdventOfCodeCore;

namespace AoC_2022
{
    internal class Day1 : IDay<List<int>>
    {
        public long A(List<List<int>> inputs)
        {
            return inputs.Select(x => x.Sum()).Max();
        }

        public long B(List<List<int>> inputs)
        {
            return inputs.Select(x => x.Sum()).OrderDescending().Take(3).Sum();
        }

        public List<List<int>> SetupInputs(string[] inputs)
        {
            var inputs2 = inputs.ToList();
            List<List<int>> ints = new();
            while (inputs2.Count() > 0)
            {
                var nums = inputs2.TakeWhile(x => x != "").ToList();
                inputs2 = inputs2.Skip(nums.Count() + 1).ToList();
                ints.Add(nums.Select(int.Parse).ToList());
            }
            return ints;   
        }
    }
}
