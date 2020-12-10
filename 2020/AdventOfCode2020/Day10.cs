using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day10 : IDay<int>
    {
        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.Select(int.Parse).ToList();
        }

        public long A(List<int> inputs)
        {
            List<int> nums = inputs.ToList();
            nums.Add(0);
            nums.Add(nums.Max() + 3);

            nums = nums.OrderBy(x => x).ToList();

            var jumps = nums.Zip(nums.Skip(1)).Select(x => x.Second - x.First);

            var jump3 = jumps.Count(x => x == 3);
            var jump1 = jumps.Count(x => x == 1);

            return jump1 * jump3;
        }

        public long B(List<int> inputs)
        {
            return -1;
        }
        
    }
}
