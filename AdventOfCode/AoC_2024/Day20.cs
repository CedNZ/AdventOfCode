using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day20 : IDay<List<char>>
    {
        public long A(List<List<char>> inputs)
        {
            HashSet<(int x, int y)> walls = [];
            HashSet<(int x, int y)> empty = [];
            (int x, int y) start = (0, 0);
            (int x, int y) end = (0, 0);

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    if (inputs[y][x] == '#')
                    {
                        walls.Add((x, y));
                    }
                    else if (inputs[y][x] == '.')
                    {
                        empty.Add((x, y));
                    }
                    else if (inputs[y][x] == 'S')
                    {
                        start = (x, y);
                    }
                    else if (inputs[y][x] == 'E')
                    {
                        end = (x, y);
                    }
                }
            }

            var xmax = walls.Max(w => w.x);
            var ymax = walls.Max(w => w.y);

            List<(int x, int y)> pathSteps = [];
            Stack<((int, int) position, int steps)> nextPos = [];
            nextPos.Push((end, 0));
            var normalPathLength = 0;
            while (nextPos.Count > 0)
            {
                var (position, steps) = nextPos.Pop();
                pathSteps.Add(position);
                if (position == start)
                {
                    normalPathLength = steps;
                }

                var neighbours = Neighbours(position)
                    .Where(p => p.x >= 0 && p.x <= xmax && p.y >= 0 && p.y <= ymax
                            && pathSteps.Contains(p) is false && walls.Contains(p) is false).ToList();
                foreach (var neighbour in neighbours)
                {
                    nextPos.Push((neighbour, steps + 1));
                }
            }

            Dictionary<((int x, int y) s, (int x, int y) e), int> cheatDict = [];

            for (int i = pathSteps.Count - 1; i >= 0; i--)
            {
                var cheatStart = pathSteps[i];
                var possibleCheatEndPoints = GetPointsWithinManhattanDistance(cheatStart.x, cheatStart.y, 2);
                foreach (var cheatEnd in possibleCheatEndPoints)
                {
                    var nIndex = pathSteps.IndexOf(cheatEnd);
                    var steps = Math.Abs(cheatStart.x - cheatEnd.x) + Math.Abs(cheatStart.y - cheatEnd.y);
                    if (nIndex >= 0 && nIndex < i)
                    {
                        var saved = i - nIndex - steps;
                        if (saved >= 100)
                        {
                            cheatDict[(cheatStart, cheatEnd)] = saved;
                        }
                    }
                }
            }

            return cheatDict.Count;
        }

        IEnumerable<(int x, int y)> Neighbours((int x, int y) current)
        {
            yield return (current.x + 1, current.y);
            yield return (current.x - 1, current.y);
            yield return (current.x, current.y + 1);
            yield return (current.x, current.y - 1);
        }

        public static IEnumerable<(int x, int y)> GetPointsWithinManhattanDistance(int x0, int y0, int D)
        {
            // Manhattan distance region:
            // |x - x0| + |y - y0| <= D

            for (int dx = -D; dx <= D; dx++)
            {
                int maxDy = D - Math.Abs(dx);
                for (int dy = -maxDy; dy <= maxDy; dy++)
                {
                    // At this point, |dx| + |dy| <= D
                    yield return (x0 + dx, y0 + dy);
                }
            }
        }

        public long B(List<List<char>> inputs)
        {
            HashSet<(int x, int y)> walls = [];
            HashSet<(int x, int y)> empty = [];
            (int x, int y) start = (0, 0);
            (int x, int y) end = (0, 0);

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    if (inputs[y][x] == '#')
                    {
                        walls.Add((x, y));
                    }
                    else if (inputs[y][x] == '.')
                    {
                        empty.Add((x, y));
                    }
                    else if (inputs[y][x] == 'S')
                    {
                        start = (x, y);
                    }
                    else if (inputs[y][x] == 'E')
                    {
                        end = (x, y);
                    }
                }
            }

            var xmax = walls.Max(w => w.x);
            var ymax = walls.Max(w => w.y);

            List<(int x, int y)> pathSteps = [];
            Stack<((int, int) position, int steps)> nextPos = [];
            nextPos.Push((end, 0));
            var normalPathLength = 0;
            while (nextPos.Count > 0)
            {
                var (position, steps) = nextPos.Pop();
                pathSteps.Add(position);
                if (position == start)
                {
                    normalPathLength = steps;
                }

                var neighbours = Neighbours(position)
                    .Where(p => p.x >= 0 && p.x <= xmax && p.y >= 0 && p.y <= ymax
                            && pathSteps.Contains(p) is false && walls.Contains(p) is false).ToList();
                foreach (var neighbour in neighbours)
                {
                    nextPos.Push((neighbour, steps + 1));
                }
            }

            Dictionary<((int x, int y) s, (int x, int y) e), int> cheatDict = [];

            for (int i = pathSteps.Count - 1; i >= 0; i--)
            {
                var cheatStart = pathSteps[i];
                var possibleCheatEndPoints = GetPointsWithinManhattanDistance(cheatStart.x, cheatStart.y, 20);
                foreach (var cheatEnd in possibleCheatEndPoints)
                {
                    var nIndex = pathSteps.IndexOf(cheatEnd);
                    var steps = Math.Abs(cheatStart.x - cheatEnd.x) + Math.Abs(cheatStart.y - cheatEnd.y);
                    if (nIndex >= 0 && nIndex < i)
                    {
                        var saved = i - nIndex - steps;
                        if (saved >= 100)
                        {
                            cheatDict[(cheatStart, cheatEnd)] = saved;
                        }
                    }
                }
            }

            return cheatDict.Count;
        }

        public List<List<char>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.ToList()).ToList();
        }
    }
}
