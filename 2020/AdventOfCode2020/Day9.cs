using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day9 : IDay<long>
    {
        public List<long> SetupInputs(string[] inputs)
        {
            var returnList = inputs.Select(long.Parse).ToList();

            var preambleLength = 25;

            if (inputs.Length < 50)
            {
                preambleLength = 5;
            }

            returnList.Insert(0, preambleLength);

            return returnList;
        }

        public long A(List<long> inputs)
        {
            var preambleLength = (int)inputs.First();
            inputs.RemoveAt(0);

            Queue<long> preamble = new Queue<long>(preambleLength);
            for(int i = 0; i < inputs.Count(); i++)
            {
                var number = inputs[i];
                if (i <= preambleLength)
                {
                    preamble.Enqueue(number);
                    continue;
                }

                var combos = Combinations(preamble);

                if (combos.Select(x => x.Item1 + x.Item2).Any(x => x == number))
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
            return -1;
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
