namespace AdventOfCodeCore
{
    public abstract class Year
    {
        DayRunner _runner;

        public Year(DayRunner dayRunner)
        {
            _runner = dayRunner;
        }

        public async Task<DayResult> RunDayAsync<TIn, TOut>(double dayNum, Func<IDayOut<TIn, TOut>> getDayFunc)
        {
            await _runner.DownloadInput((int)dayNum);
            var dayresult = _runner.RunDay(dayNum, getDayFunc);
            //await _runner.SubmitAnswer(dayresult);
            return dayresult;
        }
    }
}
