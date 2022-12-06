using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day6 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var line = inputs[0];
            return FindIndex(line, 4);
        }

        public int FindIndex(string input, int size)
        {
            var i = 0;
            for (; i < input.Length - size; i++)
            {
                var chars = input.Skip(i).Take(size).Distinct();
                if (chars.Count() == size)
                {
                    break;
                }
            }
            return i + size;
        }

        public long B(List<string> inputs)
        {
            var line = inputs[0];

            return FindIndex(line, 14);
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
