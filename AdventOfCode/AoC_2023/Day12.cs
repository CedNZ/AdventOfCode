using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day12 : IDay<(List<Spring> springs, List<int> brokenGroups)>
    {
        public long A(List<(List<Spring> springs, List<int> brokenGroups)> inputs)
        {
            foreach (var input in inputs)
            {
                var (springs, brokenGroups) = input;
                var springClusters = springs.ClusterWhile(s => s.Broken,
                    (s1, s2) => s1 is null or true && s2 is null or true);

                while (springClusters[0].Count == brokenGroups[0]
                    || springClusters[^1].Count == brokenGroups[^1])
                {
                    if (springClusters[0].Count == brokenGroups[0])
                    {
                        springClusters.RemoveAt(0);
                        brokenGroups.RemoveAt(0);
                    }
                    if (springClusters[^1].Count == brokenGroups[^1])
                    {
                        springClusters.RemoveAt(springClusters.Count - 1);
                        brokenGroups.RemoveAt(brokenGroups.Count - 1);
                    }
                }


            }

            return default;
        }

        //public int Arrangements(List<List<Spring>> springs, List<int> brokenGroups)
        //{

        //}

        public long B(List<(List<Spring> springs, List<int> brokenGroups)> inputs)
        {
            throw new NotImplementedException();
        }

        public List<(List<Spring> springs, List<int> brokenGroups)> SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var p = l.Split(' ');
                var springs = p[0].Select((x, i) =>
                {
                    bool? broken = x switch
                    {
                        '#' => true,
                        '.' => false,
                        _ => null,
                    };
                    return new Spring(i, broken);
                }).ToList();
                var groups = p[1].Split(',').Select(int.Parse).ToList();
                return (springs, groups);
            }).ToList();
        }
    }

    public class Spring
    {
        public bool? Broken { get; set; } = null!;
        public int Index { get; set; }

        public Spring(int index, bool? broken = null)
        {
            Index = index;
            Broken = broken;
        }
    }
}
