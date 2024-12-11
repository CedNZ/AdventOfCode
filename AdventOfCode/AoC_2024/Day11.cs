using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day11 : IDay<long>
    {
        public long A(List<long> inputs)
        {
            Dictionary<long, long[]> map = [];
            Dictionary<(long, int), long> score = [];

            return inputs.Sum(n => Recurse(0, n));

            long Recurse(int depth, long num)
            {
                long[] next;
                if (depth == 25)
                {
                    return 1;
                }
                if (map.TryGetValue(num, out next) is false)
                {
                    if (num == 0)
                    {
                        next = [1];
                        return Recurse(depth + 1, 1);
                    }
                    else if (num.ToString().Length % 2 == 0)
                    {
                        var nums = num.ToString();
                        var length = nums.Length / 2;
                        var n1 = int.Parse(nums[..length]);
                        var n2 = int.Parse(nums[length..]);
                        next = [n1, n2];
                    }
                    else
                    {
                        next = [num * 2024];
                    }
                    map[num] = next;
                }
                return next.Sum(n =>
                {
                    if (score.TryGetValue((n, depth), out var s))
                    {
                        return s;
                    }
                    s = Recurse(depth + 1, n);
                    score[(n, depth)] = s;
                    return s;
                });
            }
        }

        public long B(List<long> inputs)
        {
            Dictionary<long, long[]> map = [];
            Dictionary<(long, int), long> score = [];

            return inputs.Sum(n => Recurse(0, n));

            long Recurse(int depth, long num)
            {
                long[] next;
                if (depth == 75)
                {
                    return 1;
                }
                if (map.TryGetValue(num, out next) is false)
                {
                    if (num == 0)
                    {
                        next = [1];
                        return Recurse(depth + 1, 1);
                    }
                    else if (num.ToString().Length % 2 == 0)
                    {
                        var nums = num.ToString();
                        var length = nums.Length / 2;
                        var n1 = int.Parse(nums[..length]);
                        var n2 = int.Parse(nums[length..]);
                        next = [n1, n2];
                    }
                    else
                    {
                        next = [num * 2024];
                    }
                    map[num] = next;
                }
                return next.Sum(n =>
                {
                    if (score.TryGetValue((n, depth), out var s))
                    {
                        return s;
                    }
                    s = Recurse(depth + 1, n);
                    score[(n, depth)] = s;
                    return s;
                });
            }
        }

        public List<long> SetupInputs(string[] inputs)
        {
            return inputs.Single().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        }
    }
}
