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
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}