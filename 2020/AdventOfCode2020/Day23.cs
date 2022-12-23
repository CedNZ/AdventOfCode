using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day23 : IDay<int>
    {
        public List<int> SetupInputs(string[] inputs)
        {
            return inputs[0].ToCharArray().Select(x => int.Parse(x.ToString())).ToList();
        }

        public long A(List<int> inputs)
        {
            for(int i = 0; i < 100; i++)
            {
                var currentCup = inputs.First();
                inputs.RemoveAt(0);

                var threeCups = inputs.Take(3).ToList();
                inputs.RemoveRange(0, 3);

                var targetCup = currentCup - 1;
                while (!inputs.Contains(targetCup))
                {
                    targetCup--;
                    if(targetCup == 0)
                    {
                        targetCup = 9;
                    }
                }
                var insertionIndex = inputs.IndexOf(targetCup);


                insertionIndex++; //insert after the selected Cup

                inputs.InsertRange(insertionIndex, threeCups);
                inputs.Add(currentCup);
            }

            var oneIndex = inputs.IndexOf(1);

            var endOfList = inputs.Take(oneIndex).ToList();
            inputs.RemoveRange(0, oneIndex);
            inputs.AddRange(endOfList);

            return inputs.Skip(1).Aggregate((curr, next) => curr * 10 + next);
        }

        public long B(List<int> inputs)
        {
            return -1;
        }
    }
}
