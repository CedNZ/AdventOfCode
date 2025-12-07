using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day7 : IDay<(int x, int y)>
    {
        public long A(List<(int x, int y)> inputs)
        {
            var source = inputs[0];
            HashSet<int> beamXIndexes = [source.x];
            var splitterYLevels = inputs.Skip(1)
                .GroupBy(x => x.y, x => x.x)
                .OrderBy(g => g.Key)
                .ToList();
            HashSet<(int x, int y)> splittersHit = [];

            foreach (var splitterLevel in splitterYLevels)
            {
                var splitters = splitterLevel.Intersect(beamXIndexes).ToList();
                beamXIndexes = beamXIndexes.Except(splitters).ToHashSet();
                foreach (var splitter in splitters)
                {
                    splittersHit.Add((splitter, splitterLevel.Key));
                    beamXIndexes.Add(splitter - 1);
                    beamXIndexes.Add(splitter + 1);
                }
            }

            return splittersHit.Count;
        }

        public long B(List<(int x, int y)> inputs)
        {
            var source = inputs[0];
            var maxX = inputs.Max(x => x.x);
            long[] beamXIndexer = new long[maxX + 2];

            beamXIndexer[source.x] = 1;

            var splitterYLevels = inputs.Skip(1)
                .GroupBy(x => x.y, x => x.x)
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var splitterLevel in splitterYLevels)
            {
                var beamXIndexes = beamXIndexer
                    .Select((x, i) => x > 0 ? i : -1)
                    .Where(x => x > 0)
                    .ToHashSet();
                var splitters = splitterLevel.Intersect(beamXIndexes).ToList();

                foreach (var splitter in splitters)
                {
                    var beams = beamXIndexer[splitter];
                    beamXIndexer[splitter] = 0;
                    beamXIndexer[splitter - 1] += beams;
                    beamXIndexer[splitter + 1] += beams;
                }
            }

            return beamXIndexer.Sum();
        }

        public List<(int x, int y)> SetupInputs(string[] inputs)
        {
            List<(int x, int y)> splitters = [];
            for (int y = 0; y < inputs.Length; y++)
            {
                splitters.AddRange(
                    inputs[y].Select((c, i) => new {c, i})
                        .Where(x => x.c is 'S' or '^')
                        .Select(x => (x.i, y))
                    );
            }
            return splitters;
        }
    }
}
