using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day5 : IDay<(Point, Point)>
    {
        public long A(List<(Point, Point)> inputs)
        {
            var lines = inputs.Where(x => LineIsHorizontalOrVertical(x)).ToList();

            var map = new int[1000, 1000];

            foreach (var line in lines)
            {
                DrawLine(ref map, line);
            }

            var intersectPointCount = 0;
            foreach (var item in map)
            {
                if (item >= 2)
                {
                    intersectPointCount++;
                }
            }

            return intersectPointCount;
        }

        public long B(List<(Point, Point)> inputs)
        {
            var map = new int[1000, 1000];

            foreach (var line in inputs)
            {
                DrawLine(ref map, line);
            }

            long intersectPointCount = 0;
            foreach (var item in map)
            {
                if (item >= 2)
                {
                    intersectPointCount++;
                }
            }

            return intersectPointCount;
        }

        public List<(Point, Point)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x =>
            {
                var ends = x.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var start = ends[0].Split(',').Select(int.Parse).ToArray();
                var end = ends[1].Split(',').Select(int.Parse).ToArray();
                return (new Point(start[0], start[1]), new Point(end[0], end[1]));
            }).ToList();
        }

        public bool LineIsHorizontalOrVertical((Point a, Point b) points)
        {
            return (points.a.X == points.b.X) || (points.a.Y == points.b.Y);
        }

        public void DrawLine(ref int[,] map, (Point, Point) line)
        {
            if (line.Item1.X == line.Item2.X) // Vertical line
            {
                var minY = Math.Min(line.Item1.Y, line.Item2.Y);
                var maxY = Math.Max(line.Item1.Y, line.Item2.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    map[line.Item1.X, y]++;
                }
            }
            else if (line.Item1.Y == line.Item2.Y)
            {
                var minX = Math.Min(line.Item1.X, line.Item2.X);
                var maxX = Math.Max(line.Item1.X, line.Item2.X);

                for (int x = minX; x <= maxX; x++)
                {
                    map[x, line.Item1.Y]++;
                }
            }
            else
            {
                for (Point p = line.Item1; !p.Equals(line.Item2); )
                {
                    map[p.X, p.Y]++;

                    if (p.X > line.Item2.X)
                    {
                        p.X--;
                    }
                    else
                    {
                        p.X++;
                    }

                    if (p.Y > line.Item2.Y)
                    {
                        p.Y--;
                    }
                    else
                    {
                        p.Y++;
                    }
                }
            }
        }
    }
}
