using System;
using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day6 : IDay<(int x, int y, char c)>
    {
        public long A(List<(int x, int y, char c)> inputs)
        {
            var guardChar = inputs.First(x => x.c == '^');
            var guard = (guardChar.x, guardChar.y, D: Direction.N);
            var grid = new HashSet<(int x, int y)>(inputs.Where(x => x.c == '#').Select(x => (x.x, x.y)));
            var visited = new HashSet<(int x, int y)>();
            var minX = grid.Min(x => x.x);
            var maxX = grid.Max(x => x.x);
            var minY = grid.Min(x => x.y);
            var maxY = grid.Max(x => x.y);
            while (guard.x >= minX && guard.x <= maxX && guard.y >= minY && guard.y <= maxY)
            {
                visited.Add((guard.x, guard.y));
                var next = Next(guard);
                while (grid.Contains((next.x, next.y)))
                {
                    guard.D = guard.D switch
                    {
                        Direction.N => Direction.E,
                        Direction.E => Direction.S,
                        Direction.S => Direction.W,
                        Direction.W => Direction.N,
                        _ => throw new NotImplementedException(),
                    };
                    next = Next(guard);
                }
                guard = next;
            }

            return visited.Count;

            (int x, int y, Direction D) Next((int x, int y, Direction D) current)
            {
                return current.D switch
                {
                    Direction.N => (current.x, current.y - 1, Direction.N),
                    Direction.E => (current.x + 1, current.y, Direction.E),
                    Direction.S => (current.x, current.y + 1, Direction.S),
                    Direction.W => (current.x - 1, current.y, Direction.W),
                    _ => throw new NotImplementedException(),
                };
            }
        }

        public long B(List<(int x, int y, char c)> inputs)
        {
            var guardChar = inputs.First(x => x.c == '^');
            var guard = (guardChar.x, guardChar.y, D: Direction.N);
            var grid = new HashSet<(int x, int y)>(inputs.Where(x => x.c == '#').Select(x => (x.x, x.y)));
            var visited = new HashSet<(int x, int y)>();
            var minX = grid.Min(x => x.x);
            var maxX = grid.Max(x => x.x);
            var minY = grid.Min(x => x.y);
            var maxY = grid.Max(x => x.y);

            while (guard.x >= minX && guard.x <= maxX && guard.y >= minY && guard.y <= maxY)
            {
                visited.Add((guard.x, guard.y));
                var next = Next(guard);
                while (grid.Contains((next.x, next.y)))
                {
                    guard.D = guard.D switch
                    {
                        Direction.N => Direction.E,
                        Direction.E => Direction.S,
                        Direction.S => Direction.W,
                        Direction.W => Direction.N,
                        _ => throw new NotImplementedException(),
                    };
                    next = Next(guard);
                }
                guard = next;
            }

            var looped = 0;

            foreach (var (x, y) in visited)
            {
                if (grid.Contains((x, y)))
                {
                    continue;
                }
                guard = (guardChar.x, guardChar.y, D: Direction.N);
                var tempGrid = grid.ToHashSet();
                var tempVisited = new HashSet<(int x, int y, Direction D)>();
                var steps = 0;
                tempGrid.Add((x, y));
                while (guard.x >= minX && guard.x <= maxX && guard.y >= minY && guard.y <= maxY)
                {
                    if (tempVisited.Contains((guard.x, guard.y, guard.D)))
                    {
                        looped++;
                        break;
                    }
                    tempVisited.Add((guard.x, guard.y, guard.D));
                    var next = Next(guard);
                    while (tempGrid.Contains((next.x, next.y)))
                    {
                        guard.D = guard.D switch
                        {
                            Direction.N => Direction.E,
                            Direction.E => Direction.S,
                            Direction.S => Direction.W,
                            Direction.W => Direction.N,
                            _ => throw new NotImplementedException(),
                        };
                        next = Next(guard);
                        steps++;
                        if (steps > 5000)
                        {
                            looped++;
                            break;
                        }
                    }
                    guard = next;
                }
            }

            return looped;

            (int x, int y, Direction D) Next((int x, int y, Direction D) current)
            {
                return current.D switch
                {
                    Direction.N => (current.x, current.y - 1, Direction.N),
                    Direction.E => (current.x + 1, current.y, Direction.E),
                    Direction.S => (current.x, current.y + 1, Direction.S),
                    Direction.W => (current.x - 1, current.y, Direction.W),
                    _ => throw new NotImplementedException(),
                };
            }
        }

        public List<(int x, int y, char c)> SetupInputs(string[] inputs)
        {
            List<(int x, int y, char c)> grid = [];
            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    if (inputs[y][x] is '#' or '^')
                    {
                        grid.Add((x, y, inputs[y][x]));
                    }
                }
            }
            return grid;
        }

        enum Direction
        {
            N,
            E,
            S,
            W,
        }
    }
}
