using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day1 : IDay
    {
        public int A(List<int> inputs)
        {
            for(int i = 0; i < inputs.Count(); i++)
            {
                for(int j = i + 1; j < inputs.Count(); j++)
                {
                    if(inputs[i] + inputs[j] == 2020)
                    {
                        return inputs[i] * inputs[j];
                    }
                }
            }
            return -1;
        }

        public int B(List<int> inputs)
        {
            throw new NotImplementedException();
        }
    }
}
