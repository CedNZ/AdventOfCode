using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day14 : IDay<Stone>
    {
        public long A(List<Stone> inputs)
        {
            var maxX = inputs.Max(s => s.X);
            var maxY = inputs.Max(s => s.Y);
            for (int x = 0; x <= maxX; x++)
            {
                var clusters = inputs.Where(s => s.X == x)
                    .ClusterWhile(s => s.CanRoll);

                foreach (var cluster in clusters)
                {
                    var minY = cluster[0].CanRoll
                                ? 0
                                : cluster[0].Y;
                    _ = cluster.Select((s, i) =>
                    {
                        if (s.CanRoll)
                        {
                            s.Y = minY + i;
                        }
                        return s;
                    }).ToList();
                }
            }

            return inputs.Where(s => s.CanRoll).Select(s => (maxY + 1) - s.Y).Sum();
        }

        public long B(List<Stone> inputs)
        {
            var maxX = inputs.Max(s => s.X);
            var maxY = inputs.Max(s => s.Y);

            var fixedStones = inputs.Where(s => s.CanRoll is false);

            var rows = fixedStones.GroupBy(s => s.X)
                .Select(g =>
                {
                    var row = g.OrderBy(s => s.Y).ToList();
                    row.Insert(0, new Stone
                    {
                        CanRoll = false,
                        X = g.Key,
                        Y = -1,
                    });
                    row.Add(new Stone
                    {
                        CanRoll = false,
                        X = g.Key,
                        Y = maxY + 1,
                    });
                    return row.Zip(row.Skip(1), (s1, s2) => (s1, s2)).ToList();
                })
                .OrderBy(x => x[0].s1.X)
                .ToList();

            var cols = fixedStones.GroupBy(s => s.Y)
                .Select(g =>
                {
                    var col = g.OrderBy(s => s.X).ToList();
                    col.Insert(0, new Stone
                    {
                        CanRoll = false,
                        Y = g.Key,
                        X = -1,
                    });
                    col.Add(new Stone
                    {
                        CanRoll = false,
                        Y = g.Key,
                        X = maxX + 1,
                    });
                    return col.Zip(col.Skip(1), (s1, s2) => (s1, s2)).ToList();
                })
                .OrderBy(x => x[0].s1.Y)
                .ToList();

            var rollingStones = inputs.Where(x => x.CanRoll).ToList();

            var ScoreFunc = (IEnumerable<Stone> s) => s.Select(s => (maxY + 1) - s.Y).Sum();
            var oldScore = 0;
            var score = 0;
            Dictionary<(int oldScore, int newScore), int> loops = [];

            var loopStart = 0;
            var loopLength = 0;

            var loopCount = 1_000_000_000;
            for (int i = 1; i <= loopCount; i++)
            {
                RollNorth(rollingStones, rows);
                RollWest(rollingStones, cols);
                RollSouth(rollingStones, rows);
                RollEast(rollingStones, cols);

                oldScore = score;
                score = ScoreFunc(rollingStones);
                
                if (loops.TryGetValue((oldScore, score), out loopStart))
                {
                    loopLength = i - loopStart;
                    Console.WriteLine($"Loop found at i: {i} - start: {loopStart} length: {loopLength}");
                    break;
                }
                loops.Add((oldScore, score), i);
            }

            var remaining = (loopCount - loopStart) % loopLength;
            return loops.First(x => x.Value == loopStart + remaining).Key.newScore;
        }

        void RollNorth(List<Stone> stones, List<List<(Stone s1, Stone s2)>> rows)
        {
            foreach (var row in rows)
            {
                var rowStones = stones.Where(s => s.X == row[0].s1.X).ToList();
                foreach (var (s1, s2) in row)
                {
                    _ = rowStones.Where(s => s.Y > s1.Y && s.Y < s2.Y)
                        .OrderBy(s => s.Y)
                        .Select((s, i) => s.Y = s1.Y + i + 1)
                        .ToList();
                }
            }
        }

        void RollSouth(List<Stone> stones, List<List<(Stone s1, Stone s2)>> rows)
        {
            foreach (var row in rows)
            {
                var rowStones = stones.Where(s => s.X == row[0].s1.X).ToList();
                foreach (var (s1, s2) in row)
                {
                    _ = rowStones.Where(s => s.Y > s1.Y && s.Y < s2.Y)
                        .OrderByDescending(s => s.Y)
                        .Select((s, i) => s.Y = s2.Y - (i + 1))
                        .ToList();
                }
            }
        }

        void RollWest(List<Stone> stones, List<List<(Stone s1, Stone s2)>> cols)
        {
            foreach (var col in cols)
            {
                var colStones = stones.Where(s => s.Y == col[0].s1.Y).ToList();
                foreach (var (s1, s2) in col)
                {
                    _ = colStones.Where(s => s.X > s1.X && s.X < s2.X)
                        .OrderBy(s => s.X)
                        .Select((s, i) => s.X = s1.X + (i + 1))
                        .ToList();
                }
            }
        }

        void RollEast(List<Stone> stones, List<List<(Stone s1, Stone s2)>> cols)
        {
            foreach (var col in cols)
            {
                var colStones = stones.Where(s => s.Y == col[0].s1.Y).ToList();
                foreach (var (s1, s2) in col)
                {
                    _ = colStones.Where(s => s.X > s1.X && s.X < s2.X)
                        .OrderByDescending(s => s.X)
                        .Select((s, i) => s.X = s2.X - (i + 1))
                        .ToList();
                }
            }
        }

        public List<Stone> SetupInputs(string[] inputs)
        {
            return inputs.SelectMany((l, y) => l.Select((c, x) =>
            {
                return c switch
                {
                    '#' => new Stone
                    {
                        CanRoll = false,
                        X = x,
                        Y = y,
                    },
                    'O' => new Stone
                    {
                        CanRoll = true,
                        X = x,
                        Y = y,
                    },
                    _ => null,
                };
            })
            .Where(x => x != null)
            .Cast<Stone>()
            .ToList())
            .ToList();
        }
    }

    public record Stone
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool CanRoll { get; init; }
    }

    class LLNode
    {
        public int data;
        public LLNode next;

        public LLNode(int data)
        {
            this.data = data;
            next = null;
        }
    };

    class Linkedlist
    {
        public Dictionary<int, LLNode> Dict { get; set; } = [];
        int? start = null;
        public LLNode Head { get; set; } = null!;

        // insert new value at the start
        public void Insert(int value)
        {
            start ??= value;
            LLNode newNode = Dict.TryGetValue(value, out var node) ? node : new LLNode(value);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                newNode.next = Head;
                Head = newNode;
            }
            Dict[value] = newNode;
        }

        public LLNode? Start => Dict.TryGetValue(start.GetValueOrDefault(), out var node) ? node : null;

        // detect if there is a loop
        // in the linked list
        public int? DetectLoop()
        {
            LLNode slowPointer = Head, fastPointer = Head;

            while (slowPointer != null && fastPointer != null &&
                   fastPointer.next != null)
            {
                slowPointer = slowPointer.next;
                fastPointer = fastPointer.next.next;
                if (slowPointer == fastPointer)
                {
                    return slowPointer.data;
                }
            }

            return null;
        }
    }
}
