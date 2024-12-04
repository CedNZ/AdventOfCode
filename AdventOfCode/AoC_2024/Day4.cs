using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day4 : IDay<List<char>>
    {
        public long A(List<List<char>> inputs)
        {
            List<(int x, int y)> _xs = [];
            List<(int x, int y)> _ms = [];
            List<(int x, int y)> _as = [];
            List<(int x, int y)> _ss = [];
            int minX = 0;
            int minY = 0;
            int maxY = inputs.Count;
            int maxX = inputs[0].Count;

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    if (inputs[y][x] == 'X')
                    {
                        _xs.Add((x, y));
                    }
                    else if (inputs[y][x] == 'M')
                    {
                        _ms.Add((x, y));
                    }
                    else if (inputs[y][x] == 'A')
                    {
                        _as.Add((x, y));
                    }

                    else if (inputs[y][x] == 'S')
                    {
                        _ss.Add((x, y));
                    }
                }
            }

            return _xs
                .Select(x =>
                (
                    x.x,
                    x.y,
                    Direction.Any
                ))
                .SelectMany(Neighbours)
                .Distinct()
                .Join(_ms,
                    x => new { x.x, x.y },
                    m => new { m.x, m.y },
                    (x, m) =>
                    (
                        m.x,
                        m.y,
                        x.d
                    ))
                .SelectMany(Neighbours)
                .Distinct()
                .Join(_as,
                    m => new { m.x, m.y },
                    a => new { a.x, a.y },
                    (m, a) =>
                    (
                        a.x,
                        a.y,
                        m.d
                    ))
                .SelectMany(Neighbours)
                .Distinct()
                .Join(_ss,
                    a => new { a.x, a.y },
                    s => new { s.x, s.y },
                    (a, s) =>
                    (
                        s.x,
                        s.y,
                        a.d
                    ))
                .Distinct()
                .ToList()
                .Count;


            IEnumerable<(int x, int y, Direction d)> Neighbours((int x, int y, Direction d) pos)
            {
                List<(int x, int y, Direction d)> nums =
                    [
                        (pos.x - 1, pos.y - 1, Direction.NW),
                        (pos.x - 1, pos.y, Direction.W),
                        (pos.x - 1, pos.y + 1, Direction.SW),
                        (pos.x, pos.y - 1, Direction.N),
                        (pos.x, pos.y + 1, Direction.S),
                        (pos.x + 1, pos.y - 1, Direction.NE),
                        (pos.x + 1, pos.y, Direction.E),
                        (pos.x + 1, pos.y + 1, Direction.SE),
                    ];

                foreach (var n in nums.Where(n => n.x >= minX && n.x < maxX && n.y >= minY && n.y < maxY))
                {
                    if (pos.d is Direction.Any || n.d == pos.d)
                    {
                        yield return n;
                    }
                }
            };
        }

        public long B(List<List<char>> inputs)
        {
            List<(int x, int y)> _xs = [];
            List<(int x, int y)> _ms = [];
            List<(int x, int y)> _as = [];
            List<(int x, int y)> _ss = [];
            int minX = 0;
            int minY = 0;
            int maxY = inputs.Count;
            int maxX = inputs[0].Count;

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[y].Count; x++)
                {
                    if (inputs[y][x] == 'X')
                    {
                        _xs.Add((x, y));
                    }
                    else if (inputs[y][x] == 'M')
                    {
                        _ms.Add((x, y));
                    }
                    else if (inputs[y][x] == 'A')
                    {
                        _as.Add((x, y));
                    }

                    else if (inputs[y][x] == 'S')
                    {
                        _ss.Add((x, y));
                    }
                }
            }

            return _as
                .Where(a =>
                    ((_ms.Contains((a.x - 1, a.y - 1)) && _ss.Contains((a.x + 1, a.y + 1)))
                        || (_ms.Contains((a.x + 1, a.y + 1)) && _ss.Contains((a.x - 1, a.y - 1))))
                    && ((_ms.Contains((a.x + 1, a.y - 1)) && _ss.Contains((a.x - 1, a.y + 1)))
                        || (_ms.Contains((a.x - 1, a.y + 1)) && _ss.Contains((a.x + 1, a.y - 1))))
                    ).ToList()
                .Count;
        }

        public List<List<char>> SetupInputs(string[] inputs)
        {
            return inputs.Select(input => input.ToList()).ToList();
        }

        enum Direction
        {
            Any,
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW,
        }
    }
}
