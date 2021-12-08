using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day6 : IDay<int>
    {
        public long A(List<int> inputs)
        {
            for (int i = 0; i < 80; i++)
            {
                GoFish(inputs);
            }

            return inputs.Count();
        }

        private List<int> GoFish(List<int> fish)
        {
            int fishCount = fish.Count();

            for (int i = 0; i < fishCount; i++)
            {
                fish[i]--;
                if (fish[i] < 0)
                {
                    fish[i] = 6;
                    fish.Add(8);
                }
            }
            return fish;
        }

        public long B(List<int> inputs)
        {
            Dictionary<int, long> dict = new Dictionary<int, long>();
            long sum = 0;

            foreach (var item in inputs)
            {
                if (dict.TryGetValue(item, out var count))
                {
                    sum += count;
                    continue;
                }
                List<int> fish = new List<int> { item };
                for (int i = 0; i < 256; i++)
                {
                    GoFish(fish);
                }

                count = fish.Count();
                sum += count;

                dict.Add(item, count);
            }

            return sum;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs[0].Split(',').Select(int.Parse).ToList();
        }
    }
}
