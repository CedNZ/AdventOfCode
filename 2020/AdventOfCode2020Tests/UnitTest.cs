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
        public void DayTest<Tin, Tout>(string inputFile, Func<IDayOut<Tin, Tout>> GetDay, Tout expectedAnswer, bool partOne)
        {
            IDayOut<Tin, Tout> day = GetDay();

            var inputs = day.SetupInputs(System.IO.File.ReadAllLines($"..\\..\\..\\TestInput\\day{inputFile}.txt"));

            Tout output;
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

        [Theory]
        [InlineData("0,3,6", 436)]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        [InlineData("1,2,3", 27)]
        [InlineData("2,3,1", 78)]
        [InlineData("3,2,1", 438)]
        [InlineData("3,1,2", 1836)]
        public void Day15_Part1_Tests(string nums, int expectedAnswer)
        {
            Day15 day = new Day15();

            var input = day.SetupInputs(new[] { nums });

            Assert.Equal(expectedAnswer, day.A(input));
        }

        [Theory]
        [InlineData("0,3,6", 175594)]
        [InlineData("1,3,2", 2578)]
        [InlineData("2,1,3", 3544142)]
        [InlineData("1,2,3", 261214)]
        [InlineData("2,3,1", 6895259)]
        [InlineData("3,2,1", 18)]
        [InlineData("3,1,2", 362)]
        public void Day15_Part2_Tests(string nums, int expectedAnswer)
        {
            Day15 day = new Day15();

            var input = day.SetupInputs(new[] { nums });

            Assert.Equal(expectedAnswer, day.B(input));
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71, 231)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51, 51)]
        [InlineData("2 * 3 + (4 * 5)", 26, 46)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437, 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240, 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632, 23340)]
        public void Day18_Tests(string input, int expectedAnswerA, int expectedAnswerB)
        {
            Day18 day = new Day18();

            Assert.Equal(expectedAnswerA, day.A(new List<string> { input }));
            Assert.Equal(expectedAnswerB, day.B(new List<string> { input }));
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
        Func<IDay<string>> getDay14 = () => new Day14();
        Func<IDay<int>> getDay15 = () => new Day15();
        Func<IDay<Ticket>> getDay16 = () => new Day16();

        Func<IDay<(List<string> Allergens, List<string> Ingredients)>> getDay21 = () => new Day21();

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
            
            yield return new object[] { "14", getDay14, 165, true };
            yield return new object[] { "14b", getDay14, 208, false };
            /**/
            yield return new object[] { "16", getDay16, 71, true };
            yield return new object[] { "16b", getDay16, -1, false };

            yield return new object[] { "21", getDay21, 5, true };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
