using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day12 : IDay<(List<Present> presents, List<Region> region)>
    {
        public long A(List<(List<Present> presents, List<Region> region)> inputs)
        {
            var (presents, regions) = inputs[0];

            var canFit = 0;

            foreach (var region in regions)
            {
                var presentArea = region.Presents.Select((x, i) => presents.FirstOrDefault(p => p.Index == i)?.Area * x).Sum();
                if (presentArea <= region.Area)
                {
                    canFit++;
                }
            }

            return canFit;
        }

        public long B(List<(List<Present> presents, List<Region> region)> inputs)
        {
            return 0;
        }

        public List<(List<Present> presents, List<Region> region)> SetupInputs(string[] inputs)
        {
            var input = inputs.ClusterWhile(x => !string.IsNullOrWhiteSpace(x));
            var regionInput = input[^1];
            var presentInput = input[0..^1];

            var presents = presentInput.ConvertAll(x => new Present([.. x.SkipWhile(string.IsNullOrEmpty)]));
            var regions = regionInput.SkipWhile(string.IsNullOrEmpty).Select(x => new Region(x)).ToList();

            return [(presents, regions)];
        }
    }

    public record Present
    {
        public int Index { get; set; }
        public List<(int x, int y)> Shape { get; set; } = [];
        public int Area => Shape.Count;

        public Present(List<string> lines)
        {
            Index = int.Parse(lines[0].Trim(':'));
            lines.RemoveAt(0);
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        Shape.Add((x, y));
                    }
                }
            }
        }
    }

    public record Region
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Area => X * Y;

        public List<int> Presents { get; set; } = [];

        public Region(string x)
        {
            var parts = x.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var size = parts[0].Split('x');

            X = int.Parse(size[1]);
            Y = int.Parse(size[0]);
            Presents = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }
    }
}
