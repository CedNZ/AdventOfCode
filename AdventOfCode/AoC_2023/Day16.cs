using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day16 : IDay<Mirror>
    {
        public long A(List<Mirror> inputs)
        {
            var grid = inputs.ToDictionary(m => (m.X, m.Y), m => m);
            Dictionary<Direction, (int dx, int dy)> deltas = new()
            {
                {
                    Direction.Up,
                    (0, -1)
                },
                {
                    Direction.Down,
                    (0, 1)
                },
                {
                    Direction.Left,
                    (-1, 0)
                },
                {
                    Direction.Right,
                    (1, 0)
                },
            };

            var maxX = inputs.Max(s => s.X);
            var maxY = inputs.Max(s => s.Y);

            var start = new Light
            {
                X = 0,
                Y = 0,
                Direction = Direction.Right,
            };

            Queue<Light> queue = new Queue<Light>();

            HashSet<Light> visited = new HashSet<Light>();

            queue.Enqueue(start);

            while (queue.TryDequeue(out var light))
            {
                //for (int y = 0; y <= maxY; y++)
                //{
                //    for (int x = 0; x <= maxX; x++)
                //    {
                //        if (grid.TryGetValue((x, y), out var m))
                //        {
                //            Console.Write(m.C);
                //        }
                //        else
                //        {
                //            var v = visited.FirstOrDefault(c => c.X == x && c.Y == y);
                //            if (v is not null)
                //            {
                //                Console.Write("X");
                //            }
                //            else
                //            {
                //                Console.Write(" ");
                //            }
                //        }

                //    }
                //    Console.WriteLine();
                //}

                //Console.WriteLine();
                //Console.WriteLine();

                if (visited.Contains(light))
                {
                    continue;
                }
                visited.Add(light);
                if (grid.TryGetValue((light.X, light.Y), out var mirror))
                {
                    var steps = mirror.GetDirections(light.Direction);
                    var (dx, dy) = deltas[steps.Item1];
                    var newLight = light with
                    {
                        X = light.X + dx,
                        Y = light.Y + dy,
                        Direction = steps.Item1,
                    };
                    if (newLight.X >= 0 && newLight.Y >= 0
                        && newLight.X <= maxX && newLight.Y <= maxY)
                    {
                        queue.Enqueue(newLight);
                    }
                    if (steps.Item2 is not null)
                    {
                        (dx, dy) = deltas[steps.Item2.Value];
                        newLight = light with
                        {
                            X = light.X + dx,
                            Y = light.Y + dy,
                            Direction = steps.Item2.Value,
                        };
                        if (newLight.X >= 0 && newLight.Y >= 0
                            && newLight.X <= maxX && newLight.Y <= maxY)
                        {
                            queue.Enqueue(newLight);
                        }
                    }
                }
                else
                {
                    var (dx, dy) = deltas[light.Direction];
                    var newLight = light with
                    {
                        X = light.X + dx,
                        Y = light.Y + dy,
                    };
                    if (newLight.X >= 0 && newLight.Y >= 0
                        && newLight.X <= maxX && newLight.Y <= maxY)
                    {
                        queue.Enqueue(newLight);
                    }
                }
            }

            return visited.DistinctBy(v => new { v.X, v.Y }).Count();
        }

        public long B(List<Mirror> inputs)
        {
            throw new NotImplementedException();
        }

        public List<Mirror> SetupInputs(string[] inputs)
        {
            return inputs.SelectMany((l, y) =>
                l.Select((c, x) =>
                    new Mirror(x, y, c))
                .Where(m => m.C != '.')
                .ToList())
            .ToList();
        }
    }

    public record Light
    {
        public int X { get; init; }
        public int Y { get; init; }
        public Direction Direction { get; init; }
    }

    public class Mirror
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char C { get; set; }

        public Mirror(int x, int y, char c)
        {
            X = x;
            Y = y;
            C = c;
        }

        public (Direction, Direction?) GetDirections(Direction from)
        {
            return from switch
            {
                Direction.Up => C switch
                {
                    '/' => (Direction.Right, null),
                    '\\' => (Direction.Left, null),
                    '-' => (Direction.Right, Direction.Left),
                    '|' => (Direction.Up, null),
                    _ => throw new Exception("Invalid mirror"),
                },
                Direction.Down => C switch
                {
                    '/' => (Direction.Left, null),
                    '\\' => (Direction.Right, null),
                    '-' => (Direction.Left, Direction.Right),
                    '|' => (Direction.Down, null),
                    _ => throw new Exception("Invalid mirror"),
                },
                Direction.Left => C switch
                {
                    '/' => (Direction.Down, null),
                    '\\' => (Direction.Up, null),
                    '-' => (Direction.Left, null),
                    '|' => (Direction.Up, Direction.Down),
                    _ => throw new Exception("Invalid mirror"),
                },
                Direction.Right => C switch
                {
                    '/' => (Direction.Up, null),
                    '\\' => (Direction.Down, null),
                    '-' => (Direction.Right, null),
                    '|' => (Direction.Down, Direction.Up),
                    _ => throw new Exception("Invalid mirror"),
                },
                _ => throw new Exception("Invalid direction"),
            };
        }

        public override string ToString()
        {
            return $"{X},{Y} {C}";
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
