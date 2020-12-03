using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day1 : IDay<int>
    {
        public List<int> SetupInputs(string[] inputs)
        {
            return inputs.Select(int.Parse).ToList();
        }


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
            foreach(int number in inputs)
            {
                foreach(var number2 in inputs.Where(x => x + number < 2020))
                {
                    foreach (var number3 in inputs.Where(x => x + number + number2 == 2020))
                    {
                        return number * number2 * number3;
                    }
                }
            }
            return -1;
        }
    }
}
