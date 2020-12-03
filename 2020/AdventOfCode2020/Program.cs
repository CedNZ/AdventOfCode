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

            IDay day = dayNum switch
            {
                1 => new Day1(),
                _ => null
            };

            var inputs = System.IO.File.ReadAllLines($"..\\..\\..\\Inputs\\day{dayNum}.txt").Select(int.Parse).ToList();

            var outputA = day.A(inputs);


            Console.WriteLine($"Day {dayNum} Answer A: {outputA}");
            Console.Read();
        }
    }
}
