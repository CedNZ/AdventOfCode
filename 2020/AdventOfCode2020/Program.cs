using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2020");
            Console.WriteLine("Enter day number: ");
            var dayNum = int.Parse(Console.ReadLine());

            (long A, long B) result = dayNum switch
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
                _ => (-1, -1),
            };

            Console.WriteLine($"Day {dayNum} Answer A: {result.A}");
            Console.WriteLine($"Day {dayNum} Answer B: {result.B}");
            Console.Read();
        }

        static (long A, long B) RunDay<T>(int dayNum, Func<IDay<T>> GetDay)
        {
            IDay<T> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            var outputA = day.A(inputs);
            var outputB = day.B(inputs);

            return (outputA, outputB);
        }
    }
}
