using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day4 : IDay<(List<int> first, List<int> second)>
    {
        public long A(List<(List<int> first, List<int> second)> inputs)
        {
            return inputs.Count(x =>
            {
                return x.first.All(i => x.second.Contains(i))
                    || x.second.All(i => x.first.Contains(i));
            });
        }

        public long B(List<(List<int> first, List<int> second)> inputs)
        {
            return inputs.Count(x => x.first.Intersect(x.second).Any());
        }

        public List<(List<int> first, List<int> second)> SetupInputs(string[] inputs)
        {
            List<(List<int> first, List<int> second)> list = new();
            foreach (string input in inputs)
            {
                var parts = input.Split(',');
                var p1 = parts[0].Split('-').Select(int.Parse).ToArray();
                var p2 = parts[1].Split('-').Select(int.Parse).ToArray();

                List<int> templist = new();
                for (int i = p1[0]; i <= p1[1]; i++)
                {
                    templist.Add(i);
                }
                List<int> templist2 = new();
                for (int i = p2[0]; i <= p2[1]; i++)
                {
                    templist2.Add(i);
                }

                list.Add((templist, templist2));
            }
            return list;
        }
    }
}
