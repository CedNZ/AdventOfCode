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
            return default;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs[0].Split(',').Select(int.Parse).ToList();
        }
    }
}
