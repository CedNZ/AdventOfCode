using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day9 : IDay<int[]>
    {
        public long A(List<int[]> inputs)
        {
            List<((int, int), int)> lowPoints = new();

            for (int i = 0; i < inputs.Count; i++)
            {
                var previousRow = i > 0 ? inputs[i - 1] : Enumerable.Repeat(9, inputs[i].Count()).ToArray();
                var currentRow = inputs[i];
                var nextRow = i < inputs.Count - 1 ? inputs[i + 1] : Enumerable.Repeat(9, inputs[i].Count()).ToArray();

                for (int j = 0; j < currentRow.Count(); j++)
                {
                    var cell = currentRow[j];
                    var north = previousRow[j];
                    var south = nextRow[j];
                    var east = j < currentRow.Count() - 1 ? currentRow[j + 1] : 9;
                    var west = j > 0 ? currentRow[j - 1] : 9;

                    if (cell < north
                        && cell < south
                        && cell < east
                        && cell < west)
                    {
                        lowPoints.Add(((i, j), cell));
                    }
                }
            }

            return lowPoints.Sum(x => x.Item2 + 1);
        }

        public long B(List<int[]> inputs)
        {
            return default;
        }

        public List<int[]> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToList();
        }
    }
}
