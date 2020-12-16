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
            return inputs.GroupsUntilWhiteSpace().ToList();
        }

        public long A(List<string> inputs)
        {
            return inputs.Select(x => x.Distinct().Where(c => !char.IsWhiteSpace(c)).Count()).Sum();
        }

        public long B(List<string> inputs)
        {
            return inputs.Select(s => s.Split('\n')
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .Aggregate((curr, next) => string.Concat(curr.Intersect(next))))
             .Select(x => x.Count())
             .Sum();
        }
    }
}
