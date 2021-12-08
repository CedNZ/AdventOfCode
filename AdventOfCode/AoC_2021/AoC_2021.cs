﻿using AdventOfCodeCore;

namespace AoC_2021
{
    public class AoC_2021 : Year
    {
        public AoC_2021(DayRunner dayRunner) 
            : base(dayRunner)
        {
        }

        public override IEnumerable<DayResult> RunAll()
        {
            for (int i = 1; i < 2; i++)
            {
                yield return RunDay(i);
            }
        }

        public override DayResult RunDay(int dayNum)
        {
            return dayNum switch
            {
                1 => RunDay(dayNum, () => new Day1()),
                2 => RunDay(dayNum, () => new Day2()),
                3 => RunDay(dayNum, () => new Day3()),
                4 => RunDay(dayNum, () => new Day4()),
                _ => throw new NotImplementedException()
            };
        }
    }
}