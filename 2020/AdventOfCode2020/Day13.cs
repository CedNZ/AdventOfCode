using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13 : IDay<int>
    {
        public List<int> SetupInputs(string[] inputs)
        {
            var depart = int.Parse(inputs[0]);

            var busIds = inputs[1].Split(',').Where(x => x != "x").Select(int.Parse).ToList();

            busIds.Insert(0, depart);

            return busIds;
        }

        public long A(List<int> inputs)
        {
            int depart = inputs.First();
            Dictionary<int, int> busses = new Dictionary<int, int>();

            foreach(var busId in inputs.Skip(1))
            {
                var mod = depart % busId;
                var div = depart / busId;

                var nextClosestTime = ((div * busId) + busId);

                busses.Add(busId, nextClosestTime);
            }

            var bus = busses.OrderBy(kvp => kvp.Value).First();

            var waitTime = bus.Value - depart;

            return bus.Key * waitTime;
        }

        public long B(List<int> inputs)
        {
            return -1;
        }
    }
}
