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
                gamma[i] = MostCommonBit(inputs, i).GetValueOrDefault();
            }

            bool[] epsilon = gamma.Select(x => !x).ToArray();

            return BoolArrayToDecimal(epsilon) * BoolArrayToDecimal(gamma);
        }

        public bool? MostCommonBit(IEnumerable<string> inputs, int index)
        {
            var oneCount = inputs.Select(x => x[index]).Count(x => x == '1');

            var zeroCount = inputs.Count() - oneCount;

            if (oneCount == zeroCount)
            { 
                return null;
            }    

            return oneCount > zeroCount;
        }

        public int BoolArrayToDecimal(bool[] bools)
        {
            var sum = 0;
            bools.Reverse().Select((x, i) => sum += (x ? 1 << i : 0)).ToList();
            return sum;
        }

        public bool[] StringToBoolArray(string s)
        {
            return s.Select(c => c == '1').ToArray();
        }

        public long B(List<string> inputs)
        {
            var oxygenCandidates = inputs.ToList();
            var carbonCandidates = inputs.ToList();

            int index = 0;
            while (oxygenCandidates.Count > 1)
            {
                var mcb = MostCommonBit(oxygenCandidates, index).GetValueOrDefault(true);
                oxygenCandidates = oxygenCandidates.Where(x => (x[index] == '1') == mcb).ToList();
                index++;
            }

            index = 0;
            while (carbonCandidates.Count > 1)
            {
                var mcb = MostCommonBit(carbonCandidates, index).GetValueOrDefault(true);
                carbonCandidates = carbonCandidates.Where(x => (x[index] == '0') == mcb).ToList();
                index++;
            }

            return BoolArrayToDecimal(StringToBoolArray(oxygenCandidates.Single())) 
                * BoolArrayToDecimal(StringToBoolArray(carbonCandidates.Single()));
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
