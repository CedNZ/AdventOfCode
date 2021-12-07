namespace AdventOfCodeCore
{
    public abstract class Year
    {
        DayRunner _runner;

        public Year(DayRunner dayRunner)
        {
            _runner = dayRunner;
        }

        public DayResult RunDay<TIn, TOut>(int dayNum, Func<IDayOut<TIn, TOut>> getDayFunc)
        {
            return _runner.RunDay(dayNum, getDayFunc);
        }

        public abstract IEnumerable<DayResult> RunAll();

        public abstract DayResult RunDay(int day);
    }
}
