using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day15 : IDay<(HashSet<(int x, int y)> walls, HashSet<(int x, int y)> boxes, (int x, int y) start, string instructions)>
    {
        public long A(List<(HashSet<(int x, int y)> walls, HashSet<(int x, int y)> boxes, (int x, int y) start, string instructions)> inputs)
        {
            var (walls, boxes, start, instructions) = inputs[0];

            var pos = start;


            foreach (var instruction in instructions)
            {
                var next = Move(pos, instruction);
                if (walls.Contains(next))
                {
                    continue;
                }
                if (boxes.Contains(next))
                {
                    var nextBox = next;
                    do
                    {
                        nextBox = Move(nextBox, instruction);
                        if (walls.Contains(nextBox))
                        {
                            break;
                        }
                    }
                    while (boxes.Contains(nextBox));
                    if (walls.Contains(nextBox))
                    {
                        continue;
                    }
                    boxes.Remove(next);
                    boxes.Add(nextBox);
                }
                pos = next;
            }

            return boxes.Sum(b => 100 * b.y + b.x);
        }

        (int x, int y) Move((int x, int y) pos, char direction)
        {
            return direction switch
            {
                '^' => (pos.x, pos.y - 1),
                'v' => (pos.x, pos.y + 1),
                '<' => (pos.x - 1, pos.y),
                '>' => (pos.x + 1, pos.y),
                _ => pos
            };
        }

        public long B(List<(HashSet<(int x, int y)> walls, HashSet<(int x, int y)> boxes, (int x, int y) start, string instructions)> inputs)
        {

            return 0;
        }

        public List<(HashSet<(int x, int y)> walls, HashSet<(int x, int y)> boxes, (int x, int y) start, string instructions)> SetupInputs(string[] inputs)
        {
            var gridLines = inputs.TakeWhile(x => string.IsNullOrEmpty(x) is false).ToList();
            var instructions = inputs.Skip(gridLines.Count + 1).ToList().Aggregate("", (agg, next) => agg + next);

            HashSet<(int x, int y)> walls = [];
            HashSet<(int x, int y)> boxes = [];
            (int, int) start = (0, 0);

            for (int y = 0; y < gridLines.Count; y++)
            {
                for (int x = 0; x < gridLines[y].Length; x++)
                {
                    if (gridLines[y][x] == '#')
                    {
                        walls.Add((x, y));
                    }
                    else if (gridLines[y][x] == 'O')
                    {
                        boxes.Add((x, y));
                    }
                    else if (gridLines[y][x] == '@')
                    {
                        start = (x, y);
                    }
                }
            }

            return [(walls, boxes, start, instructions)];
        }
    }
}
