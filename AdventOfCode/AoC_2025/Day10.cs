using System;
using System.Net.NetworkInformation;
using AdventOfCodeCore;
using Microsoft.Z3;

namespace AoC_2025
{
    public class Day10 : IDay<Machine>
    {
        public long A(List<Machine> inputs)
        {
            return inputs.Sum(SolveMachineA);
        }

        int SolveMachineA(Machine machine)
        {
            Queue<SolutionStepA> solveQueue = [];
            solveQueue.Enqueue(
                new SolutionStepA
                {
                    Lights = new bool[machine.Goal.Length],
                    ButtonPresses = 0,
                });

            while (solveQueue.TryDequeue(out var step))
            {

                foreach (var button in machine.Buttons)
                {
                    var nextStep = step with
                    {
                        ButtonPresses = step.ButtonPresses + 1,
                        Pressed = step.Pressed.ToList(),
                        Lights = step.Lights.ToArray(),
                    };
                    foreach (var b in button.Lights)
                    {
                        nextStep.Lights[b] = !nextStep.Lights[b];
                    }
                    nextStep.Pressed.Add(button.Lights);

                    if (nextStep.Lights.SequenceEqual(machine.Goal))
                    {
                        return nextStep.ButtonPresses;
                    }

                    solveQueue.Enqueue(nextStep);
                }
            }

            return 0;
        }

        public long B(List<Machine> inputs)
        {
            return inputs.Sum(SolveMachineB);
        }

        int SolveMachineB(Machine machine)
        {
            var ctx = new Context();
            var optimize = ctx.MkOptimize();

            var buttonVars = new IntExpr[machine.Buttons.Count];

            for (int b = 0; b < machine.Buttons.Count; b++)
            {
                buttonVars[b] = ctx.MkIntConst($"button_{b}");
                optimize.Add(ctx.MkGe(buttonVars[b], ctx.MkInt(0)));
            }

            for (var j = 0; j < machine.Joltages.Count; j++)
            {
                var terms = new List<ArithExpr>(machine.Joltages.Count);
                for (int b = 0; b < machine.Buttons.Count; b++)
                {
                    if (machine.Buttons[b].Lights.Contains(j))
                    {
                        terms.Add(buttonVars[b]);
                    }
                }

                var sumExpr = ctx.MkAdd(terms.ToArray());
                var tarExpr = ctx.MkInt(machine.Joltages[j]);
                optimize.Add(ctx.MkEq(sumExpr, tarExpr));
            }

            optimize.MkMinimize(ctx.MkAdd(buttonVars.Cast<ArithExpr>().ToArray()));

            var status = optimize.Check();
            if (status != Status.SATISFIABLE)
            {
                throw new Exception("Can't solve");
            }

            return Enumerable
                .Range(0, machine.Buttons.Count)
                .Select(i => ((IntNum)optimize.Model.Evaluate(buttonVars[i])).Int)
                .Sum();
        }

        public List<Machine> SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var p = l.Split(' ');

                return new Machine
                {
                    Goal = p[0][1..^1].Select(c => c == '#').ToArray(),
                    Joltages = p[^1][1..^1].Split(',').Select(int.Parse).ToList(),
                    Buttons = p[1..^1].Select(b => new Button
                    {
                        Lights = b[1..^1].Split(',').Select(int.Parse).ToList(),
                    }).ToList(),
                };
            }).ToList();
        }
    }

    public record SolutionStepA
    {
        public int ButtonPresses { get; set; }
        public bool[] Lights { get; set; } = [];
        public List<List<int>> Pressed { get; set; } = [];
    }

    public record SolutionStepB
    {
        public int ButtonPresses { get; set; }
        public int[] Joltages { get; set; } = [];
        public List<List<int>> Pressed { get; set; } = [];
    }

    public record Machine
    {
        public bool[] Goal { get; set; } = [];
        public List<Button> Buttons { get; set; } = [];
        public List<int> Joltages { get; set; } = [];
    }

    public record Button
    {
        public List<int> Lights { get; set; } = [];
    }
}
