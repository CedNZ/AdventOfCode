using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCodeCore;
using AoC_2015;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class Tests_2015
    {
        [Theory]
        [ClassData(typeof(DayTestData_2015))]
        public void DayTest<TIn, TOut>(object inputFile, Func<IDayOut<TIn, TOut>> GetDay, TOut expectedAnswer, bool partOne)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\2015\\day{inputFile}.txt"));

            TOut result;
            if (partOne)
            {
                result = day!.A(inputs!);
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

    public class DayTestData_2015 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, () => new Day2(), 101, true };
            /*
            yield return new object[] { "5", () => new Day5(), "CMZ", true };
            yield return new object[] { "5", () => new Day5(), "MCD", false };
            yield return new object[] { "7", () => new Day7(), 95437, true };
            yield return new object[] { "7", () => new Day7(), 24933642, false };
            yield return new object[] { "8", () => new Day8(), 21, true };
            yield return new object[] { "8", () => new Day8(), 8, false };/**/
            //yield return new object[] { 9, () => new Day9(), 13, true };
            //yield return new object[] { 9, () => new Day9(), 1, false };
            //yield return new object[] { "9B", () => new Day9(), 36, false };
            //yield return new object[] { 10, () => new Day10(), 13140, true };
            //yield return new object[] { 11, () => new Day11(), 10605, true };
            //yield return new object[] { 11, () => new Day11(), 2713310158, false };
            //yield return new object[] { 12, () => new Day12(), 31, true };
            //yield return new object[] { 13, () => new Day13(), 13, true };
            //yield return new object[] { 13, () => new Day13(), 140, false };
            //yield return new object[] { 14, () => new Day14(), 24, true };
            //yield return new object[] { 14, () => new Day14(), 93, false };
            //yield return new object[] { 15, () => new Day15(), 26, true };
            //yield return new object[] { 15, () => new Day15(), 56000011, false };

            //yield return new object[] { 16, () => new Day16(), 1651, true };

            //yield return new object[] { 20, () => new Day20(), 3, true };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
