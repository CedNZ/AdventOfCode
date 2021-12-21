using AdventOfCodeCore;

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
                5 => RunDay(dayNum, () => new Day5()),
                6 => RunDay(dayNum, () => new Day6()),
                7 => RunDay(dayNum, () => new Day7()),
                8 => RunDay(dayNum, () => new Day8()),
                9 => RunDay(dayNum, () => new Day9()),
                10 => RunDay(dayNum, () => new Day10()),
                11 => RunDay(dayNum, () => new Day11()),
                12 => RunDay(dayNum, () => new Day12()),
                13 => RunDay(dayNum, () => new Day13()),
                14 => RunDay(dayNum, () => new Day14()),
                15 => RunDay(dayNum, () => new Day15()),
                16 => RunDay(dayNum, () => new Day16()),
                17 => RunDay(dayNum, () => new Day17()),
                18 => RunDay(dayNum, () => new Day18()),
                19 => RunDay(dayNum, () => new Day19()),
                _ => throw new NotImplementedException()
            };
        }
    }
}