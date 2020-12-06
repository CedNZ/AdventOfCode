using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day6 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return GroupsUntilWhiteSpace(inputs).ToList();
        }

        public long A(List<string> inputs)
        {
            return inputs.Select(x => x.Distinct().Where(c => !char.IsWhiteSpace(c)).Count()).Sum();
        }

        public long B(List<string> inputs)
        {
            return -1;
        }

        private IEnumerable<string> GroupsUntilWhiteSpace(string[] inputs)
        {
            string output = "";
            for(int i = 0; i < inputs.Count(); i++)
            {
                if(string.IsNullOrWhiteSpace(inputs[i]))
                {
                    yield return output;
                    output = "";
                }
                else
                {
                    output += inputs[i] + "\n";
                }
            }
            yield return output;
        }
    }
}
