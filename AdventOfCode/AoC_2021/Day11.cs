using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day11 : IDay<int>
    {
        public long A(List<int> inputs)
        {
            var squareSize = 10;
            int[,] octopus = new int[squareSize, squareSize];

            inputs.Select((x, i) => octopus[i / squareSize, i % squareSize] = x).ToList();

            long flashCount = 0;

            List<(int, int)> flashed;
            for (int day = 0; day < 100; day++)
            {
                flashed = new();
                for (int i = 0; i < squareSize; i++)
                {
                    for (int j = 0; j < squareSize; j++)
                    {
                        if (octopus[i, j] >= 0)
                        {
                            octopus[i, j]++;
                            if (octopus[i, j] > 9)
                            {
                                octopus[i, j] = -1;
                                flashed.Add((i, j));
                            }
                        }
                    }
                }

                for (int i = 0; i < flashed.Count; i++)
                {
                    var newFlashed = FlashNeighbours(flashed[i], ref octopus);
                    flashed.AddRange(newFlashed);
                }

                flashCount += flashed.Count;
                for (int i = 0; i < squareSize; i++)
                {
                    for (int j = 0; j < squareSize; j++)
                    {
                        if (octopus[i, j] < 0)
                        {
                            octopus[i, j] = 0;
                        }
                    }
                }
            }

            return flashCount;
        }

        private List<(int i, int j)> FlashNeighbours((int i, int j) index, ref int[,] octopus)
        {
            List<(int i, int j)> flashed = new List<(int i, int j)>();

            for (int i = index.i - 1; i <= index.i + 1; i++)
            {
                if (i >= 0 && i < octopus.GetLength(0))
                {
                    for (int j = index.j - 1; j <= index.j + 1; j++)
                    {
                        if (j >= 0 && j < octopus.GetLength(0))
                        {
                            try
                            {
                                if (octopus[i, j] != 0)
                                {
                                    octopus[i, j]++;
                                    if (octopus[i, j] > 9)
                                    {
                                        octopus[i, j] = 0;
                                        flashed.Add((i, j));
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                }
            }

            return flashed;
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
