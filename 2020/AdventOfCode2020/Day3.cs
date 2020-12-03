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

        public long B(List<string> inputs)
        {
            List<(int deltaX, int deltaY)> slopes = new List<(int deltaX, int deltaY)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            List<long> treeCounts = new List<long>();

            foreach(var item in slopes)
            {
                int deltaX = item.deltaX;
                int deltaY = item.deltaY;

                int treeCount = 0;

                for(int i = 0; i < inputs.Count(); i += deltaY)
                {
                    int lineLength = inputs[i].Length;
                    if(inputs[i][(deltaX * (i / deltaY)) % lineLength] == '#')
                    {
                        treeCount++;
                    }
                }

                treeCounts.Add(treeCount);
            }

            return treeCounts.Aggregate((agg, next) => agg * next);
        }
    }
}
