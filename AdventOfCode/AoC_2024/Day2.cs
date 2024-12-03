using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day2 : IDay<List<int>>
    {
        public long A(List<List<int>> inputs)
        {
            var count = 0;
            foreach (var list in inputs)
            {
                if (list.Select((x, i) => i == 0 || (x > list[i - 1] && (x - list[i - 1] <= 3))).All(x => x))
                {
                    count++;
                }
                else if (list.Select((x, i) => i == 0 || (x < list[i - 1] && (list[i - 1] - x <= 3))).All(x => x))
                {
                    count++;
                }
            }
            return count;
        }

        public long B(List<List<int>> inputs)
        {
            var count = 0;
            foreach (var list in inputs)
            {
                var delta = list.Select((x, i) => i == 0 ? 0 : x - list[i - 1]).Skip(1).ToList();

                if (delta.All(x => Math.Abs(x) <= 3) && (delta.All(x => x > 0) || delta.All(x => x < 0)))
                {
                    count++;
                    continue;
                }

                var pos = delta.Count(x => x > 0);
                var neg = delta.Count(x => x < 0);
                var zero = delta.Count(x => x == 0);
                var greaterThenThree = delta.Count(x => Math.Abs(x) > 3);

                int? index = null;

                if (zero == 1)
                {
                    index = delta.FindIndex(x => x == 0);
                }
                else if (greaterThenThree == 1)
                {
                    index = delta.FindIndex(x => Math.Abs(x) > 3);
                }
                else if (pos == 1)
                {
                    index = delta.FindIndex(x => x > 0);
                }
                else if (neg == 1)
                {
                    index = delta.FindIndex(x => x < 0);
                }


                if (index is { } i)
                {

                    var trimmed = list.Take(i).Concat(list.Skip(i + 1)).ToList();
                    var deltas = trimmed.Select((x, i) => i == 0 ? 0 : x - trimmed[i - 1]).Skip(1).ToList();

                    pos = deltas.Count(x => x > 0);
                    neg = deltas.Count(x => x < 0);
                    zero = deltas.Count(x => x == 0);
                    greaterThenThree = deltas.Count(x => Math.Abs(x) > 3);

                    if (zero == 0 && greaterThenThree == 0 && ((pos > 0 && neg == 0) || (neg > 0 && pos == 0)))
                    {
                        count++;

                        continue;
                    }
                    else
                    {
                        i++;

                        trimmed = list.Take(i).Concat(list.Skip(i + 1)).ToList();
                        deltas = trimmed.Select((x, i) => i == 0 ? 0 : x - trimmed[i - 1]).Skip(1).ToList(); 

                        if (deltas.All(x => x != 0 && Math.Abs(x) <= 3))
                        {
                            if (deltas.All(x => x > 0) || deltas.All(x => x < 0))
                            {
                                count++;
                                continue;
                            }
                        }
                    }
                }
            }
            return count;
        }

        public List<List<int>> SetupInputs(string[] inputs)
        {
            return inputs.Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        }
    }
}
