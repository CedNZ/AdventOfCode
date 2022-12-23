using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode2020
{
    class Program
    {
        private static Stopwatch stopwatch;
        private static TimeSpan runtimeA;
        private static TimeSpan runtimeB;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2020");
            var run = true;
            while(run) {
                Console.WriteLine("Enter day number: ");
                var dayNum = int.Parse(Console.ReadLine());

                (object A, object B) result = dayNum switch
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
                    //17 => RunDay(dayNum, () => new Day17()),
                    18 => RunDay(dayNum, () => new Day18()),

                    21 => RunDay(dayNum, () => new Day21()),
                    22 => RunDay(dayNum, () => new Day22()),
                    23 => RunDay(dayNum, () => new Day23()),
                    _ => (-1, -1),
                };

                Console.WriteLine($"Day {dayNum} Answer A: {result.A}\nCompleted in {runtimeA}");
                Console.WriteLine($"Day {dayNum} Answer B: {result.B}\nCompleted in {runtimeB}");
                Console.WriteLine("\nRerun? y/N");
                run = Console.ReadLine().ToUpper() == "Y";
            }
        }

        static (TOut A, TOut B) RunDay<TIn, TOut>(int dayNum, Func<IDayOut<TIn, TOut>> GetDay)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputsA = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));
            var inputsB = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            stopwatch = Stopwatch.StartNew();

            var outputA = day.A(inputsA);
            runtimeA = stopwatch.Elapsed;

            var outputB = day.B(inputsB);
            runtimeB = stopwatch.Elapsed - runtimeA;

            stopwatch.Stop();

            return (outputA, outputB);
        }
    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static IEnumerable<string> GroupsUntilWhiteSpace(this IEnumerable<string> inputs)
        {
            string output = "";

            foreach(var input in inputs)
            {
                if(string.IsNullOrWhiteSpace(input))
                {
                    yield return output.Trim();
                    output = "";
                }
                else
                {
                    output += input + "\n";
                }
            }
            yield return output.Trim();
        }
    }
}
