using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day11 : IDay<Galaxy>
    {
        public long A(List<Galaxy> inputs)
        {
            return ExpandA(inputs).Pairs()
                .Select(p => Math.Abs(p.Item1.Col - p.Item2.Col) + Math.Abs(p.Item1.Row - p.Item2.Row))
                .Sum();
        }

        public List<Galaxy> ExpandA(List<Galaxy> galaxies)
        {
            var maxRow = galaxies.Max(x => x.Row);
            var maxCol = galaxies.Max(x => x.Col);

            var emptyRows = Enumerable.Range(0, (int)maxRow)
                .Where(r => galaxies.Any(g => g.Row == r) is false)
                .ToList();
            var emptyCols = Enumerable.Range(0, (int)maxCol)
                .Where(c => galaxies.Any(g => g.Col == c) is false)
                .ToList();

            for (int r = 0; r < emptyRows.Count; r++)
            {
                var row = emptyRows[r] + r;
                _ = galaxies.Where(g => g.Row > row)
                    .Select(g => g.Row++)
                    .ToList();
            }

            for (int c = 0; c < emptyCols.Count; c++)
            {
                var col = emptyCols[c] + c;
                _ = galaxies.Where(g => g.Col > col)
                    .Select(g => g.Col++)
                    .ToList();
            }

            return galaxies;
        }

        public List<Galaxy> ExpandB(List<Galaxy> galaxies, int expansion = 1_000_000)
        {
            var maxRow = galaxies.Max(x => x.Row);
            var maxCol = galaxies.Max(x => x.Col);

            var emptyRows = Enumerable.Range(0, (int)maxRow)
                .Where(r => galaxies.Any(g => g.Row == r) is false)
                .ToList();
            var emptyCols = Enumerable.Range(0, (int)maxCol)
                .Where(c => galaxies.Any(g => g.Col == c) is false)
                .ToList();

            expansion--;

            for (int r = 0; r < emptyRows.Count; r++)
            {
                var row = emptyRows[r] + (r * expansion);
                _ = galaxies.Where(g => g.Row > row)
                    .Select(g => g.Row += expansion)
                    .ToList();
            }

            for (int c = 0; c < emptyCols.Count; c++)
            {
                var col = emptyCols[c] + (c * expansion);
                _ = galaxies.Where(g => g.Col > col)
                    .Select(g => g.Col += expansion)
                    .ToList();
            }

            return galaxies;
        }

        public long B(List<Galaxy> inputs)
        {
            return ExpandB(inputs).Pairs()
                .Select(p => Math.Abs(p.Item1.Col - p.Item2.Col) + Math.Abs(p.Item1.Row - p.Item2.Row))
                .Sum();
        }

        public List<Galaxy> SetupInputs(string[] inputs)
        {
            var galaxies = new List<Galaxy>();
            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    if (inputs[y][x] == '#')
                    {
                        galaxies.Add(new Galaxy(x, y));
                    }
                }
            }

            return galaxies;
        }
    }

    public class Galaxy(int x, int y)
    {
        public long Col { get; set; } = x;
        public long Row { get; set; } = y;

        public override string ToString()
        {
            return $"({Col}, {Row})";
        }
    }
}
