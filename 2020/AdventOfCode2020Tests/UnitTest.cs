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
                Assert.Equal(336, day.B(inputs));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Day4_Test(bool partOne)
        {
            Day4 day = new Day4();

            var testCase = new[]
            {
                "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                "byr:1937 iyr:2017 cid:147 hgt:183cm",
                "",
                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                "hcl:#cfa07d byr:1929",
                "",
                "hcl:#ae17e1 iyr:2013",
                "eyr:2024",
                "ecl:brn pid:760753108 byr:1931",
                "hgt:179cm",
                "",
                "hcl:#cfa07d eyr:2025 pid:166559648",
                "iyr:2011 ecl:brn hgt:59in",
            };

            var inputs = day.SetupInputs(testCase);

            if(partOne)
                Assert.Equal(2, day.A(inputs));
            else
                Assert.Equal(336, day.B(inputs));
        }
    }
}
