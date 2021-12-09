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

            FindLowPoints(inputs, lowPoints);

            return lowPoints.Sum(x => x.Item2 + 1);
        }

        private static void FindLowPoints(List<int[]> inputs, List<((int, int), int)> lowPoints)
        {
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
        }



        List<int[]> heightMap = new();
        List<(int, int)> basin = new();

        public long B(List<int[]> inputs)
        {
            heightMap = inputs.ToList();
            List<((int, int), int)> lowPoints = new();

            FindLowPoints(inputs, lowPoints);

            List<int> basinSize = new();

            foreach (var lowPoint in lowPoints)
            {
                basin = new();

                var i = lowPoint.Item1.Item1;
                var j = lowPoint.Item1.Item2;

                basinSize.Add(InBasin(i, j));
            }


            return basinSize.OrderByDescending(x => x).Take(3).Aggregate(1, (x, y) => x * y);
        }

        public int InBasin(int i, int j)
        {
            if (basin.Contains((i, j)))
            {
                return 0; //already counted
            }
            if (i >= 0 && i < heightMap.Count() && j >= 0 && j < heightMap[0].Count())
            {
                if (heightMap[i][j] < 9)
                {
                    basin.Add((i, j));
                    return 1 +
                        InBasin(i - 1, j)
                        + InBasin(i + 1, j)
                        + InBasin(i, j - 1)
                        + InBasin(i, j + 1);
                }
            }
            return 0;
        }

        public List<int[]> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToList();
        }
    }
}
