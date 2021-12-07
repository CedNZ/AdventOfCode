using System.Diagnostics;

namespace AdventOfCodeCore
{
    public class DayRunner
    {
        public DayResult RunDay<TIn, TOut>(int dayNum, Func<IDayOut<TIn, TOut>> GetDay)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputsA = day.SetupInputs(File.ReadAllLines($".\\Input\\day{dayNum}.txt"));
            var inputsB = inputsA.ToList();

            var stopwatch = Stopwatch.StartNew();

            var outputA = day.A(inputsA);
            var runtimeA = stopwatch.Elapsed;

            var outputB = day.B(inputsB);
            var runtimeB = stopwatch.Elapsed - runtimeA;

            stopwatch.Stop();

            return new DayResult
            {
                Day = dayNum,
                OutputA = outputA,
                OutputB = outputB,
                RuntimeA = runtimeA,
                RuntimeB = runtimeB
            };
        }
    }

    public class DayResult
    {
        public int Day;
        public object? OutputA;
        public object? OutputB;
        public TimeSpan RuntimeA;
        public TimeSpan RuntimeB;

        public override string ToString()
        {
            return $"{Day} A: {OutputA}\t{RuntimeA}{Environment.NewLine}{Day} B: {OutputB}\t{RuntimeB}";
        }
    }
}
