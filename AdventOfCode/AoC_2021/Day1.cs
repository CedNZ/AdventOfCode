using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day1 : IDay<int>
    {
        public long A(List<int> inputs)
        {
            int lastDepth = inputs.First();
            int increaseCount = 0;

            foreach (var currentDepth in inputs.Skip(1))
            {
                if (currentDepth > lastDepth)
                {
                    increaseCount++;
                }
                lastDepth = currentDepth;
            }

            return increaseCount;
        }

        public long B(List<int> inputs)
        {
            var overlapping = LinqExtensions.OverlappingSets(inputs, 3);

            var lastDepth = overlapping.First();
            int increaseCount = 0;

            foreach (var currentDepth in overlapping.Skip(1))
            {
                if (currentDepth.Sum() > lastDepth.Sum())
                {
                    increaseCount++;
                }
                lastDepth = currentDepth;
            }

            return increaseCount;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.Select(int.Parse).ToList();
        }
    }
}
