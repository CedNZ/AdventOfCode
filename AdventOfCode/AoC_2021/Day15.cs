using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day15 : IDay<List<int>>
    {
        int[,] map;

        public long A(List<List<int>> inputs)
        {
            map = new int[inputs.Count, inputs.First().Count];
            var c = 0;
            var r = 0;
            foreach (var row in inputs)
            {
                foreach (var cell in row)
                {
                    map[r, c++] = cell;
                }
                c = 0;
                r++;
            }

            return AstarFind();
        }

        public int AstarFind()
        {
            var start = (0, 0);
            var end = (map.GetLength(1) - 1, map.GetLength(0) - 1);

            List<(int col, int row)> openSet = new();
            openSet.Add(start);

            Dictionary<(int col, int row), (int col, int row)> cameFrom = new();

            Dictionary<(int col, int row), int> gScore = new();
            gScore.Add(start, 0);

            int GScore((int, int) x) => gScore.TryGetValue(x, out var gscore) ? gscore : 100_000_000;
            int h((int, int) x) => (((map.GetLength(0) - x.Item1) * (map.GetLength(1) - x.Item2))) + (GScore(x) * 100000);

            Dictionary<(int col, int row), int> fScore = new();
            fScore.Add(start, h(start));


            while (openSet.Count > 0)
            {
                var current = openSet.OrderBy(n => fScore[n]).First();
                if (current == end)
                {
                    return gScore[current];
                }

                openSet.Remove(current);
                foreach (var neighbour in Neighbours(current.col, current.row))
                {
                    var tentativeScore = GScore(current) + map[neighbour.row, neighbour.col];
                    if (tentativeScore < GScore(neighbour))
                    {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = tentativeScore;
                        fScore[neighbour] = tentativeScore + h(neighbour);
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            return -1;
        }

        public IEnumerable<(int col, int row)> Neighbours(int col, int row)
        {            
            if (row + 1 < map.GetLength(0))
            {
                yield return (col, row + 1);
            }

            if (col + 1 < map.GetLength(1))
            {
                yield return (col + 1, row);
            }

            
            if (col > 0)
            {
                yield return (col - 1, row);
            }
            if (row > 0)
            {
                yield return (col, row - 1);
            }/**/
        }

        public long B(List<List<int>> inputs)
        {
            return default;
        }

        public List<List<int>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Select(c => int.Parse(c.ToString())).ToList()).ToList();
        }
    }
}
