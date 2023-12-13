using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day13 : IDay<(List<int> rows, List<int> columns)>
    {
        public long A(List<(List<int> rows, List<int> columns)> inputs)
        {
            return inputs.Select((x, i) => new
            {
                i,
                RowIndex = FindMirrorIndex(x.rows),
                ColIndex = FindMirrorIndex(x.columns),
            })
            .Sum(x =>
            {
                if (x.RowIndex.HasValue)
                {
                    return x.RowIndex.Value * 100;
                }
                return x.ColIndex!.Value;
            });
        }

        Dictionary<int, (int? row, int? col)> cache = [];

        public int? FindMirrorIndex(List<int> nums)
        {
            for (int i = 1; i < nums.Count; i++)
            {
                var l = nums.Take(i).Reverse().ToList();
                var r = nums.Skip(i).ToList();
                var minIndex = Math.Min(l.Count, r.Count);
                if (l.Take(minIndex).SequenceEqual(r.Take(minIndex)))
                {
                    return i;
                }
            }
            return null;
        }

        public long B(List<(List<int> rows, List<int> columns)> inputs)
        {
            return inputs.Select((x, i) => new
            {
                i,
                RowIndex = FindMirrorIndexB(x.rows),
                ColIndex = FindMirrorIndexB(x.columns),
            })
            .Sum(x =>
            {
                if (x.RowIndex.HasValue)
                {
                    return x.RowIndex.Value * 100;
                }
                return x.ColIndex!.Value;
            });
        }

        public int? FindMirrorIndexB(List<int> nums)
        {
            for (int i = 1; i < nums.Count; i++)
            {
                var l = nums.Take(i).Reverse().ToList();
                var r = nums.Skip(i).ToList();
                var minIndex = Math.Min(l.Count, r.Count);
                var corrected = 0;
                for (int j = 0; j < minIndex; j++)
                {
                    var (n1, n2) = (l[j], r[j]);
                    if (n1 == n2)
                    {
                        continue;
                    }
                    else if (LinqExtensions.HasOnlyOneBitSet(n1 ^ n2))
                    {
                        corrected++;
                        if (corrected >= 2)
                        {
                            break;
                        }
                    }
                    else
                    {
                        corrected = -1;
                        break;
                    }
                }
                if (corrected == 1)
                {
                    return i;
                }
            }
            return null;
        }

        public List<(List<int> rows, List<int> columns)> SetupInputs(string[] inputs)
        {
            return inputs.ClusterWhile(l => string.IsNullOrEmpty(l) is false)
                .Select(x =>
                {
                    var lines = x.SkipWhile(l => string.IsNullOrWhiteSpace(l)).ToList();

                    var rows = lines.Select(l => l.Select((c, i) => (c == '#' ? 1 : 0) << i).Sum()).ToList();
                    List<int> cols = [];
                    for (int c = 0; c < lines[0].Length; c++)
                    {
                        cols.Add(lines.Select(l => l[c]).Select((c, i) => (c == '#' ? 1 : 0) << i).Sum());
                    }

                    return (rows, cols);
                }).ToList();
        }
    }
}
