using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day9 : IDay<long>
    {
        private int preambleLength = 25;

        public List<long> SetupInputs(string[] inputs)
        {
            var returnList = inputs.Select(long.Parse).ToList();

            if (inputs.Length < 50)
            {
                preambleLength = 5;
            }

            return returnList;
        }

        public long A(List<long> inputs)
        {
            return FindWeakness(inputs);
        }

        private long FindWeakness(List<long> inputs)
        {
            Queue<long> preamble = new Queue<long>(preambleLength);
            for(int i = 0; i < inputs.Count(); i++)
            {
                var number = inputs[i];
                if(i <= preambleLength)
                {
                    preamble.Enqueue(number);
                    continue;
                }

                var combos = Combinations(preamble);

                if(combos.Select(x => x.Item1 + x.Item2).Any(x => x == number))
                {
                    preamble.Enqueue(number);
                    _ = preamble.Dequeue();
                }
                else
                {
                    return number;
                }
            }
            return -1;
        }

        public long B(List<long> inputs)
        {
            var weakness = FindWeakness(inputs);

            var windowStart = 0;
            var windowFinish = 1;

            while(inputs.Take(windowFinish).Sum() < weakness)
            {
                windowFinish++;
            }

            var windowSum = inputs.Skip(windowStart).Take(windowFinish).Sum();

            while(windowSum != weakness && windowFinish < inputs.Count())
            {
                if (windowSum > weakness)
                {
                    windowStart++;
                    windowFinish--;
                }
                else
                {
                    windowFinish++;
                }
                windowSum = inputs.Skip(windowStart).Take(windowFinish).Sum();
            }


            var window = inputs.Skip(windowStart).Take(windowFinish).OrderBy(x => x).ToList();

            return window.First() + window.Last();
        }

        public IEnumerable<(long, long)> Combinations(IEnumerable<long> all)
        {
            return from item1 in all
                   from item2 in all
                   where item1 < item2
                   select (item1, item2);
        }
    }
}
