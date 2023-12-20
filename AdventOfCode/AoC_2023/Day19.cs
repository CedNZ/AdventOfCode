using System.Text.Json;
using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day19 : IDay<(List<MachinePart> machineParts, Dictionary<string, Workflow> workflows)>
    {
        public long A(List<(List<MachinePart> machineParts, Dictionary<string, Workflow> workflows)> inputs)
        {
            var (machineParts, workflows) = inputs[0];
            return machineParts.Where(mp =>
            {
                var currentWorkflow = workflows["in"];
                while (currentWorkflow != null)
                {
                    var nextWorkflow = currentWorkflow.Steps.Select(s => s(mp)).FirstOrDefault(s => s != null);
                    if (nextWorkflow == null)
                    {
                        break;
                    }
                    currentWorkflow = workflows[nextWorkflow];
                }
                return mp.Accepted.GetValueOrDefault();
            }).Sum(mp => mp.X + mp.M + mp.A + mp.S);
        }

        public long B(List<(List<MachinePart> machineParts, Dictionary<string, Workflow> workflows)> inputs)
        {
            throw new NotImplementedException();
        }

        public List<(List<MachinePart> machineParts, Dictionary<string, Workflow> workflows)> SetupInputs(string[] inputs)
        {
            var workflowsInput = inputs.TakeUntil(string.IsNullOrWhiteSpace).ToList();
            var parts = inputs.SkipUntil(string.IsNullOrWhiteSpace).Skip(1).ToList();

            var machineParts = parts.ConvertAll(p =>
            {
                var m = Regex.Matches(p, "\\d+");
                return new MachinePart
                {
                    X = long.Parse(m[0].Value),
                    M = long.Parse(m[1].Value),
                    A = long.Parse(m[2].Value),
                    S = long.Parse(m[3].Value),
                };
            });
            var workflows = workflowsInput.Select(w =>
            {
                var name = new string(w.TakeUntil(c => c == '{').ToArray());
                var steps = new string(w.SkipUntil(c => c == '{').Skip(1).TakeUntil(c => c == '}').ToArray()).Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                List<Func<MachinePart, string?>> workflows = [];
                foreach (var step in steps)
                {
                    if (step.Contains(':') && step is [var p, var op, .. var arg])
                    {
                        var args = arg.Split(':', StringSplitOptions.RemoveEmptyEntries);
                        var val = int.Parse(args[0]);
                        var target = args[1];

                        var getProp = (MachinePart mp) => p switch
                        {
                            'x' => mp.X,
                            'm' => mp.M,
                            'a' => mp.A,
                            's' => mp.S,
                            _ => throw new NotImplementedException(p.ToString()),
                        };

                        var test = (long p) => op switch
                        {
                            '<' => p < val,
                            '>' => p > val,
                            _ => throw new NotImplementedException(op.ToString()),
                        };

                        workflows.Add((mp) => test(getProp(mp)) ? target : null);
                    }
                    else
                    {
                        workflows.Add(mp => step);
                    }
                }

                return new Workflow
                {
                    Name = name,
                    Steps = workflows,
                };
            }).ToDictionary(x => x.Name, x => x);

            workflows.Add("A", new Workflow
            {
                Name = "A",
                Steps = new List<Func<MachinePart, string?>> { mp =>
                {
                    mp.Accepted = true;
                    return null;
                }},
            });
            workflows.Add("R", new Workflow
            {
                Name = "R",
                Steps = new List<Func<MachinePart, string?>> { mp =>
                {
                    mp.Accepted = false;
                    return null;
                }},
            });

            return [(machineParts, workflows)!];
        }
    }

    public record MachinePart
    {
        public long X { get; init; }
        public long M { get; init; }
        public long A { get; init; }
        public long S { get; init; }

        public bool? Accepted { get; set; }
    }

    public record Workflow
    {
        public string Name { get; init; } = "";
        public List<Func<MachinePart, string?>> Steps { get; init; } = [];
    }
}
