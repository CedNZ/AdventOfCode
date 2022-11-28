using AdventOfCodeCore;

namespace AoC_2021
{
    public class AoC_2021 : Year
    {
        public AoC_2021(DayRunner dayRunner) 
            : base(dayRunner)
        {
        }

        public override async Task<DayResult> RunDayAsync(int dayNum)
        {
            return dayNum switch
            {
                1 => await RunDayAsync(dayNum, () => new Day1()),
                2 => await RunDayAsync(dayNum, () => new Day2()),
                3 => await RunDayAsync(dayNum, () => new Day3()),
                4 => await RunDayAsync(dayNum, () => new Day4()),
                5 => await RunDayAsync(dayNum, () => new Day5()),
                6 => await RunDayAsync(dayNum, () => new Day6()),
                7 => await RunDayAsync(dayNum, () => new Day7()),
                8 => await RunDayAsync(dayNum, () => new Day8()),
                9 => await RunDayAsync(dayNum, () => new Day9()),
                10 => await RunDayAsync(dayNum, () => new Day10()),
                11 => await RunDayAsync(dayNum, () => new Day11()),
                12 => await RunDayAsync(dayNum, () => new Day12()),
                13 => await RunDayAsync(dayNum, () => new Day13()),
                14 => await RunDayAsync(dayNum, () => new Day14()),
                15 => await RunDayAsync(dayNum, () => new Day15()),
                16 => await RunDayAsync(dayNum, () => new Day16()),
                17 => await RunDayAsync(dayNum, () => new Day17()),
                18 => await RunDayAsync(dayNum, () => new Day18()),
                19 => await RunDayAsync(dayNum, () => new Day19()),
                20 => await RunDayAsync(dayNum, () => new Day20()),
                21 => await RunDayAsync(dayNum, () => new Day21()),
                _ => throw new NotImplementedException()
            };
        }
    }
}