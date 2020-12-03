using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day3 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }

        public int A(List<string> inputs)
        {
            int deltaX = 3;
            int deltaY = 1;

            int treeCount = 0;

            for(int i = 0; i < inputs.Count(); i += deltaY)
            {
                int lineLength = inputs[i].Length;
                if (inputs[i][(deltaX * i) % lineLength] == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }

        public int B(List<string> inputs)
        {
            return -1;
        }
    }
}
