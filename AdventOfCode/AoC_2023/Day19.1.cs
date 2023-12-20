using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day19_1 : IDay<Dictionary<string, List<Steps>>>
    {
        long IDayOut<Dictionary<string, List<Steps>>, long>.A(List<Dictionary<string, List<Steps>>> inputs)
        {
            return 0;
        }

        long IDayOut<Dictionary<string, List<Steps>>, long>.B(List<Dictionary<string, List<Steps>>> inputs)
        {
            var stepDict = inputs[0];

            Queue<(string name, Ranges ranges)> searchQueue = [];
            searchQueue.Enqueue(("in", new Ranges()));
            long sum = 0;
            while (searchQueue.TryDequeue(out var r))
            {
                var (name, ranges) = r;

                ranges.Path = ranges.Path.ToList();
                ranges.Path.Add(name);

                //ranges.Dump(name);

                if (name == "A")
                {
                    var s = (ranges.XMax - ranges.XMin)
                            * (ranges.MMax - ranges.MMin)
                            * (ranges.AMax - ranges.AMin)
                            * (ranges.SMax - ranges.SMin);
                    //ranges.Dump(s.ToString());
                    sum += s;
                }
                else if (name == "R")
                {

                }
                else
                {
                    var workflow = stepDict[name];
                    foreach (var step in workflow)
                    {
                        if (step.Prop is null)
                        {
                            searchQueue.Enqueue((step.Target, ranges));
                        }
                        else
                        {
                            var (min, max) = step.Prop switch
                            {
                                'x' => (ranges.XMin, ranges.XMax),
                                'm' => (ranges.MMin, ranges.MMax),
                                'a' => (ranges.AMin, ranges.AMax),
                                's' => (ranges.SMin, ranges.SMax),
                                _ => throw new NotImplementedException(),
                            };

                            var (ltd, gtd) = step.Op switch
                            {
                                '>' => (0, 0),
                                '<' => (-1, -1),
                                _ => throw new NotImplementedException(),
                            };

                            var ltMin = Math.Min(min, step.Value.GetValueOrDefault() + -ltd);
                            var ltMax = Math.Min(max, step.Value.GetValueOrDefault() + ltd);

                            var gtMin = Math.Max(min, step.Value.GetValueOrDefault() + gtd);
                            var gtMax = Math.Max(max, step.Value.GetValueOrDefault() + gtd);

                            var ltRanges = step.Prop switch
                            {
                                'x' => ranges with
                                {
                                    XMin = ltMin,
                                    XMax = ltMax,
                                },
                                'm' => ranges with
                                {
                                    MMin = ltMin,
                                    MMax = ltMax,
                                },
                                'a' => ranges with
                                {
                                    AMin = ltMin,
                                    AMax = ltMax,
                                },
                                's' => ranges with
                                {
                                    SMin = ltMin,
                                    SMax = ltMax,
                                },
                                _ => throw new NotImplementedException(),
                            };

                            var gtRanges = step.Prop switch
                            {
                                'x' => ranges with
                                {
                                    XMin = gtMin,
                                    XMax = gtMax,
                                },
                                'm' => ranges with
                                {
                                    MMin = gtMin,
                                    MMax = gtMax,
                                },
                                'a' => ranges with
                                {
                                    AMin = gtMin,
                                    AMax = gtMax,
                                },
                                's' => ranges with
                                {
                                    SMin = gtMin,
                                    SMax = gtMax,
                                },
                                _ => throw new NotImplementedException(),
                            };


                            if (step.Op == '<')
                            {
                                searchQueue.Enqueue((step.Target, ltRanges));
                                ranges = gtRanges;
                            }
                            else
                            {
                                searchQueue.Enqueue((step.Target, gtRanges));
                                ranges = ltRanges;
                            }
                        }
                    }
                }
            }

            return sum;
        }

        List<Dictionary<string, List<Steps>>> IDayOut<Dictionary<string, List<Steps>>, long>.SetupInputs(string[] inputs)
        {
            Dictionary<string, List<Steps>> stepDict = [];

            foreach (var w in inputs.TakeUntil(c => string.IsNullOrEmpty(c)))
            {
                List<Steps> stepList = [];
                var name = new string(w.TakeUntil(c => c == '{').ToArray());
                var steps = new string(w.SkipUntil(c => c == '{').Skip(1).TakeUntil(c => c == '}').ToArray()).Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var step in steps)
                {
                    if (step.Contains(':') && step is [var p, var op, .. var arg])
                    {
                        var args = arg.Split(':', StringSplitOptions.RemoveEmptyEntries);
                        var val = int.Parse(args[0]);
                        var target = args[1];

                        stepList.Add(new Steps
                        {
                            Prop = p,
                            Op = op,
                            Value = val,
                            Target = target,
                        });
                    }
                    else
                    {
                        stepList.Add(new Steps
                        {
                            Target = step,
                        });
                    }
                }

                stepDict.Add(name, stepList);
            }

            return [stepDict];
        }
    }

    record Steps
    {
        public char? Prop { get; init; }
        public char? Op { get; init; }
        public int? Value { get; init; }
        public string Target { get; init; }
    }

    record Ranges
    {
        public long XMin { get; init; } = 0;
        public long XMax { get; init; } = 4_000;
        public long MMin { get; init; } = 0;
        public long MMax { get; init; } = 4_000;
        public long AMin { get; init; } = 0;
        public long AMax { get; init; } = 4_000;
        public long SMin { get; init; } = 0;
        public long SMax { get; init; } = 4_000;
        public List<string> Path { get; set; } = [];

        public string Score => ((XMax - XMin)
                        * (MMax - MMin)
                        * (AMax - AMin)
                        * (SMax - SMin)).ToString("N0");
    }
}
