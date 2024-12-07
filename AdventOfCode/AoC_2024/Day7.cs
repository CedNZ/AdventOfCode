using System.Text;
using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day7 : IDay<(long target, List<int> nums)>
    {
        public long A(List<(long target, List<int> nums)> inputs)
        {
            var good = 0;
            long result = 0L;
            foreach (var input in inputs)
            {
                var (target, nums) = input;

                var n = nums.Count - 1;
                for (int i = 0; i <= Math.Pow(2, n) - 1; i++)
                {
                    var ops = Convert.ToString(i, 2).PadLeft(n, '0')
                        .Select(c => Enum.Parse<Op>([c]))
                        .ToList();

                    long val = nums[0];
                    nums.Skip(1).Select((x, i) =>
                    {
                        if (ops[i] == Op.Add)
                        {
                            val += x;
                        }
                        else
                        {
                            val *= x;
                        }
                        return val;
                    }).ToList();
                    if (val == target)
                    {
                        good++;
                        result += target;
                        break;
                    }
                }
            }
            return result;
        }

        public long B(List<(long target, List<int> nums)> inputs)
        {
            var good = 0;
            long result = 0L;
            foreach (var input in inputs)
            {
                var (target, nums) = input;

                var n = nums.Count - 1;
                for (int i = 0; i <= Math.Pow(3, n) - 1; i++)
                {
                    var ops = ConvertToTernary(i).PadLeft(n, '0')
                        .Select(c => Enum.Parse<Op>([c]))
                        .ToList();

                    long val = nums[0];
                    nums.Skip(1).Select((x, i) =>
                    {
                        val = ops[i] switch
                        {
                            Op.Add => val + x,
                            Op.Multiply => val * x,
                            Op.Concat => long.Parse(val.ToString() + x.ToString()),
                            _ => throw new NotImplementedException(),
                        };
                        return val;
                    }).ToList();
                    if (val == target)
                    {
                        good++;
                        result += target;
                        break;
                    }
                }
            }
            return result;
        }

        static string ConvertToTernary(int number)
        {
            // If the number is zero, just return "0".
            if (number == 0)
            {
                return "0";
            }

            // Keep track of whether the number is negative.
            bool isNegative = number < 0;
            number = Math.Abs(number);

            StringBuilder sb = new StringBuilder();

            // Repeatedly divide by 3 and record the remainders.
            while (number > 0)
            {
                int remainder = number % 3;
                sb.Insert(0, remainder.ToString());
                number /= 3;
            }

            // Reapply the negative sign if needed.
            if (isNegative)
            {
                sb.Insert(0, "-");
            }

            return sb.ToString();
        }

        public List<(long target, List<int> nums)> SetupInputs(string[] inputs)
        {
            return  inputs.Select(l =>
            {
                var p = l.Split(':');
                var target = long.Parse(p[0]);
                var nums = p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                return (target, nums);
            })
            .ToList();
        }


        enum Op
        {
            Add,
            Multiply,
            Concat,
        }
    }
}
