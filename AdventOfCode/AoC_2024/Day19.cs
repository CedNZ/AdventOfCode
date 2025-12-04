using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day19 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var stripes = inputs[0].Split(", ").ToList();
            var towels = inputs.Skip(2).ToList();

            Dictionary<int, HashSet<int>> stripeDict = [];
            var possibleCount = 0;
            foreach (var towel in towels)
            {
                stripeDict = [];
                for (int i = 0; i < towel.Length; i++)
                {
                    var l = towel[i];
                    HashSet<int> sDict = [];
                    var possibleStripes = stripes.Where(s =>
                            s[0] == l
                            && (s.Length + i) <= towel.Length
                            && s == towel.Substring(i, s.Length))
                        .ToList();
                    foreach (var stripe in possibleStripes)
                    {
                        sDict.Add(stripe.Length + i);
                    }
                    stripeDict[i] = sDict;
                }

                var overlap = ContiguousOverlap(stripeDict, towel.Length, towel);

                if (overlap)
                {
                    possibleCount++;
                }
            }
            return possibleCount;
        }

        bool ContiguousOverlap(Dictionary<int, HashSet<int>> stripeDict, int totalLength, string towel)
        {
            if (stripeDict[0].Count == 0 || stripeDict.Any(h => h.Value.Contains(totalLength)) is false)
            {
                return false;
            }
            var potentialStart = stripeDict[0];
            List<int> indexes = [];
            foreach (var start in potentialStart)
            {
                indexes.Add(start);
            }
            HashSet<int> visited = [];
            indexes = indexes.OrderDescending().ToList();
            while (indexes.Count > 0)
            {
                var index = indexes[0];
                indexes.RemoveAt(0);
                visited.Add(index);
                var nextIndexes = stripeDict[index];
                foreach (var nextIndex in nextIndexes)
                {
                    if (nextIndex == totalLength)
                    {
                        return true;
                    }
                    if (nextIndex <= totalLength)
                    {
                        if (visited.Contains(nextIndex) is false)
                        {
                            indexes.Add(nextIndex);
                        }
                    }
                }
                indexes = indexes.Distinct().OrderDescending().ToList();
            }
            return false;
        }

        public long B(List<string> inputs)
        {
            var stripes = inputs[0].Split(", ").ToList();
            var towels = inputs.Skip(2).ToList();

            var possibleCount = 0;
            var sum = 0L;
            Dictionary<int, HashSet<int>> stripeDict = [];
            foreach (var towel in towels)
            {
                stripeDict = [];
                for (int i = 0; i < towel.Length; i++)
                {
                    var l = towel[i];
                    HashSet<int> sDict = [];
                    var possibleStripes = stripes.Where(s =>
                            s[0] == l
                            && (s.Length + i) <= towel.Length
                            && s == towel.Substring(i, s.Length))
                        .ToList();
                    foreach (var stripe in possibleStripes)
                    {
                        sDict.Add(stripe.Length + i);
                    }
                    stripeDict[i] = sDict;
                }

                sum += ContiguousOverlapB(stripeDict, towel.Length, towel);
            }

            return sum;
        }

        long ContiguousOverlapB(Dictionary<int, HashSet<int>> stripeDict, int totalLength, string towel)
        {
            if (stripeDict[0].Count == 0 || stripeDict.Any(h => h.Value.Contains(totalLength)) is false)
            {
                return 0;
            }
            var potentialStart = stripeDict[0];
            List<int> indexes = [];
            foreach (var start in potentialStart)
            {
                indexes.Add(start);
            }
            HashSet<int> reachedEnd = [];
            Dictionary<int, long> pathDict = [];
            indexes = indexes.OrderDescending().ToList();

            var pathCount = DFS(0);

            long DFS(int index)
            {
                if (index == totalLength)
                {
                    return 1L;
                }
                if (pathDict.TryGetValue(index, out var p))
                {
                    return p;
                }

                var nextIndexes = stripeDict[index].OrderDescending();
                var pathCount = 0L;
                foreach (var nextIndex in nextIndexes)
                {
                    pathCount += DFS(nextIndex);
                }
                pathDict[index] = pathCount;
                return pathCount;
            }

            return pathCount;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
