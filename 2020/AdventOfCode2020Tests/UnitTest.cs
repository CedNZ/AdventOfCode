using System;
using Xunit;
using AdventOfCode2020;

namespace AdventOfCode2020Tests
{
    public class UnitTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Day1_Test(bool partOne)
        {
            Day1 day = new Day1();

            var testCase = new[]
            {
                "1721",
                "979",
                "366",
                "299",
                "675",
                "1456",
            };

            var inputs = day.SetupInputs(testCase);
            if (partOne)
                Assert.Equal(514579, day.A(inputs));
            else
                Assert.Equal(241861950, day.B(inputs));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Day2_Test(bool partOne)
        {
            Day2 day = new Day2();

            var testCase = new[]
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc",
            };

            var inputs = day.SetupInputs(testCase);

            if (partOne)
                Assert.Equal(2, day.A(inputs));
            else
                Assert.Equal(1, day.B(inputs));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Day3_Test(bool partOne)
        {
            Day3 day = new Day3();

            var testCase = new[]
            {
                "..##.......",
                "#...#...#..",
                ".#....#..#.",
                "..#.#...#.#",
                ".#...##..#.",
                "..#.##.....",
                ".#.#.#....#",
                ".#........#",
                "#.##...#...",
                "#...##....#",
                ".#..#...#.#",
            };

            var inputs = day.SetupInputs(testCase);

            if(partOne)
                Assert.Equal(7, day.A(inputs));
            else
                Assert.Equal(1, day.B(inputs));
        }
    }
}
