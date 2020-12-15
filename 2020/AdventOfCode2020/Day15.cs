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
            List<int> numbers = new List<int>(2020);

            var lastNumber = 0;

            for (int i = 0; i < 2020; i++)
            {
                if(i < inputs.Count())
                {
                    numbers.Add(inputs[i]);
                    
                }
                else
                {
                    if (numbers.Count(x => x == lastNumber) > 1)
                    {
                        var mostRecentIndex = numbers.LastIndexOf(lastNumber);
                        var indexBefore = numbers.LastIndexOf(lastNumber, mostRecentIndex - 1);

                        numbers.Add((mostRecentIndex + 1)- (indexBefore + 1));
                    }
                    else
                    {
                        numbers.Add(0);
                    }
                }
                lastNumber = numbers.Last();
            }

            return lastNumber;
        }

        public long B(List<int> inputs)
        {
            return -1;
        }
    }
}
