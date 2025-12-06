using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day5 : IDay<(List<(long start, long end)> ranges, List<long> ids)>
    {
        public long A(List<(List<(long start, long end)> ranges, List<long> ids)> inputs)
        {
            var (ranges, ids) = inputs.SingleOrDefault();

            var fresh = 0;
            foreach (var id in ids)
            {
                if (ranges.Any(r => id >= r.start && id <= r.end))
                {
                    fresh++;
                }
            }

            return fresh;
        }

        public long B(List<(List<(long start, long end)> ranges, List<long> ids)> inputs)
        {
            var (ranges, _) = inputs.SingleOrDefault();

            List<long> nums = new List<long>(ranges.Count * 2);
            foreach (var range in ranges)
            {
                nums.Add(range.start);
                nums.Add(range.end + 1);
            }

            nums.Sort();
            var validIds = 0L;
            for (int i = 0; i < nums.Count - 1; i++)
            {
                var start = nums[i];
                var end = nums[i + 1];
                if (ranges.Any(x => start >= x.start && start <= x.end))
                {
                    validIds += end - start;
                }
            }

            return validIds;
        }

        public List<(List<(long start, long end)> ranges, List<long> ids)> SetupInputs(string[] inputs)
        {
            List<(long, long)> ranges = [];
            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                var s = input.Split('-').Select(long.Parse).ToList();
                ranges.Add((s[0], s[1]));
            }

            List<long> ids = inputs
                .SkipUntil(string.IsNullOrWhiteSpace)
                .Skip(1)
                .Select(long.Parse)
                .ToList();

            return [(ranges, ids)];
        }
    }
}
