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
            List<int> nums = inputs.Select(int.Parse).ToList();
            nums.Add(0);
            nums.Add(nums.Max() + 3);

            nums = nums.OrderBy(x => x).ToList();
            return nums;
        }

        public long A(List<int> inputs)
        { 
            var jumps = inputs.Zip(inputs.Skip(1)).Select(x => x.Second - x.First);

            var jump3 = jumps.Count(x => x == 3);
            var jump1 = jumps.Count(x => x == 1);

            return jump1 * jump3;
        }

        public long B(List<int> inputs)
        {
            var connectorGroups = GroupsUntil(inputs, 3).Select(g => g.Count()).ToList();

            var answerCollection = new List<long>();

            for (int i = 0; i < connectorGroups.Count(); i++)
            {
                answerCollection.Add(Tribonnaci(connectorGroups[i]) * (i > 0 ? answerCollection[i - 1] : 1));
            }

            return answerCollection.Last();
        }


        public IEnumerable<IEnumerable<int>> GroupsUntil(List<int> inputs, int split)
        {
            int startIndex = 0;
            for(int i = 1; i < inputs.Count(); i++)
            {
                if(inputs[i] - inputs[i - 1] == 3)
                {
                    yield return inputs.Skip(startIndex).Take(i - startIndex);
                    startIndex = i;
                }
            }
            yield return inputs.Skip(startIndex);
        }

        public int Tribonnaci(int n)
        {
            if(n <= 0)
            {
                return 0;
            }
            if(n <= 1)
            {
                return 1;
            }
            return Tribonnaci(n - 1) + Tribonnaci(n - 2) + Tribonnaci(n - 3);
        }
    }
}
