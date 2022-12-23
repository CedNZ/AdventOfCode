using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day5 : IDayOut<string, string>
    {
        public string? A(List<string> inputs)
        {
            foreach (var instruction in inputs)
            {
                var nums = instruction.Split(' ').Select(x => int.TryParse(x, out var i) ? i : -1).Where(x => x > 0).ToList();
                var fromStack = StacksA.Find(s => s.Number == nums[1]);
                var toStack = StacksA.Find(s => s.Number == nums[2]);

                for (int i = 0; i < nums[0]; i++)
                {
                    if (fromStack.Crates.TryPop(out var c) is false)
                    {
                        break;
                    }
                    toStack.Crates.Push(c);
                }
            }/**/
            return StacksA.Aggregate("", (agg, next) => agg += next.Crates.Peek());
        }

        public string? B(List<string> inputs)
        {
            foreach (var instruction in inputs)
            {
                var nums = instruction.Split(' ').Select(x => int.TryParse(x, out var i) ? i : -1).Where(x => x > 0).ToList();
                var fromStack = StacksB.Find(s => s.Number == nums[1]);
                var toStack = StacksB.Find(s => s.Number == nums[2]);

                var tempStack = new Stack<Crate>();
                for (int i = 0; i < nums[0]; i++)
                {
                    if (fromStack.Crates.TryPop(out var c) is false)
                    {
                        break;
                    }
                    tempStack.Push(c);
                }
                while (tempStack.Any())
                {
                    toStack.Crates.Push(tempStack.Pop());
                }
            }
            return StacksB.Aggregate("", (agg, next) => agg += next.Crates.Peek());
        }

        public List<Stack> StacksA { get; set; } = new();
        public List<Stack> StacksB { get; set; } = new();

        public List<string> SetupInputs(string[] inputs)
        {
            var crates = inputs.TakeWhile(l => l.StartsWith("move") is false).Where(l => l != "").ToList();
            List<string> instructions = inputs.Skip(crates.Count() + 1).ToList();

            List<Stack> stacksa = new();
            List<Stack> stacksb = new();
            for (int i = 0; i < crates.Max(l => l.Length); i++)
            {
                if (int.TryParse(crates[^1][i].ToString(), out var stacknum))
                {
                    var stacka = new Stack(stacknum);
                    var stackb = new Stack(stacknum);
                    stacksa.Add(stacka);
                    stacksb.Add(stackb);

                    for (int j = crates.Count - 2; j >= 0; j--)
                    {
                        if (char.IsWhiteSpace(crates[j][i]) is false)
                        {
                            stacka.Crates.Push(new Crate { Name = crates[j][i].ToString() });
                            stackb.Crates.Push(new Crate { Name = crates[j][i].ToString() });
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            StacksA = stacksa.ToList();
            StacksB = stacksb.ToList();

            return instructions;
        }
    }

    public struct Crate
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }

    public struct Stack
    {
        public Stack<Crate> Crates { get; set; } = new();
        public int Number { get; set; }

        public Stack(int num)
        {
            Number = num;
        }

        public override string ToString() => $"{Number} - {Crates.Count()}";
    }
}
