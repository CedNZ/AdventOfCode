using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day16 : IDay<List<char>>
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

            return AStar(start, end, walls, empty);
        }

        private long AStar((int x, int y) start, (int x, int y) end, HashSet<(int x, int y)> walls, HashSet<(int x, int y)> empty)
        {
            var startNode = new Node(start, Direction.Right, 0);
            Dictionary<(int x, int y), Node> visited = [];
            List<Node> openSet = [startNode];
            Dictionary<Node, Node> cameFrom = [];
            Dictionary<Node, long> gScore = [];
            gScore.Add(startNode, startNode.cost);
            long GScore(Node n) => gScore.TryGetValue(n, out var gscore) ? gscore : long.MaxValue;
            Dictionary<(int x, int y), long> hMem = [];
            long h(Node n)
            {
                if (hMem.TryGetValue(n.position, out var h))
                {
                    return n.cost + h;
                }

                var rise = Math.Abs(end.y - n.position.y);
                var run = Math.Abs(end.x - n.position.x);

                var m = rise * 1.0 / run;
                var turns = rise / m;

                h = (1001 * (int)turns);
                hMem[n.position] = h;
                return n.cost + h;
            };
            Dictionary<Node, long> fScore = [];
            fScore.Add(startNode, h(startNode));

            while (openSet.Count > 0)
            {
                var current = openSet.First();

                if (visited.TryGetValue(current.position, out var v))
                {
                    if (v.cost > current.cost)
                    {
                        visited[current.position] = current;
                    }
                    else
                    {
                        var currentStraight = current;
                        var nextStraight = current;
                        do
                        {
                            currentStraight = nextStraight;
                            nextStraight = Neighbours(currentStraight).Single(n => n.direction == current.direction);
                        } while (walls.Contains(nextStraight.position) is false);

                        var visitedStraight = v;
                        var nVisitedStraight = v;
                        do
                        {
                            visitedStraight = nVisitedStraight;
                            nVisitedStraight = Neighbours(visitedStraight).SingleOrDefault(n => n.direction == current.direction);
                        } while (nVisitedStraight is not null && walls.Contains(nVisitedStraight.position) is false);

                        if (currentStraight.cost < visitedStraight.cost)
                        {
                            visited[current.position] = current;
                            openSet.Remove(v);
                        }
                        else
                        {
                            openSet.Remove(current);
                            continue;
                        }
                    }
                }
                visited[current.position] = current;

                var prev = current;
                var loop = false;
                do
                {
                    var (pos, d, c) = prev;
                    if (prev != current && current.position == pos)
                    {
                        openSet.Remove(current);
                        loop = true;
                        break;
                    }
                }
                while (cameFrom.TryGetValue(prev, out prev));
                if (loop)
                {
                    continue;
                }


                if (current.position == end)
                {
                    return current.cost;
                }
                openSet.Remove(current);
                foreach (var neighbour in Neighbours(current))
                {
                    if (walls.Contains(neighbour.position))
                    {
                        continue;
                    }
                    var tentativeGScore = neighbour.cost;
                    if (tentativeGScore < GScore(neighbour))
                    {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = tentativeGScore;
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
                openSet = openSet
                    .OrderBy(x => x.cost)
                    .ToList();
            }
            return 0;
        }

        private IEnumerable<Node> Neighbours(Node n)
        {
            return n.direction switch
            {
                Direction.Up =>
                [
                    n with
                    {
                        position = (n.position.x, n.position.y - 1),
                        direction = Direction.Up,
                        cost = n.cost + 1
                    },
                    n with
                    {
                        position = (n.position.x - 1, n.position.y),
                        direction = Direction.Left,
                        cost = n.cost + 1001
                    },
                    n with
                    {
                        position = (n.position.x + 1, n.position.y),
                        direction = Direction.Right,
                        cost = n.cost + 1001
                    },
                ],
                Direction.Down =>
                [
                    n with
                    {
                        position = (n.position.x, n.position.y + 1),
                        direction = Direction.Down,
                        cost = n.cost + 1
                    },
                    n with
                    {
                        position = (n.position.x - 1, n.position.y),
                        direction = Direction.Left,
                        cost = n.cost + 1001
                    },
                    n with
                    {
                        position = (n.position.x + 1, n.position.y),
                        direction = Direction.Right,
                        cost = n.cost + 1001
                    },
                ],
                Direction.Left =>
                [
                    n with
                    {
                        position = (n.position.x - 1, n.position.y),
                        direction = Direction.Left,
                        cost = n.cost + 1
                    },
                    n with
                    {
                        position = (n.position.x, n.position.y - 1),
                        direction = Direction.Up,
                        cost = n.cost + 1001
                    },
                    n with
                    {
                        position = (n.position.x, n.position.y + 1),
                        direction = Direction.Down,
                        cost = n.cost + 1001
                    },
                ],
                Direction.Right =>
                [
                    n with
                    {
                        position = (n.position.x + 1, n.position.y),
                        direction = Direction.Right,
                        cost = n.cost + 1
                    },
                    n with
                    {
                        position = (n.position.x, n.position.y - 1),
                        direction = Direction.Up,
                        cost = n.cost + 1001
                    },
                    n with
                    {
                        position = (n.position.x, n.position.y + 1),
                        direction = Direction.Down,
                        cost = n.cost + 1001
                    },
                ],
                _ => throw new NotImplementedException(),
            };
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private record Node((int x, int y) position, Direction direction, long cost);

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

            return AStarB(start, end, walls, empty);
        }

        private long AStarB((int x, int y) start, (int x, int y) end, HashSet<(int x, int y)> walls, HashSet<(int x, int y)> empty)
        {
            var startNode = new Node(start, Direction.Right, 0);
            Dictionary<(int x, int y), Node> visited = [];
            List<Node> openSet = [startNode];
            Dictionary<Node, Node> cameFrom = [];
            Dictionary<Node, long> gScore = [];
            gScore.Add(startNode, startNode.cost);
            long GScore(Node n) => gScore.TryGetValue(n, out var gscore) ? gscore : long.MaxValue;
            Dictionary<(int x, int y), long> hMem = [];
            long h(Node n)
            {
                if (hMem.TryGetValue(n.position, out var h))
                {
                    return n.cost + h;
                }

                var rise = Math.Abs(end.y - n.position.y);
                var run = Math.Abs(end.x - n.position.x);

                var m = rise * 1.0 / run;
                var turns = rise / m;

                h = (1001 * (int)turns);
                hMem[n.position] = h;
                return n.cost + h;
            };
            Dictionary<Node, long> fScore = [];
            fScore.Add(startNode, h(startNode));



            while (openSet.Count > 0)
            {
                var current = openSet.First();

                if (visited.TryGetValue(current.position, out var v))
                {
                    if (v.cost > current.cost)
                    {
                        visited[current.position] = current;
                    }
                    else
                    {
                        var currentStraight = current;
                        var nextStraight = current;
                        do
                        {
                            currentStraight = nextStraight;
                            nextStraight = Neighbours(currentStraight).Single(n => n.direction == current.direction);
                        } while (walls.Contains(nextStraight.position) is false);

                        var visitedStraight = v;
                        var nVisitedStraight = v;
                        do
                        {
                            visitedStraight = nVisitedStraight;
                            nVisitedStraight = Neighbours(visitedStraight).SingleOrDefault(n => n.direction == current.direction);
                        } while (nVisitedStraight is not null && walls.Contains(nVisitedStraight.position) is false);

                        if (currentStraight.cost < visitedStraight.cost)
                        {
                            visited[current.position] = current;
                            openSet.Remove(v);
                        }
                        else
                        {
                            openSet.Remove(current);
                            continue;
                        }
                    }
                }
                visited[current.position] = current;

                var prev = current;
                var loop = false;
                do
                {
                    var (pos, d, c) = prev;
                    if (prev != current && current.position == pos)
                    {
                        openSet.Remove(current);
                        loop = true;
                        break;
                    }
                }
                while (cameFrom.TryGetValue(prev, out prev));
                if (loop)
                {
                    continue;
                }


                if (current.position == end)
                {
                    return current.cost;
                }
                openSet.Remove(current);
                foreach (var neighbour in Neighbours(current))
                {
                    if (walls.Contains(neighbour.position))
                    {
                        continue;
                    }
                    var tentativeGScore = neighbour.cost;
                    if (tentativeGScore < GScore(neighbour))
                    {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = tentativeGScore;
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
                openSet = openSet
                    .OrderBy(x => x.cost)
                    .ToList();
            }
            return 0;
        }

        public List<List<char>> SetupInputs(string[] inputs)
        {
            return inputs.Select(i => i.ToList()).ToList();
        }
    }
}
