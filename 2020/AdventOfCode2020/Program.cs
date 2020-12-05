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
                1 => RunIntDay(dayNum),
                2 => RunOther(dayNum),
                3 => RunStringDay(dayNum),
                4 => RunCustom(dayNum),
                5 => RunStringDay(dayNum),
                _ => (-1, -1),
            };

            Console.WriteLine($"Day {dayNum} Answer A: {result.A}");
            Console.WriteLine($"Day {dayNum} Answer B: {result.B}");
            Console.Read();
        }

        static (long A, long B) RunIntDay(int dayNum)
        {
            IDay<int> day = dayNum switch
            {
                1 => new Day1(),
                _ => throw new NotImplementedException(),
            };

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            var outputA = day.A(inputs);
            var outputB = day.B(inputs);

            return (outputA, outputB);
        }

        static (long A, long B) RunStringDay(int dayNum)
        {
            IDay<string> day = dayNum switch
            {
               3 => new Day3(),
               5 => new Day5(),
                _ => throw new NotImplementedException(),
            };

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            var outputA = day.A(inputs);
            var outputB = day.B(inputs);

            return (outputA, outputB);
        }

        static (long A, long B) RunOther(int dayNum)
        {
            IDay<(int min, int max, char letter, string password)> day = new Day2();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            var outputA = day.A(inputs);
            var outputB = day.B(inputs);

            return (outputA, outputB);
        }

        static (long A, long B) RunCustom(int dayNum)
        {
            IDay<Passport> day = new Day4();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt"));

            var outputA = day.A(inputs);
            var outputB = day.B(inputs);

            return (outputA, outputB);
        }
    }
}
