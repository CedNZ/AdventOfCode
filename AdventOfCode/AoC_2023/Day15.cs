using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day15 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            return inputs.Sum(x => x.Aggregate(0, Hash));
        }

        public static int Hash(int hash, char c)
        {
            hash += c;
            hash *= 17;
            hash %= 256;
            return hash;
        }

        public long B(List<string> inputs)
        {
            List<List<(string label, long focal)>> boxes = new(256);
            for (int i = 0; i < 256; i++)
            {
                boxes.Add([]);
            }
            foreach (var input in inputs)
            {
                var label = new string([.. input.TakeWhile(c => c is not '=' and not '-')]);
                var box = label.Aggregate(0, Hash);
                var last = input[^1];
                if (last == '-')
                {
                    var lens = boxes[box].Find(x => x.label == label);
                    if (lens.label is not null)
                    {
                        boxes[box].Remove(lens);
                    }
                }
                else
                {
                    var focal = int.Parse(last.ToString());
                    var lens = boxes[box].FirstOrDefault(x => x.label == label);
                    if (string.IsNullOrEmpty(lens.label))
                    {
                        boxes[box].Add((label, focal));
                    }
                    else
                    {
                        var index = boxes[box].IndexOf(lens);
                        lens.focal = focal;
                        boxes[box][index] = lens;
                    }
                }
            }
            return boxes.Select((b, i) => b.Select((l, j) => (1 + i) * (j + 1) * l.focal).Sum())
                .Sum();
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return [.. inputs[0].Split(',', StringSplitOptions.RemoveEmptyEntries)];
        }
    }
}
