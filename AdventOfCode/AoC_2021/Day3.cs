using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day3 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            bool[] gamma = new bool[inputs.First().Length];

            for (int i = 0; i < inputs.First().Length; i++)
            {
                gamma[i] = MostCommonBit(inputs, i);
            }

            bool[] epsilon = gamma.Select(x => !x).ToArray();

            return BoolArrayToDecimal(epsilon) * BoolArrayToDecimal(gamma);
        }

        public bool MostCommonBit(IEnumerable<string> inputs, int index)
        {
            var oneCount = inputs.Select(x => x[index]).Count(x => x == '1');

            return oneCount > inputs.Count() / 2;
        }

        public int BoolArrayToDecimal(bool[] bools)
        {
            var sum = 0;
            bools.Reverse().Select((x, i) => sum += (x ? 1 << i : 0)).ToList();
            return sum;
        }

        public long B(List<string> inputs)
        {

            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
