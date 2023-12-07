using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day6 : IDay<Race>
    {
        public long A(List<Race> inputs)
        {
            return inputs.Select(x =>
            {
                var wins = 0;
                for (int i = 1; i < x.Time; i++)
                {
                    var boatDist = (x.Time - i) * i;
                    if (boatDist > x.Distance)
                    {
                        wins++;
                    }
                }
                return wins;
            })
            .Aggregate((agg, next) => agg * next);
        }

        public long B(List<Race> inputs)
        {
            var time = long.Parse(inputs.Select(x => x.Time.ToString()).Aggregate((agg, next) => agg += next));
            var distance = long.Parse(inputs.Select(x => x.Distance.ToString()).Aggregate((agg, next) => agg += next));

            var wins = 0;
            for (int i = 1; i < time; i++)
            {
                var boatDist = (time - i) * i;
                if (boatDist > distance)
                {
                    wins++;
                }
            }
            return wins;
        }

        public List<Race> SetupInputs(string[] inputs)
        {
            var times = inputs[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var distances = inputs[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            return times.Zip(distances).Select(x => new Race(x.First, x.Second)).ToList();
        }
    }

    public record Race(int Time, int Distance);
}
