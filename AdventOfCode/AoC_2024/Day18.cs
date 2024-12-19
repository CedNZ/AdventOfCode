using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day18 : IDayOut<(int x, int y), string>
    {
        public string A(List<(int x, int y)> inputs)
        {
            HashSet<(int x, int y)> bytes = [];

            var start = (0, 0);
            var end = (70, 70);

            for (int i = 0; i < 1024; i++)
            {
                var (x, y) = inputs[i];
                bytes.Add((x, y));
            }

            return AstarFind(start, end, bytes).ToString();
        }

        public int AstarFind((int x, int y) start, (int x, int y) end, HashSet<(int x, int y)> bytes)
        {
            List<Node> openSet = [];
            var startNode = new Node(start.x, start.y, 0);
            var endNode = new Node(end.x, end.y, int.MaxValue);
            openSet.Add(startNode);

            Dictionary<Node, Node> cameFrom = [];
            HashSet<(int x, int y)> visited = [];

            Dictionary<Node, int> gScore = new()
            {
                { startNode, startNode.Steps }
            };

            int GScore(Node n) => gScore.TryGetValue(n, out var gscore) ? gscore : n.Steps;
            int h(Node n) => Math.Abs(end.x - n.X) + Math.Abs(end.y - n.Y) + GScore(n) + n.Steps;

            Dictionary<Node, int> fScore = new()
            {
                { startNode, h(startNode) }
            };

            while (openSet.Count > 0)
            {
                var current = openSet.OrderBy(n => fScore[n]).First();
                if (current.P == end)
                {
                    return gScore[current];
                }

                visited.Add(current.P);
                openSet.Remove(current);
                var neighbours = Neighbours(current)
                    .Where(n => visited.Contains(n.P) is false
                        && bytes.Contains((n.X, n.Y)) is false
                        && n.X >= 0 && n.X <= end.x
                        && n.Y >= 0 && n.Y <= end.y)
                    .ToList();
                foreach (var neighbour in neighbours)
                {
                    var tentativeScore = GScore(current);
                    if (tentativeScore < GScore(neighbour))
                    {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = neighbour.Steps;
                        fScore[neighbour] = neighbour.Steps + h(neighbour);
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            return -1;
        }

        IEnumerable<Node> Neighbours(Node current)
        {
            yield return current with
            {
                Steps = current.Steps + 1,
                X = current.X + 1,
                Y = current.Y,
            };
            yield return current with
            {
                Steps = current.Steps + 1,
                X = current.X - 1,
                Y = current.Y,
            };
            yield return current with
            {
                Steps = current.Steps + 1,
                X = current.X,
                Y = current.Y + 1,
            };
            yield return current with
            {
                Steps = current.Steps + 1,
                X = current.X,
                Y = current.Y - 1,
            };
        }

        record Node
        {
            public int X { get; init; }
            public int Y { get; init; }
            public int Steps { get; init; }

            public (int x, int y) P => (X, Y);

            public Node(int x, int y, int steps)
            {
                X = x;
                Y = y;
                Steps = steps;
            }
        }

        public string B(List<(int x, int y)> inputs)
        {
            var low = 0;
            var high = inputs.Count;

            var end = (70, 70);
            var start = (0, 0);
            HashSet<(int x, int y)> bytes = [];
            var index = 0;

            do
            {
                var mid = (low + high) / 2;
                bytes.Clear();
                bytes = inputs.Take(mid).ToHashSet();

                var result = AstarFind(start, end, bytes);

                if (result < 0)
                {
                    index = mid;
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

            } while (low <= high);


            bytes.Clear();
            bytes = inputs.Take(index).ToHashSet();
            AstarFind(start, end, bytes);

            bytes.Clear();
            bytes = inputs.Take(index - 1).ToHashSet();
            AstarFind(start, end, bytes);

            return string.Join(',', inputs[index - 1]);
        }

        public List<(int x, int y)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Split(',').Select(int.Parse))
                .Select(x => (x.First(), x.Last()))
                .ToList();
        }
    }
}
