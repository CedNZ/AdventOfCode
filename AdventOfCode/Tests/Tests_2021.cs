using AdventOfCodeCore;
using AoC_2021;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

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

        [Fact]
        public void Day16Tests_1()
        {
            Day16.MainPacket = null;
            Day16.BuildPackets("D2FE28".ToList(), out var packet);

            packet.Value.Should().Be(2021);

        }

        [Fact]
        public void Day16Tests_2()
        {
            Day16.MainPacket = null;
            Day16.BuildPackets("38006F45291200".ToList(), out var packet);

            packet.SubPackets.Should().HaveCount(2);

            packet.SubPackets[0].Value.Should().Be(10);
            packet.SubPackets[1].Value.Should().Be(20);
        }

        [Fact]
        public void Day16Tests_3()
        {
            Day16.MainPacket = null;
            Day16.BuildPackets("EE00D40C823060".ToList(), out var packet);

            packet.SubPackets.Should().HaveCount(3);

            packet.SubPackets[0].Value.Should().Be(1);
            packet.SubPackets[1].Value.Should().Be(2);
            packet.SubPackets[2].Value.Should().Be(3);
        }

        [Theory]
        [InlineData("8A004A801A8002F478", 16)]
        [InlineData("620080001611562C8802118E34", 12)]
        [InlineData("C0015000016115A2E0802F182340", 23)]
        [InlineData("A0016C880162017C3686B18A3D4780", 31)]
        public void Day16_Tests(string input, long expected)
        {
            Day16.MainPacket = null;
            var day = new Day16();

            day.A(input.ToList()).Should().Be(expected);
        }

        [Theory]
        [InlineData("C200B40A82", 3)]
        [InlineData("04005AC33890", 54)]
        [InlineData("880086C3E88112", 7)]
        [InlineData("CE00C43D881120", 9)]
        [InlineData("D8005AC2A8F0", 1)]
        [InlineData("F600BC2D8F", 0)]
        [InlineData("9C005AC2F8F0", 0)]
        [InlineData("9C0141080250320F1802104A08", 1)]
        public void Day16_PartB_Tests(string input, long expected)
        {
            Day16.MainPacket = null;
            var day = new Day16();

            day.B(input.ToList()).Should().Be(expected);
        }

        [Theory]
        [InlineData("[[1,2],[[3,4],5]]", 143)]
        [InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        [InlineData("[[[[6,6],[0,6]],[[6,7],[7,7]]],[[[7,7],[7,7]],[[7,8],[8,9]]]]", 3816)]
        [InlineData("[[10,[11,12]],20]", 1321)]
        public void Day18_Magnitude_Tests(string input, long expected)
        {
            var day = new Day18();
            var inputs = day.SetupInputs(new[] { input });
            day.A(inputs).Should().Be(expected);
        }
    }

    public class DayTestData : IEnumerable<object[]>
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
            yield return new object[] { "15.3", () => new Day15(), 12, true };/**/
            yield return new object[] { "15", () => new Day15(), 315, false };
            yield return new object[] { "17", () => new Day17(), 45, true };
            yield return new object[] { "17", () => new Day17(), 112, false };
            yield return new object[] { "18", () => new Day18(), 4140, true };
            yield return new object[] { "18", () => new Day18(), 3993, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}