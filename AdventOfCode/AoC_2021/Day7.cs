using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day7 : IDay<int>
    {
        public long A(List<int> inputs)
        {
            var mode = inputs.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            var average = (int)Math.Ceiling(inputs.Average());

            var min = Math.Min(mode, average);
            var max = Math.Max(mode, average);

            var lowest = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                var sum = inputs.Sum(x => Math.Abs(x - i));

                if (sum < lowest)
                {
                    lowest = sum;
                }
            }

            return lowest;
        }

        public long B(List<int> inputs)
        {
            var mode = inputs.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            var average = (int)Math.Ceiling(inputs.Average());

            var min = Math.Min(mode, average);
            var max = Math.Max(mode, average);

            var lowest = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                var sum = inputs.Sum(x =>
                {
                    var distance = Math.Abs(x - i);
                    return (distance * (distance + 1)) / 2;
                });

                if (sum < lowest)
                {
                    lowest = sum;
                }
            }

            return lowest;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs[0].Split(',').Select(int.Parse).ToList();
        }
    }
}
