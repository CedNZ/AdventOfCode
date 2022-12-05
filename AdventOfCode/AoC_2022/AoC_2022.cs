using AdventOfCodeCore;

namespace AoC_2022
{
    public class AoC_2022 : Year
    {
        public AoC_2022(DayRunner dayRunner) 
            : base(dayRunner)
        {
        }

        public override async Task<DayResult> RunDayAsync(int day)
        {
            return day switch
            {
                1 => await RunDayAsync(day, () => new Day1()),
                2 => await RunDayAsync(day, () => new Day2()),
                3 => await RunDayAsync(day, () => new Day3()),
                4 => await RunDayAsync(day, () => new Day4()),
                5 => await RunDayAsync(day, () => new Day5()),
                _ => await RunDayAsync(day, () => new Day1()),
            };
        }
    }
}