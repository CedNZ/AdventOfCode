using AdventOfCodeCore;

namespace AoC_2015
{
    public class Day1 : IDay<char>
    {
        public long A(List<char> inputs)
        {
            int floor = 0;
            foreach (char c in inputs)
            {
                if (c == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }
            }
            return floor;
        }

        public long B(List<char> inputs)
        {
            int index = 1;
            int floor = 0;
            foreach (char c in inputs)
            {
                if (c == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }

                if (floor == -1)
                {
                    return index;
                }
                index++;
            }
            return index;
        }

        public List<char> SetupInputs(string[] inputs)
        {
            return inputs[0].ToList();
        }
    }
}
