using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day6 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var line = inputs[0];
            var i = 4;
            for (; i < line.Length; i++)
            {
                if (line[i] != line[i - 1] && line[i - 1] != line[i - 2] && line[i - 2] != line[i - 3]
                    && line[i] != line[i - 2] && line[i] != line[i -3]
                    && line[i - 1] != line[i - 3])
                {
                    break;
                }
            }
            return i + 1;
        }

        public long B(List<string> inputs)
        {
            var line = inputs[0];

            var i = 0;
            for (; i < line.Length - 14; i++)
            {
                var chars = line.Skip(i).Take(14).Distinct();
                if (chars.Count() == 14)
                {
                    break;
                }
            }
            return i + 14;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
