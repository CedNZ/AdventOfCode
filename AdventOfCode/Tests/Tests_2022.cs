using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCodeCore;
using AoC_2022;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class Tests_2022
    {
        [Theory]
        [ClassData(typeof(DayTestData_2022))]
        public void DayTest<TIn, TOut>(string inputFile, Func<IDayOut<TIn, TOut>> GetDay, TOut expectedAnswer, bool partOne)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\2022\\day{inputFile}.txt"));

            TOut result;
            if (partOne)
            {
                result = day.A(inputs);
            }
            else
            {
                result = day.B(inputs);
            }

            if (result is long r && expectedAnswer is long e)
            {
                (r.ToString("N")).Should().Be(e.ToString("N"));
            }
            else
            {
                result.Should().Be(expectedAnswer);
            }
        }
    }

    public class DayTestData_2022 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {/*
            yield return new object[] { "1", () => new Day1(), 7, true };
            yield return new object[] { "1", () => new Day1(), 5, false };
            yield return new object[] { "2", () => new Day2(), 150, true };
            yield return new object[] { "2", () => new Day2(), 900, false };
            yield return new object[] { "3", () => new Day3(), 198, true };
            yield return new object[] { "3", () => new Day3(), 230, false };
            yield return new object[] { "4", () => new Day4(), 494, true }; //should be expecting 4512
            yield return new object[] { "4", () => new Day4(), 1924, false };
            yield return new object[] { "5", () => new Day5(), 5, true };
            yield return new object[] { "5", () => new Day5(), 12, false };
            yield return new object[] { "6", () => new Day6(), 5934, true };
            //yield return new object[] { "6", () => new Day6(), 26984457539, false }; //commented out for excessive runtimes
            yield return new object[] { "7", () => new Day7(), 37, true };
            yield return new object[] { "7", () => new Day7(), 168, false };
            yield return new object[] { "8", () => new Day8(), 26, true };
            yield return new object[] { "8", () => new Day8(), 61229, false }; /*
            yield return new object[] { "9", () => new Day9(), 15, true };
            yield return new object[] { "9", () => new Day9(), 1134, false };
            yield return new object[] { "10", () => new Day10(), 26397, true };
            yield return new object[] { "10", () => new Day10(), 288957, false }; 
            yield return new object[] { "11", () => new Day11(), 1656, true };
            yield return new object[] { "11", () => new Day11(), 195, false };/*
            yield return new object[] { "12", () => new Day12(), 10, true };
            yield return new object[] { "12.1", () => new Day12(), 19, true };
            yield return new object[] { "12.2", () => new Day12(), 226, true };
            yield return new object[] { "12", () => new Day12(), 36, false };
            yield return new object[] { "12.1", () => new Day12(), 103, false };
            yield return new object[] { "12.2", () => new Day12(), 3509, false };/*
            //yield return new object[] { "13", () => new Day13(), 17, true };
            yield return new object[] { "14", () => new Day14(), 1588, true };
            yield return new object[] { "14", () => new Day14(), 2188189693529, false };
            yield return new object[] { "15", () => new Day15(), 40, true };
            yield return new object[] { "15.1", () => new Day15(), 40, true };
            yield return new object[] { "15.2", () => new Day15(), 10, true };
            yield return new object[] { "15.3", () => new Day15(), 12, true };/*
            yield return new object[] { "15", () => new Day15(), 315, false };
            yield return new object[] { "17", () => new Day17(), 45, true };
            yield return new object[] { "17", () => new Day17(), 112, false };
            yield return new object[] { "18", () => new Day18(), 4140, true };
            yield return new object[] { "18", () => new Day18(), 3993, false };
            yield return new object[] { "19", () => new Day19(), 79, true };
            //yield return new object[] { "19.1", () => new Day19(), 3, true }; // count >= 3
            yield return new object[] { "19", () => new Day19(), 3621, false }; /**/
            //yield return new object[] { "20", () => new Day20(), 35, true };
            //yield return new object[] { "20", () => new Day20(), 3351, false };
            yield return new object[] { "5", () => new Day5(), "CMZ", true };
            yield return new object[] { "5", () => new Day5(), "MCD", false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
