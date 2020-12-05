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
            if(partOne)
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

            if(partOne)
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

        [Fact]
        public void Day4_Part1_Test()
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

            Assert.Equal(2, day.A(inputs));
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

        [Fact]
        public void Day4_Part2_Invalid_Test()
        {
            Day4 day = new Day4();

            string[] invalidPassports = new[]
            {
                "eyr:1972 cid:",
                "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
                "",
                "iyr:2019",
                "hcl:#602927 eyr:1967 hgt:170cm",
                "ecl:grn pid:012533040 byr:1946",
                "",
                "hcl:dab227 iyr:2012",
                "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
                "",
                "hgt:59cm ecl:zzz",
                "eyr:2038 hcl:74454a iyr:2023",
                "pid:3556412378 byr:2007",
            };

            var inputs = day.SetupInputs(invalidPassports);

            Assert.Equal(0, day.B(inputs));
        }

        [Fact]
        public void Day4_Part2_Valid_Test()
        {
            Day4 day = new Day4();

            string[] validPassports = new[]
            {
                "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
                "hcl:#623a2f",
                "",
                "eyr:2029 ecl:blu cid:129 byr:1989",
                "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
                "",
                "hcl:#888785",
                "hgt:164cm byr:2001 iyr:2015 cid:88",
                "pid:545766238 ecl:hzl",
                "eyr:2022",
                "",
                "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719",
            };

            var inputs = day.SetupInputs(validPassports);

            Assert.Equal(4, day.B(inputs));
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

            Assert.Equal(expectedId, day.A(inputs));
        }
    }
}
