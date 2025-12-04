using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCodeCore;
using AoC_2023;
using Shouldly;
using Xunit;

namespace Tests
{
    public class Tests_2023
    {
        [Theory]
        [ClassData(typeof(DayTestData_2023))]
        public void DayTest<TIn, TOut>(double inputFile, Func<IDayOut<TIn, TOut>> GetDay, TOut expectedAnswer, bool partOne)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\2023\\day{inputFile}.txt"));

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
                (r.ToString("N")).ShouldBe(e.ToString("N"));
            }
            else
            {
                result.ShouldBe(expectedAnswer);
            }
        }
    }

    public class DayTestData_2023 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {/*
            */
            //yield return new object[] { 1, () => new Day1(), 281, false };
            //yield return new object[] { 2, () => new Day2(), 8, true };
            //yield return new object[] { 2, () => new Day2(), 2286, false };
            //yield return new object[] { 3, () => new Day3(), 4361, true };
            //yield return new object[] { 4, () => new Day4(), 30, false };
            //yield return new object[] { 5, () => new Day5(), 46, false };
            //yield return new object[] { 7, () => new Day7(), 6440, true };
            //yield return new object[] { 7.1, () => new Day7b(), 5905, false };
            //yield return new object[] { 10, () => new Day10(), 8, true };
            //yield return new object[] { 11, () => new Day11(), 374, true };
            yield return new object[] { 13, () => new Day13(), 405, true };
            yield return new object[] { 13, () => new Day13(), 400, false };
            yield return new object[] { 14, () => new Day14(), 136, true };
            yield return new object[] { 15, () => new Day15(), 145, false };
            yield return new object[] { 16, () => new Day16(), 46, true };
            yield return new object[] { 18.1, () => new Day18_1(), 952408144115, false };
            yield return new object[] { 19, () => new Day19(), 19114, true };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
