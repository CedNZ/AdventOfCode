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
                _ => await RunDayAsync(day, () => new Day1()),
            };
        }
    }
}