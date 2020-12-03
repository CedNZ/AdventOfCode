using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day2 : IDay<(int min, int max, char letter, string password)>
    {
        public List<(int min, int max, char letter, string password)> SetupInputs(string[] inputs)
        {
            string pattern = @"(\d+)-(\d+) (\w): (\w+)";

            List<(int min, int max, char letter, string password)> result = new List<(int min, int max, char letter, string password)>(inputs.Count());

            foreach(var input in inputs)
            {
                var matches = Regex.Matches(input, pattern);

                var min = int.Parse(matches.First().Groups[1].Value);
                var max = int.Parse(matches.First().Groups[2].Value);
                var letter = matches.First().Groups[3].Value[0];
                var password = matches.First().Groups[4].Value;

                result.Add((min, max, letter, password));
            }
            return result;
        }

        public int A(List<(int min, int max, char letter, string password)> inputs)
        {
            int validPasswords = 0;
            foreach(var input in inputs)
            {
                int letterCount = input.password.Count(x => x == input.letter);
                if (letterCount >= input.min && letterCount <= input.max)
                {
                    validPasswords++;
                }
            }
            return validPasswords;
        }

        public int B(List<(int min, int max, char letter, string password)> inputs)
        {
            return -1;
        }
    }
}
