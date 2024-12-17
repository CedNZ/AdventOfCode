using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day17 : IDayOut<string, string>
    {
        public string A(List<string> inputs)
        {
            int A, B, C;
            A = int.Parse(Regex.Match(inputs[0], "\\d+").Value);
            B = int.Parse(Regex.Match(inputs[1], "\\d+").Value);
            C = int.Parse(Regex.Match(inputs[2], "\\d+").Value);

            int pc = 0;
            List<int> program = inputs[4].Split(':').Last().Split(',').Select(int.Parse).ToList();
            List<int> output = [];

            while (pc < program.Count)
            {
                var op = (Op)program[pc];

                if (op == Op.ADV)
                {
                    var numerator = A;
                    var denominator = Math.Pow(2, Combo(pc + 1));
                    A = (int)(numerator / denominator);
                }
                else if (op == Op.BXL)
                {
                    B ^= Literal(pc + 1);
                }
                else if (op == Op.BST)
                {
                    B = Combo(pc + 1) % 8;
                }
                else if (op == Op.JNZ)
                {
                    if (A != 0)
                    {
                        pc = Literal(pc + 1);
                        continue;
                    }
                }
                else if (op == Op.BXC)
                {
                    B ^= C;
                }
                else if (op == Op.OUT)
                {
                    var x = Combo(pc + 1);
                    output.Add(x % 8);
                }
                else if (op == Op.BDV)
                {
                    var numerator = A;
                    var denominator = Math.Pow(2, Combo(pc + 1));
                    B = (int)(numerator / denominator);
                }
                else if (op == Op.CDV)
                {
                    var numerator = A;
                    var denominator = Math.Pow(2, Combo(pc + 1));
                    C = (int)(numerator / denominator);
                }

                pc += 2;
            }

            return string.Join(',', output.ToList());

            int Combo(int c)
            {
                return program[c] switch
                {
                    0 => 0,
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => A,
                    5 => B,
                    6 => C,
                    _ => throw new Exception("Invalid parameter mode")
                };
            }
            int Literal(int c) => program[c];
        }

        enum Op
        {
            ADV,
            BXL,
            BST,
            JNZ,
            BXC,
            OUT,
            BDV,
            CDV,
        }


        public string B(List<string> inputs)
        {
            throw new NotImplementedException();
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return [.. inputs];
        }
    }
}
