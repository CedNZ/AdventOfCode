using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day16 : IDay<Valve>
    {
        public long A(List<Valve> inputs)
        {
            var start = inputs.Find(v => v.Name == "AA");
            AllValves = inputs;

            var leaves = Tunnels.Where(t => t.Value.Count == 1).Select(t => t.Key).ToList();
            var trees = leaves.Select(l => GetTree(l));
            Dictionary<string, Func<int, (int pressure, int time)>> treeFuncs = new();
            foreach (var t in trees)
            {
                var valve = TreePath(AllValves.First(v => v.Name == t[0]), t);

                var valves = new ValveEnumerable(valve).ToList();
                valves.Insert(0, valve);

                Func<int, (int pressure, int time)> treeFunc = (x) => (0, valves.Count());

                treeFunc = valves.Where(v => v.TotalPressue > v.Parent?.TotalPressue)
                    .Aggregate(treeFunc, (f, vNext) =>
                    {
                        return (x) =>
                        {
                            var res = f(x);
                            int timeDelta = 30 - vNext.Time;
                            int time = x - timeDelta;
                            return (res.pressure + (vNext.FlowRate * time), res.time);
                        };
                    });
                treeFuncs.Add(valve.Name, treeFunc);
            }

            List<string> toOpen = inputs.Where(x => x.Open is false).Select(x => x.Name).ToList();
            toOpen = toOpen.Except(trees.SelectMany(t => t)).ToList();
            toOpen.AddRange(trees.Select(t => t[0]));
            PriorityQueue<Valve, int> priorityQueue = new();
            priorityQueue.Enqueue(start, -start.TotalPressue);

            int max = 0;
            Dictionary<int, List<Valve>> timeQueue = new();
            for (int i = 0; i <= 30; i++)
            {
                timeQueue[i] = new List<Valve>();
            }

            timeQueue[start.Time].Add(start);
            for (int time = start.Time; time >= 0; time--)
            {
                var list = timeQueue[time];
                if (list.Count == 0)
                {
                    continue;
                }
                var lmin = list.Min(x => x.TotalPressue);
                var lmax = list.Max(x => x.TotalPressue);
                if (lmin != lmax)
                {
                    list = list.Where(l => l.TotalPressue > Math.Max(max, (lmax / 3 * 2))).ToList();
                }
                while (list.Count > 0)
                {
                    var current = list.FirstOrDefault();
                    list.Remove(current);
                    var valveEnumerable = new ValveEnumerable(current).ToList();
                    valveEnumerable.Insert(0, current);
                    if (current.Time == 0 || toOpen.All(n => valveEnumerable.Where(v => v.Open).Select(v => v.Name).Contains(n)))
                    {
                        max = Math.Max(max, current.TotalPressue);
                        list = list.Where(l => l.TotalPressue > max).ToList();
                        continue;
                    }
                    foreach (var neighbour in Neighbours(current))
                    {
                        if (neighbour.Leaf is false)
                        {
                            if (treeFuncs.TryGetValue(neighbour.Name, out var f))
                            {
                                var r = f(neighbour.Time);
                                var nvalve = neighbour with
                                {
                                    Parent = current,
                                    TotalPressue = current.TotalPressue + r.pressure,
                                    Time = current.Time - r.time,
                                    Open = true,
                                    Leaf = true,
                                };
                                if (nvalve.Time >= 0)
                                {
                                    timeQueue[nvalve.Time].Add(nvalve);
                                }
                            }
                            else
                            {
                                timeQueue[neighbour.Time].Add(neighbour);
                            }
                        }
                    }
                }
            }

            return max;
        }

        private Valve TreePath(Valve start, List<string> tree)
        {
            Stack<Valve> stack = new Stack<Valve>();
            var toOpen = AllValves.Where(v => v.Open is false && tree.Contains(v.Name)).Select(v => v.Name).ToList();
            stack.Push(start);
            Valve? previous = null;
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                ValveEnumerable valves = new ValveEnumerable(current);
                if (current.Name == start.Name && current.Open && toOpen.All(n => valves.Any(v => v.Name == n)))
                {
                    return current;
                }
                foreach (var item in Neighbours(current))
                {
                    if (tree.Contains(item.Name) && item.Name != previous?.Name)
                    {
                        stack.Push(item);
                    }
                }
                previous = current;
            }
            return null;
        }

        private List<string> GetTree(string leaf, string parent = "")
        {
            if (Tunnels[leaf].Count > 2)
            {
                return new();
            }

            var tree = GetTree(Tunnels[leaf].Find(x => x != parent), leaf);

            tree.Add(leaf);

            return tree;
        }

        private int RelievePressure(Valve valve, int openCount)
        {
            if (valve.Time == 0)
            {
                return valve.TotalPressue;
            }
            else if (AllValves.Count == openCount)
            {
                return valve.TotalPressue;
            }

            return Neighbours(valve).Max(v => RelievePressure(v, openCount + (v.Open ? 1 : 0)));
        }

        private IEnumerable<Valve> Neighbours(Valve valve)
        {

            if (valve.Open is false)
            {
                yield return valve with { Open = true, Parent = valve };
            }

            if (valve.Leaf)
            {
                yield return valve.Parent with { Parent = valve };
            }
            else
            {
                var tunnels = Tunnels[valve.Name];
                foreach (var item in tunnels.Select(v => valve.FindPrevious(v) ?? AllValves.Find(x => x.Name == v)))
                {
                    if (item is not null)
                    {
                        yield return item with { Parent = valve };
                    }
                }
            }
        }

        static List<Valve> AllValves = new();
        static Dictionary<string, List<string>> Tunnels = new();

        private IEnumerable<(Valve, int)> Connections(Valve valve, List<Valve> path, int minute, List<Valve> allValves)
        {
            var tunnels = Tunnels[valve.Name];

            if (valve.Open is false)
            {
                yield return (valve with { Open = true }, (minute - 1) * valve.FlowRate);
            }
            foreach (var item in tunnels.Select(v => (path.Find(p => p.Open && p.Name == v) ?? allValves.Find(x => x.Name == v), 0)))
            {
                yield return item;
            }
        }

        public long B(List<Valve> inputs)
        {
            throw new NotImplementedException();
        }


        public List<Valve> SetupInputs(string[] inputs)
        {
            List<Valve> valves = new List<Valve>();
            Tunnels = new();

            Func<string, Valve> getValve = (name) => valves.FirstOrDefault(v => v.Name == name)
                    ?? new Valve
                    {
                        Name = name,
                    };

            foreach (var item in inputs)
            {
                var matches = Regex.Match(item, @"Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.*)");

                var valve = getValve(matches.Groups[1].Value);

                valve.FlowRate = int.Parse(matches.Groups[2].Value);
                if (valve.FlowRate == 0)
                {
                    valve.Open = true;
                }

                Tunnels.Add(valve.Name, matches.Groups[3].Value.Split(',').Select(s => s.Trim()).ToList());

                valves.Add(valve);
            }

            return valves;
        }
    }

    public record Valve
    {
        public string Name { get; set; }
        public int FlowRate { get; set; }

        public bool Open { get; set; } = false;

        private Valve? _parent;
        public Valve? Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                Time = value.Time - 1;
                TotalPressue = value.Name == Name && Open ? Time * FlowRate + value.TotalPressue : value.TotalPressue;
                FindPrevious = (name) => Name == name ? this : _parent?.FindPrevious(name);
            }
        }
        public Func<string, Valve?> FindPrevious = (_) => null;
        public int TotalPressue { get; set; } = 0;
        public int Time { get; set; } = 30;

        public bool Leaf { get; set; } = false;

        public override string ToString() => $"{Name} {(Open ? 'O' : 'C')} {Time} {TotalPressue}";
        //public Valve[] Connected { get; set; }
    }

    public class ValveEnumerable : IEnumerable<Valve>
    {
        private Valve _valve;
        public ValveEnumerable(Valve valve)
        {
            _valve = valve;
        }

        public IEnumerator<Valve> GetEnumerator()
        {
            return new ValveEnumerator(_valve);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ValveEnumerator : IEnumerator<Valve>
    {
        private Valve _initial;
        private Valve _current;

        public ValveEnumerator(Valve valve)
        {
            _initial = valve;
            _current = valve;
        }

        public Valve Current
        {
            get => _current ?? throw new InvalidOperationException();
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _current = null;
        }

        public bool MoveNext()
        {
            _current = _current.Parent;
            if (_current == null)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            _current = _initial;
        }
    }
}
