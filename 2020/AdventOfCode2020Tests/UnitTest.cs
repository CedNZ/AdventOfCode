using System;
using Xunit;
using AdventOfCode2020;
using AdventOfCode2020.GameConsole;
using System.Collections.Generic;
using System.Collections;

namespace AdventOfCode2020Tests
{
    public class UnitTest
    {
        static Func<Day1> getDay1 = () => new Day1();


        [Theory]
        [ClassData(typeof(DayTestData))]
        public void DayTest<T>(string inputFile, Func<IDay<T>> GetDay, long expectedAnswer, bool partOne)
        {
            IDay<T> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\day{inputFile}.txt"));

            long output;
            if(partOne)
            {
                output = day.A(inputs);
            }
            else
            {
                output = day.B(inputs);
            }

            Assert.Equal(expectedAnswer, output);
        }

        [Fact]
        public void Day4_Func_Test()
        {
            Assert.True(Day4.BirthYearValid(new Passport { byr = "2002" }));
            Assert.False(Day4.BirthYearValid(new Passport { byr = "2003" }));

            Assert.True(Day4.HeightValid(new Passport { hgt = "60in" }));
            Assert.True(Day4.HeightValid(new Passport { hgt = "190cm" }));
            Assert.False(Day4.HeightValid(new Passport { hgt = "190in" }));
            Assert.False(Day4.HeightValid(new Passport { hgt = "190" }));

            Assert.True(Day4.HairColourValid(new Passport { hcl = "#123abc" }));
            Assert.False(Day4.HairColourValid(new Passport { hcl = "#123abz" }));
            Assert.False(Day4.HairColourValid(new Passport { hcl = "123abc" }));

            Assert.True(Day4.EyeColourValid(new Passport { ecl = "brn" }));
            Assert.False(Day4.EyeColourValid(new Passport { ecl = "wat" }));

            Assert.True(Day4.PassportIdValid(new Passport { pid = "000000001" }));
            Assert.False(Day4.PassportIdValid(new Passport { pid = "0123456789" }));
        }

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void Day5_Part1_Test(string seat, int expectedId)
        {
            Day5 day = new Day5();

            var testCase = new[]
            {
                seat
            };

            var inputs = day.SetupInputs(testCase);

            var results = day.A(inputs);

            Assert.Equal(expectedId, results);
        }

        [Fact]
        public void Day10_Part2_Easy()
        {
            Day10 day = new Day10();

            var inputData = new[]
            {
                "16",
                "10",
                "15",
                "5",
                "1",
                "11",
                "7",
                "19",
                "6",
                "12",
                "4",
            };

            var inputs = day.SetupInputs(inputData);

            Assert.Equal(8, day.B(inputs));
        }
    }

    public class DayTestData : IEnumerable<object[]>
    {
        Func<IDay<int>> getDay1 = () => new Day1();
        Func<IDay<(int min, int max, char letter, string password)>> getDay2 = () => new Day2();
        Func<IDay<string>> getDay3 = () => new Day3();
        Func<IDay<Passport>> getDay4 = () => new Day4();
        Func<IDay<string>> getDay5 = () => new Day5();
        Func<IDay<string>> getDay6 = () => new Day6();
        Func<IDay<Bag>> getDay7 = () => new Day7();
        Func<IDay<Instruction>> getDay8 = () => new Day8();
        Func<IDay<long>> getDay9 = () => new Day9();
        Func<IDay<int>> getDay10 = () => new Day10();
        Func<IDay<Seat>> getDay11 = () => new Day11();
        Func<IDay<string>> getDay12 = () => new Day12();
        Func<IDay<int>> getDay13 = () => new Day13();


        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {"1", getDay1, 514579, true };
            yield return new object[] {"1", getDay1, 241861950, false };

            yield return new object[] {"2", getDay2, 2, true };
            yield return new object[] {"2", getDay2, 1, false };

            yield return new object[] {"3", getDay3, 7, true };
            yield return new object[] {"3", getDay3, 336, false };

            yield return new object[] {"4", getDay4, 2, true };
            yield return new object[] {"4invalid", getDay4, 0, false };
            yield return new object[] {"4valid", getDay4, 4, false };

            yield return new object[] {"6", getDay6, 11, true };
            yield return new object[] {"6", getDay6, 6, false };

            yield return new object[] {"7", getDay7, 4, true };
            yield return new object[] {"7", getDay7, 32, false };
            yield return new object[] {"7.2", getDay7, 126, false };

            yield return new object[] {"8", getDay8, 5, true };
            yield return new object[] {"8", getDay8, 8, false };

            yield return new object[] {"9", getDay9, 127, true };
            yield return new object[] {"9", getDay9, 62, false };

            yield return new object[] { "10", getDay10, 220, true };
            yield return new object[] { "10", getDay10, 19208, false };

            //yield return new object[] { "11", getDay11, 37, true };
            //yield return new object[] { "11", getDay11, 26, false };

            yield return new object[] { "12", getDay12, 25, true };
            yield return new object[] { "12", getDay12, 286, false };

            yield return new object[] { "13", getDay13, 295, true };
            yield return new object[] { "13", getDay13, -1, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
