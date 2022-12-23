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
        int columns;
        int rows;

        public long A(List<List<int>> inputs)
        {
            SetupInputs(inputs);
            return AstarFind((0, 0), (columns - 1, rows - 1));
        }

        public void SetupInputs(List<List<int>> inputs)
        {
            rows = inputs.Count;
            columns = inputs.First().Count;

            map = new int[rows, columns];
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
        }

        public int AstarFind((int col, int row) start, (int col, int row) end, int mapMulti = 1)
        {
            List<(int col, int row)> openSet = new();
            openSet.Add(start);

            Dictionary<(int col, int row), (int col, int row)> cameFrom = new();

            Dictionary<(int col, int row), int> gScore = new();
            gScore.Add(start, 0);

            int GScore((int col, int row) x) => gScore.TryGetValue(x, out var gscore) ? gscore : 100_000_000;
            int h((int col, int row) x) => (((columns * mapMulti - x.col) * (rows * mapMulti - x.row))) + (GScore(x) * 1000);

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
                foreach (var neighbour in Neighbours(current.col, current.row, mapMulti))
                {
                    var tentativeScore = GScore(current) + Map(neighbour);
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

        public int Map((int col, int row) position)
        {
            if (position.col < columns && position.row < rows)
            {
                return map[position.row, position.col];
            }

            var colOffset = position.col / columns;
            var rowOffset = position.row / rows;

            var value = map[position.row % rows, position.col % columns] + colOffset + rowOffset;

            return (value < 10 ? value : (value - 1) % 9 + 1);
        }

        public IEnumerable<(int col, int row)> Neighbours(int col, int row, int gridMulti = 1)
        {            
            if (row + 1 < rows * gridMulti)
            {
                yield return (col, row + 1);
            }

            if (col + 1 < columns * gridMulti)
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
            if (map == null)
            {
                SetupInputs(inputs);
            }
            int mapMulti = 5;
            return AstarFind((0, 0), (columns * mapMulti - 1, rows * mapMulti - 1), mapMulti);
        }

        public List<List<int>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Select(c => int.Parse(c.ToString())).ToList()).ToList();
        }
    }
}
