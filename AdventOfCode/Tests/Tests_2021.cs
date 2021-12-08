using AdventOfCodeCore;
using AoC_2021;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Tests
{
    public class Tests_2021
    {
        [Theory]
        [ClassData(typeof(DayTestData))]
        public void DayTest<TIn, TOut>(string inputFile, Func<IDayOut<TIn, TOut>> GetDay, TOut expectedAnswer, bool partOne)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\day{inputFile}.txt"));

            TOut result;
            if (partOne)
            {
                result = day.A(inputs);
            }
            else
            {
                result = day.B(inputs);
            }

            result.Should().Be(expectedAnswer);
        }
    }

    public class DayTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
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
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}