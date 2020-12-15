using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day15 : IDay<int>
    {
        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.First().Split(',').Select(int.Parse).ToList();
        }

        public long A(List<int> inputs)
        {
            return PlayGame(inputs, 2020);
        }

        public long PlayGame(List<int> inputs, int limit)
        {
            Dictionary<int, (int, int)> numbers = new Dictionary<int, (int, int)>();

            int nextNumber = 0;
            (int, int) lastIndex = (0, 0);

            for(int i = 0; i < limit; i++)
            {
                if(i < inputs.Count())
                {
                    nextNumber = inputs[i];
                    lastIndex = (-1, i);
                    numbers[nextNumber] = lastIndex;
                }
                else
                {
                    if(lastIndex.Item1 < 0)
                    {
                        nextNumber = 0;
                    }
                    else
                    {
                        nextNumber = (lastIndex.Item2 + 1) - (lastIndex.Item1 + 1);
                    }

                    if(numbers.ContainsKey(nextNumber))
                    {
                        lastIndex = numbers[nextNumber];
                        lastIndex.Item1 = lastIndex.Item2;
                        lastIndex.Item2 = i;
                    }
                    else
                    {
                        lastIndex = (-1, i);
                    }
                    numbers[nextNumber] = lastIndex;
                }
            }

            return nextNumber;
        }


        public long B(List<int> inputs)
        {
            return PlayGame(inputs, 30000000);
        }
    }
}
