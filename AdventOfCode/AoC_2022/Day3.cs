using AdventOfCodeCore;

namespace AoC_2022
{
    internal class Day3 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var score = 0;
            foreach (var item in inputs)
            {
                var firstHalf = item.Take(item.Count() / 2).ToList();
                var secondH = item.TakeLast(item.Count() / 2).ToList();
                var c = firstHalf.Join(secondH,
                                x => x,
                                x => x,
                                (x, y) => x)
                    .Distinct()
                    .Single();

                //var c = match.FirstOrDefault();

                if (char.IsLower(c))
                {
                    score += (int)c - 96;
                }
                else
                {
                    score += (int)c - 38;
                }
            }
            return score;
        }

        public long B(List<string> inputs)
        {
            var score = 0;
            foreach (var chunk in inputs.Chunk(3))
            {
                var match = chunk[0].Join(
                        chunk[1],
                        x => x,
                        x => x,
                        (x, y) => x
                    ).Join(
                        chunk[2],
                        x => x,
                        x => x,
                        (x, y) => x
                    ).Distinct().Single();

                if (char.IsLower(match))
                {
                    score += (int)match - 96;
                }
                else
                {
                    score += (int)match - 38;
                }
            }
            return score;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
