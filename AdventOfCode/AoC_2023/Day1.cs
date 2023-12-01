using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day1 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            return inputs.Sum(x => (x.First(char.IsDigit) - '0') * 10 + x.Last(char.IsDigit) - '0');
        }

        public long B(List<string> inputs)
        {
            return inputs.Sum(x => GetFirstNumber(x) * 10 + GetLastNumber(x));
        }

        public int GetFirstNumber(string s)
        {
            int? num = null;
            var index = 0;
            while (num is null)
            {
                num = GetNumber(s.Substring(index++));
            }
            return num.Value;
        }

        public int GetLastNumber(string s)
        {
            int? num = null;
            var index = s.Length - 1;
            while (num is null)
            {
                num = GetNumber(s.Substring(index--));
            }
            return num.Value;
        }

        public int? GetNumber(string s)
        {
            if (char.IsDigit(s[0]))
            {
                return s[0] - '0';
            }

            return s switch
            {
                [ 'o', 'n', 'e', .. _] => 1,
                [ 't', 'w', 'o', .. _] => 2,
                [ 't', 'h', 'r', 'e', 'e', .. _ ] => 3,
                [ 'f', 'o', 'u', 'r', .. _] => 4,
                [ 'f', 'i', 'v', 'e', .. _] => 5,
                [ 's', 'i', 'x', .. _] => 6,
                [ 's', 'e', 'v', 'e', 'n', .. _] => 7,
                [ 'e', 'i', 'g', 'h', 't', .. _] => 8,
                [ 'n', 'i', 'n', 'e', .. _] => 9,
                _ => null,
            };
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return [.. inputs];
        }
    }
}
