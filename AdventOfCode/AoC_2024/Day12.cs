using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day12 : IDay<List<char>>
    {
        public long A(List<List<char>> inputs)
        {
            Dictionary<char, List<(int x, int y)>> map = [];
            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    var c = inputs[y][x];
                    if (map.ContainsKey(c))
                    {
                        map[c].Add((x, y));
                    }
                    else
                    {
                        map.Add(c, [(x, y)]);
                    }
                }
            }

            return map.Sum(m =>
            {
                var clusters = m.Value.Cluster(x => x,
                    (p1, p2) => (p1.x == p2.x && Math.Abs(p1.y - p2.y) == 1)
                            || (p1.y == p2.y && Math.Abs(p1.x - p2.x) == 1));

                var fence = 0;
                foreach (var shape in clusters)
                {
                    var area = shape.Count;
                    var perimeter = Perimeter(shape);
                    fence += area * perimeter;
                }

                return fence;
            });
        }

        public long B(List<List<char>> inputs)
        {
            Dictionary<char, List<(int x, int y)>> map = [];
            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    var c = inputs[y][x];
                    if (map.ContainsKey(c))
                    {
                        map[c].Add((x, y));
                    }
                    else
                    {
                        map.Add(c, [(x, y)]);
                    }
                }
            }

            return map.Sum(m =>
            {
                var clusters = m.Value.Cluster(x => x,
                    (p1, p2) => (p1.x == p2.x && Math.Abs(p1.y - p2.y) == 1)
                            || (p1.y == p2.y && Math.Abs(p1.x - p2.x) == 1));

                var fence = 0;
                foreach (var shape in clusters)
                {
                    var area = shape.Count;
                    var sides = Sides(shape);

                    var xmin = shape.Min(x => x.x);
                    var xmax = shape.Max(x => x.x);
                    var xdiff = xmax - xmin + 1;
                    var ymin = shape.Min(x => x.y);
                    var ymax = shape.Max(x => x.y);
                    var ydiff = ymax - ymin + 1;

                    HashSet<(int x, int y)> holes = [];
                    if (xdiff >= 3 && ydiff >= 3)
                    {
                        for (int x = xmin + 1; x <= xmax - 1; x++)
                        {
                            for (int y = ymin + 1; y <= ymax - 1; y++)
                            {
                                if (shape.Contains((x, y)) is false)
                                {
                                    if (FindHole(x, y))
                                    {
                                        holes.Add((x, y));
                                    }
                                }
                            }
                        }
                    }

                    HashSet<(int x, int y)> potentialHoles = [];
                    for (int y = ymin; y <= ymax; y++)
                    {
                        var possibleX = Enumerable.Range(xmin, xmax - xmin + 1).Select(x => (x, y));
                        var c2 = shape.ToList();
                        var full = shape.Where(x => possibleX.Contains(x));
                        var fullClusters = full.Cluster(x => x.x, (x1, x2) => Math.Abs(x1 - x2) == 1);
                        if (fullClusters.Count > 1)
                        {
                            var empty = possibleX.Except(full);
                            foreach (var e in empty)
                            {
                                potentialHoles.Add(e);
                            }
                        }
                    }
                    for (int x = xmin; x <= xmax; x++)
                    {
                        var possibleY = Enumerable.Range(ymin, ymax - ymin + 1).Select(y => (x, y));
                        var full = shape.Where(x => possibleY.Contains(x));
                        var fullClusters = full.Cluster(x => x.y, (y1, y2) => Math.Abs(y1 - y2) == 1);
                        if (fullClusters.Count > 1)
                        {
                            var empty = possibleY.Except(full);
                            foreach (var e in empty)
                            {
                                if (potentialHoles.Contains(e))
                                {
                                    holes.Add(e);
                                }
                            }
                        }
                    }


                    if (holes.Any())
                    {
                        var holeClusters = holes.Cluster(x => x,
                            (p1, p2) => (p1.x == p2.x && Math.Abs(p1.y - p2.y) == 1)
                                || (p1.y == p2.y && Math.Abs(p1.x - p2.x) == 1));

                        foreach (var hole in holeClusters)
                        {
                            if (hole.All(h =>
                            {
                                List<(int x, int y)> corners =
                                [
                                    (h.x - 1, h.y - 1), (h.x + 1, h.y - 1),
                            (h.x -1 , h.y + 1), (h.x + 1, h.y + 1),
                        ];

                                var contains = corners.All(c => shape.Contains(c) || hole.Contains(c));
                                return contains;
                            }))
                            {
                                sides += Sides(hole);
                            }
                        }
                    }


                    bool FindHole(int x, int y)
                    {
                        List<(int x, int y)> around =
                        [
                            (x - 1, y - 1), (x, y - 1), (x + 1, y - 1),
                            (x - 1, y),                 (x + 1, y),
                            (x -1 , y + 1), (x, y + 1), (x + 1, y + 1),
                        ];

                        return around.All(a => shape.Contains(a));
                    }


                    fence += area * sides;
                }


                return fence;
            });
        }

        int Sides(List<(int x, int y)> points)
        {
            var occupied = new HashSet<(int x, int y)>(points);
            var visited = new HashSet<Visited>();

            var current = occupied.Min();
            Direction direction = Direction.East;
            Pos pos = Pos.TopLeft;
            var sides = 0;
            var visiting = new Visited(current.x, current.y, direction, pos);
            while (visited.Contains(visiting) is false)
            {
                visited.Add(visiting);

                var moves = Next(visiting);
                foreach (var move in moves)
                {
                    if (occupied.Contains((move.X, move.Y)))
                    {
                        if (move.D != visiting.D)
                        {
                            sides++;
                        }
                        visiting = move;
                        break;
                    }
                }

            }

            return sides;
        }

        IEnumerable<Visited> Next(Visited visiting)
        {
            var d = visiting.D;
            var p = visiting.P;
            if (d is Direction.East)
            {
                if (p is Pos.TopLeft)
                {
                    yield return visiting with
                    {
                        P = Pos.BottomLeft,
                        D = Direction.North,
                        X = visiting.X,
                        Y = visiting.Y - 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopLeft,
                        D = Direction.East,
                        X = visiting.X + 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopRight,
                        D = Direction.South,
                        X = visiting.X,
                        Y = visiting.Y + 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopRight,
                        D = Direction.South,
                        X = visiting.X,
                        Y = visiting.Y,
                    };
                }
            }
            else if (d is Direction.South)
            {
                if (p is Pos.TopRight)
                {
                    yield return visiting with
                    {
                        P = Pos.TopLeft,
                        D = Direction.East,
                        X = visiting.X + 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopRight,
                        D = Direction.South,
                        X = visiting.X,
                        Y = visiting.Y + 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomRight,
                        D = Direction.West,
                        X = visiting.X - 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomRight,
                        D = Direction.West,
                        X = visiting.X,
                        Y = visiting.Y,
                    };
                }
            }
            else if (d is Direction.West)
            {
                if (p is Pos.BottomRight)
                {
                    yield return visiting with
                    {
                        P = Pos.TopRight,
                        D = Direction.South,
                        X = visiting.X,
                        Y = visiting.Y + 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomRight,
                        D = Direction.West,
                        X = visiting.X - 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomLeft,
                        D = Direction.North,
                        X = visiting.X,
                        Y = visiting.Y - 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomLeft,
                        D = Direction.North,
                        X = visiting.X,
                        Y = visiting.Y,
                    };
                }
            }
            else if (d is Direction.North)
            {
                if (p is Pos.BottomLeft)
                {
                    yield return visiting with
                    {
                        P = Pos.BottomRight,
                        D = Direction.West,
                        X = visiting.X - 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.BottomLeft,
                        D = Direction.North,
                        X = visiting.X,
                        Y = visiting.Y - 1,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopLeft,
                        D = Direction.East,
                        X = visiting.X + 1,
                        Y = visiting.Y,
                    };
                    yield return visiting with
                    {
                        P = Pos.TopLeft,
                        D = Direction.East,
                        X = visiting.X,
                        Y = visiting.Y,
                    };
                }
            }
        }

        record Visited(int X, int Y, Direction D, Pos P);

        enum Direction
        {
            North,
            East,
            South,
            West,
        }

        enum Pos
        {
            TopLeft,
            TopRight,
            BottomRight,
            BottomLeft,
        }

        int Perimeter(List<(int x, int y)> points)
        {
            // Put all points into a HashSet for O(1) lookup
            var occupied = new HashSet<(int x, int y)>(points);

            int perimeter = 0;

            // Directions to check neighbors: up, down, left, right
            var directions = new (int dx, int dy)[]
            {
                (0, -1),
                (0,  1),
                (-1, 0),
                (1,  0)
            };

            foreach (var cell in occupied)
            {
                // Check each neighbor
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (cell.x + dx, cell.y + dy);
                    // If neighbor is not occupied, this side contributes to perimeter
                    if (!occupied.Contains(neighbor))
                    {
                        perimeter++;
                    }
                }
            }

            return perimeter;
        }

        public List<List<char>> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.ToList()).ToList();
        }
    }
}
