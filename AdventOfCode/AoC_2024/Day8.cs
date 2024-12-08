using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day8 : IDay<List<char>>
    {
        public long A(List<List<char>> inputs)
        {
            var minX = 0;
            var maxX = inputs.Max(x => x.Count);
            var minY = 0;
            var maxY = inputs.Count;
            Dictionary<char, List<(int x, int y)>> antennas = [];
            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    if (inputs[y][x] == '.')
                    {
                        continue;
                    }
                    if (antennas.TryGetValue(inputs[y][x], out List<(int x, int y)>? value))
                    {
                        value.Add((x, y));
                    }
                    else
                    {
                        antennas[inputs[y][x]] = [(x, y)];
                    }
                }
            }

            var antiNodes = new HashSet<(int x, int y)>();

            foreach (var antenna in antennas)
            {
                var positions = antenna.Value;
                foreach (var pair in positions.CartesianPairs())
                {
                    var (x1, y1) = pair.Item1;
                    var (x2, y2) = pair.Item2;
                    var diffX = x1 - x2;
                    var diffY = y1 - y2;

                    var p = (x1 - diffX, y1 - diffY);
                    if (p != pair.Item1 && p != pair.Item2 && p.Item1 >= minX && p.Item1 < maxX && p.Item2 >= minY && p.Item2 < maxY)
                    {
                        antiNodes.Add(p);
                    }
                    p = (x1 + diffX, y1 + diffY);
                    if (p != pair.Item1 && p != pair.Item2 && p.Item1 >= minX && p.Item1 < maxX && p.Item2 >= minY && p.Item2 < maxY)
                    {
                        antiNodes.Add(p);
                    }
                    p = (x2 - diffX, y2 - diffY);
                    if (p != pair.Item1 && p != pair.Item2 && p.Item1 >= minX && p.Item1 < maxX && p.Item2 >= minY && p.Item2 < maxY)
                    {
                        antiNodes.Add(p);
                    }
                    p = (x2 + diffX, y2 + diffY);
                    if (p != pair.Item1 && p != pair.Item2 && p.Item1 >= minX && p.Item1 < maxX && p.Item2 >= minY && p.Item2 < maxY)
                    {
                        antiNodes.Add(p);
                    }
                }
            }

            return antiNodes.Count;
        }

        public long B(List<List<char>> inputs)
        {
            var minX = 0;
            var maxX = inputs.Max(x => x.Count);
            var minY = 0;
            var maxY = inputs.Count;
            Dictionary<char, List<(int x, int y)>> antennas = [];
            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    if (inputs[y][x] == '.')
                    {
                        continue;
                    }
                    if (antennas.TryGetValue(inputs[y][x], out List<(int x, int y)>? value))
                    {
                        value.Add((x, y));
                    }
                    else
                    {
                        antennas[inputs[y][x]] = [(x, y)];
                    }
                }
            }

            var antiNodes = new HashSet<(int x, int y)>();

            foreach (var antenna in antennas)
            {
                var positions = antenna.Value;
                foreach (var pair in positions.CartesianPairs())
                {
                    var (x1, y1) = pair.Item1;
                    var (x2, y2) = pair.Item2;
                    var diffX = x1 - x2;
                    var diffY = y1 - y2;

                    var p = pair.Item1;
                    while (p.x >= minX && p.x < maxX && p.y >= minY && p.y < maxY)
                    {
                        antiNodes.Add(p);
                        p = (p.x - diffX, p.y - diffY);
                    }
                    p = pair.Item1;
                    while (p.x >= minX && p.x < maxX && p.y >= minY && p.y < maxY)
                    {
                        antiNodes.Add(p);
                        p = (p.x + diffX, p.y + diffY);
                    }
                }
            }

            return antiNodes.Count;
        }

        public List<List<char>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.ToList()).ToList();
        }
    }
}
