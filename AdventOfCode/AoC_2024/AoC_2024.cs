using AdventOfCodeCore;

namespace AoC_2024
{
    public class AoC_2024(DayRunner dayRunner) : Year(dayRunner)
    {
        public override async Task<DayResult> RunDayAsync(double day)
        {
            return day switch
            {
                1 => await RunDayAsync(day, () => new Day1()),
                2 => await RunDayAsync(day, () => new Day2()),
                3 => await RunDayAsync(day, () => new Day3()),
                4 => await RunDayAsync(day, () => new Day4()),
                5 => await RunDayAsync(day, () => new Day5()),
                6 => await RunDayAsync(day, () => new Day6()),
                7 => await RunDayAsync(day, () => new Day7()),
                8 => await RunDayAsync(day, () => new Day8()),
                9 => await RunDayAsync(day, () => new Day9()),
                10 => await RunDayAsync(day, () => new Day10()),
                11 => await RunDayAsync(day, () => new Day11()),
                12 => await RunDayAsync(day, () => new Day12()),
                13 => await RunDayAsync(day, () => new Day13()),
                _ => new DayResult { OutputA = "Add the day to the year Dumbass" },
            };
        }
    }
}
