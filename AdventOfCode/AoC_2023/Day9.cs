using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day9 : IDay<List<long>>
    {
        public long A(List<List<long>> inputs)
        {
            return inputs.Select(GetNumber).Sum();
        }

        public long GetNumber(List<long> nums)
        {
            var diffs = nums.Skip(1).Select((x, i) => x - nums[i]).ToList();
            if (diffs.All(x => x == 0))
            {
                return nums[^1];
            }
            return nums[^1] + GetNumber(diffs);
        }

        public long B(List<List<long>> inputs)
        {
            return inputs.Select(GetNumberReverse).Sum();
        }

        public long GetNumberReverse(List<long> nums)
        {
            var diffs = nums.Skip(1).Select((x, i) => x - nums[i]).ToList();
            if (diffs.All(x => x == 0))
            {
                return nums[0];
            }
            return nums[0] - GetNumberReverse(diffs);
        }

        public List<List<long>> SetupInputs(string[] inputs)
        {
            return inputs.Select(l => l.Split(' ').Select(long.Parse).ToList()).ToList();
        }
    }
}
