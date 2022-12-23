using AdventOfCodeCore;

namespace AoC_2018
{
    public class Day1 : IDay<int>
    {
        public long A(List<int> inputs)
        {
            return inputs.Sum();
        }

        public long B(List<int> inputs)
        {
            return default;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.Select(int.Parse).ToList();
        }
    }
}
