using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day3 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var sum = 0;

            foreach (var input in inputs)
            {
                var nums = input.Select(c => c - '0').ToList();
                var n1 = nums[0];
                var n2 = nums[1];
                for (var i = 2; i < nums.Count; i++)
                {
                    var n = nums[i];
                    if (n2 < n)
                    {
                        if (n1 < n2)
                        {
                            n1 = n2;
                        }
                        n2 = n;
                    }
                    else if (n1 < n2)
                    {
                        n1 = n2;
                        n2 = n;
                    }
                }
                var max = n1 * 10 + n2;
                sum += max;
            }

            return sum;
        }

        public long B(List<string> inputs)
        {
            long sum = 0L;

            foreach (var input in inputs)
            {
                var nums = input.Select(c => c - '0').ToList();

                var largest = GetLargest(nums, 12);
                sum += largest;
            }

            return sum;
        }

        long GetLargest(List<int> input, int length)
        {
            var ns = input[0..length];

            for (int i = length; i < input.Count; i++)
            {
                var n = input[i];

                var idx = FindIndexOf(ns, (x, y) => x < y);
                if (idx >= 0)
                {
                    ns.RemoveAt(idx);
                    ns.Add(n);
                }
                else
                {
                    var min = ns.Min();
                    if (n > min)
                    {
                        ns.Remove(min);
                        ns.Add(n);
                    }
                }
            }

            var numString = string.Join("", ns);

            return long.Parse(numString);
        }

        int FindIndexOf(List<int> nums, Func<int, int, bool> predicate)
        {
            for (int i = 0; i < nums.Count - 1; i++)
            {
                if (predicate(nums[i], nums[i + 1]))
                {
                    return i;
                }
            }
            return -1;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
