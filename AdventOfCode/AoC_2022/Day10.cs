using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day10 : IDay<PCInstruction>
    {
        public long A(List<PCInstruction> inputs)
        {
            foreach (PCInstruction instruction in inputs)
            {
                RunInstruction(instruction);
            }
            return TotalSignal;
        }

        public void RunInstruction(PCInstruction instruction)
        {
            int cycles = instruction.Instruction switch
            {
                "noop" => 1,
                "addx" => 2,
                _ => 1
            };
            
            for (int i = 0; i < cycles; i++)
            {
                Clock++;
                if ((Clock - 20) % 40 == 0)
                {
                    TotalSignal += SignalStrength;
                }
            }
            X += instruction.Argument.GetValueOrDefault();
        }

        public int TotalSignal = 0;
        public int SignalStrength => X * Clock;

        public static int Clock { get; set; } = 0;
        public static int X { get; set; } = 1;

        public long B(List<PCInstruction> inputs)
        {
            throw new NotImplementedException();
        }

        public List<PCInstruction> SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var parts = l.Split(' ');
                return new PCInstruction
                {
                    Instruction = parts[0],
                    Argument = parts.Length > 1 ? int.TryParse(parts[1], out var val) ? val : null : null,
                };
            }).ToList();
        }
    }

    public struct PCInstruction
    {
        public string Instruction { get; set; }
        public int? Argument { get; set; }
    }
}
