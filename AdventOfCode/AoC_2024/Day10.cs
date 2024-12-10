using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day10 : IDay<List<int>>
    {
        public long A(List<List<int>> inputs)
        {
            Dictionary<int, List<(int x, int y)>> heightPositions = [];
            heightPositions.Add(0, []);
            heightPositions.Add(1, []);
            heightPositions.Add(2, []);
            heightPositions.Add(3, []);
            heightPositions.Add(4, []);
            heightPositions.Add(5, []);
            heightPositions.Add(6, []);
            heightPositions.Add(7, []);
            heightPositions.Add(8, []);
            heightPositions.Add(9, []);

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    heightPositions[inputs[y][x]].Add((x, y));
                }
            }

            var trailHeads = heightPositions[0];

            return trailHeads.Sum(t => Path(t, 0, []));

            IEnumerable<(int x, int y)> neighbours((int x, int y) position, int num)
            {
                var test = new List<(int x, int y)>
                {
                    (position.x - 1, position.y),
                    (position.x + 1, position.y),
                    (position.x, position.y - 1),
                    (position.x, position.y + 1),
                };

                return test.Where(x => x.x >= 0 && x.x < inputs[0].Count && x.y >= 0 && x.y < inputs.Count && inputs[x.y][x.x] == num + 1);
            }

            int Path((int x, int y) position, int num, HashSet<(int x, int y)> visited)
            {
                if (visited.Contains(position))
                {
                    return 0;
                }
                visited.Add(position);
                if (num == 9)
                {
                    return 1;
                }
                var test = neighbours(position, num);
                if (test.Count() == 0)
                {
                    return 0;
                }
                return test.Sum(t => Path(t, num + 1, visited));
            }
        }

        public long B(List<List<int>> inputs)
        {
            Dictionary<int, List<(int x, int y)>> heightPositions = [];
            heightPositions.Add(0, []);
            heightPositions.Add(1, []);
            heightPositions.Add(2, []);
            heightPositions.Add(3, []);
            heightPositions.Add(4, []);
            heightPositions.Add(5, []);
            heightPositions.Add(6, []);
            heightPositions.Add(7, []);
            heightPositions.Add(8, []);
            heightPositions.Add(9, []);

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    heightPositions[inputs[y][x]].Add((x, y));
                }
            }

            var trailHeads = heightPositions[0];

            return trailHeads.Sum(t => Path(t, 0));

            IEnumerable<(int x, int y)> neighbours((int x, int y) position, int num)
            {
                var test = new List<(int x, int y)>
                {
                    (position.x - 1, position.y),
                    (position.x + 1, position.y),
                    (position.x, position.y - 1),
                    (position.x, position.y + 1),
                };

                return test.Where(x => x.x >= 0 && x.x < inputs[0].Count && x.y >= 0 && x.y < inputs.Count && inputs[x.y][x.x] == num + 1);
            }

            int Path((int x, int y) position, int num)
            {
                if (num == 9)
                {
                    return 1;
                }
                var test = neighbours(position, num);
                if (test.Count() == 0)
                {
                    return 0;
                }
                return test.Sum(t => Path(t, num + 1));
            }
        }

        public List<List<int>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Select(c => c - '0').ToList()).ToList();
        }
    }
}
