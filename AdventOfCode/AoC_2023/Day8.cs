using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day8 : IDay<(string map, HashSet<Node> nodes)>
    {
        public long A(List<(string map, HashSet<Node> nodes)> inputs)
        {
            var (map, nodes) = inputs[0];
            var startNode = nodes.First(n => n.Id == "AAA");
            return GetSteps(map, startNode, "ZZZ");
        }

        private static long GetSteps(string map, Node startNode, string endNode)
        {
            var steps = 0;
            var mapIndex = 0;
            var node = startNode;
            while (node.Id.EndsWith(endNode) is false)
            {
                var direction = map[mapIndex++];
                if (mapIndex == map.Length)
                {
                    mapIndex = 0;
                }
                node = direction switch
                {
                    'L' => node.LeftChild,
                    'R' => node.RightChild,
                    _ => throw new Exception("Invalid direction")
                };
                steps++;
            }
            return steps;
        }

        public long B(List<(string map, HashSet<Node> nodes)> inputs)
        {
            var (map, nodes) = inputs[0];
            var starting = nodes.Where(x => x.Id.EndsWith('A')).ToList();
            var steps = starting.Select(n => GetSteps(map, n, "Z")).ToList();
            var lcm = steps.CalculateLCM();

            return lcm;
        }

        public List<(string map, HashSet<Node> nodes)> SetupInputs(string[] inputs)
        {
            HashSet<Node> nodes = new();
            var map = inputs[0];

            foreach (var input in inputs.Skip(2))
            {
                var parts = input.Split('=', StringSplitOptions.RemoveEmptyEntries);
                var id = parts[0].Trim();
                var children = parts[1]
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToArray();

                var node = nodes.FirstOrDefault(n => n.Id == id)
                    ?? new Node
                    {
                        Id = id,
                    };
                nodes.Add(node);

                var left = nodes.FirstOrDefault(n => n.Id == children[0])
                    ?? new Node
                    {
                        Id = children[0],
                    };
                nodes.Add(left);

                var right = nodes.FirstOrDefault(n => n.Id == children[1])
                    ?? new Node
                    {
                        Id = children[1],
                    };
                nodes.Add(right);

                node.LeftChild = left;
                node.RightChild = right;

            }

            return [(map, nodes)];
        }
    }

    public class Node
    {
        public string Id { get; init; }

        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
