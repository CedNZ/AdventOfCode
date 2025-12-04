using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day2 : IDay<(long start, long end)>
    {
        public long A(List<(long start, long end)> inputs)
        {
            var valid = 0;
            foreach (var (start, end) in inputs)
            {

                var current = start;
                while (current <= end)
                {
                    var digits = DigitCount(current);
                    var length = digits / 2;
                    var num = current.ToString()[0..length];
                    var test = int.Parse(num + num);

                    if (test >= start && test <= end)
                    {
                        valid++;
                    }

                    var nextNum = (int.Parse(num) + 1).ToString();
                    current = int.Parse(nextNum + nextNum);
                }
            }

            return valid;
        }

        public long B(List<(long start, long end)> inputs)
        {
            throw new NotImplementedException();
        }

        int DigitCount(long x)
        {
            if (x < 10) return 1;
            if (x < 100) return 2;
            if (x < 1000) return 3;
            if (x < 10000) return 4;
            if (x < 100000) return 5;
            if (x < 1000000) return 6;
            if (x < 10000000) return 7;
            if (x < 100000000) return 8;
            if (x < 1000000000) return 9;
            return 10;
        }

        public List<(long start, long end)> SetupInputs(string[] inputs)
        {
            return inputs.Single().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s =>
            {
                var p = s.Split('-', StringSplitOptions.RemoveEmptyEntries);
                return (long.Parse(p[0]), long.Parse(p[1]));
            }).ToList();
        }
    }
}
