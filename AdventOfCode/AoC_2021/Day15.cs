using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day15 : IDay<int>
    {
        int[,] map;
        int sideLength;

        public long A(List<int> inputs)
        {
            sideLength = (int)Math.Sqrt(inputs.Count);

            map = new int[sideLength, sideLength];
            int index = 0;
            for (int col = 0; col < sideLength; col++)
            {
                for (int row = 0; row < sideLength; row++)
                {
                    map[col, row] = inputs[index++];
                }
            }

            return FindPath(0, 0) - map[0, 0];
        }

        public int FindPath(int col, int row)
        {
            if (col == sideLength - 1 && row == sideLength - 1)
            {
                return map[col, row];
            }
            return map[col, row] + Neighbours(col, row).Min(x => FindPath(x.col, x.row));
        }

        public IEnumerable<(int col, int row)> Neighbours(int col, int row)
        {
            if (col + 1 < sideLength)
            {
                yield return (col + 1, row);
            }
            if (row + 1 < sideLength)
            {
                yield return (col, row + 1);
            }
            /*
            if (col > 1)
            {
                yield return (col - 1, row);
            }
            if (row > 1)
            {
                yield return (col, row - 1);
            }/**/
        }

        public long B(List<int> inputs)
        {
            return default;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.SelectMany(x => x.Select(c => int.Parse(c.ToString()))).ToList();
        }
    }
}
