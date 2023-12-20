using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day20 : IDay<Module>
    {
        public long A(List<Module> inputs)
        {
            long highPulse = 0;
            long lowPulse = 0;
            for (int i = 0; i < 1000; i++)
            {
                PushButton(inputs, ref highPulse, ref lowPulse);
            }


            return highPulse * lowPulse;
        }

        private static void PushButton(List<Module> inputs, ref long highPulse, ref long lowPulse)
        {
            Queue<(string to, bool pulse, string from)> pulses = [];
            pulses.Enqueue(("roadcaster", false, "button"));
            while (pulses.TryDequeue(out var p))
            {
                var (to, pulse, from) = p;
                _ = pulse ? highPulse++ : lowPulse++;
                var module = inputs.First(m => m.Name == to);
                var result = module.HandlePulse(pulse, from);
                if (result is null)
                {
                    continue;
                }
                foreach (var output in module.Outputs)
                {
                    pulses.Enqueue((output, result.Value, to));
                }
            }
        }

        public long B(List<Module> inputs)
        {
            Queue<(string to, bool pulse, string from)> pulses = [];

            var lx = inputs.First(x => x.Name == "lx");
            var lxInputs = lx.Inputs.ToDictionary(x => x, _ => 0L);

            long count = 0;
            while (true)
            {
                count++;
                pulses.Enqueue(("roadcaster", false, "button"));
                while (pulses.TryDequeue(out var p))
                {
                    var (to, pulse, from) = p;

                    if (!pulse && to == "rx")
                    {
                        return count;
                    }

                    if (pulse && to == "lx")
                    {
                        lxInputs[from] = count;
                        if (lxInputs.All(x => x.Value > 0))
                        {
                            break;
                        }
                    }

                    var module = inputs.First(m => m.Name == to);
                    var result = module.HandlePulse(pulse, from);
                    if (result is null)
                    {
                        continue;
                    }
                    foreach (var output in module.Outputs)
                    {
                        pulses.Enqueue((output, result.Value, to));
                    }
                }
                if (lxInputs.All(x => x.Value > 0))
                {
                    break;
                }
            }

            return lxInputs.Values.CalculateLCM();
        }

        public List<Module> SetupInputs(string[] inputs)
        {
            List<Module> modules = inputs.Select(l =>
            {
                var name = new string(l.TakeUntil(char.IsWhiteSpace).ToArray());
                var outputs = new string(l.SkipUntil(c => c == '>').Skip(1).ToArray()).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                return new Module(name, outputs);
            }).ToList();

            for (int i = 0; i < modules.Count; i++)
            {
                var module = modules[i];
                foreach (var output in module.Outputs)
                {
                    var m = modules.FirstOrDefault(m => m.Name == output);
                    if (m is null)
                    {
                        m = new Module($"_{output}", []);
                        modules.Add(m);
                    }
                    m.Inputs.Add(module.Name);
                }
            }

            return modules;
        }
    }

    public class Module
    {
        private char _type { get; set; }
        public string Name { get; set; } = "";
        public List<string> Outputs { get; set; } = [];
        public List<string> Inputs { get; set; } = [];

        public Module(string name, List<string> outputs)
        {
            _type = name[0];
            Name = name[1..];
            Outputs = outputs;
        }

        public bool? HandlePulse(bool pulse, string from)
        {
            return _type switch
            {
                '%' => HandleFlipflop(pulse),
                '&' => HandleConjunction(pulse, from),
                _ => pulse,
            };
        }

        private bool _state;
        private bool? HandleFlipflop(bool pulse)
        {
            if (pulse)
            {
                return null;
            }
            else
            {
                _state = !_state;
                return _state;
            }
        }

        private Dictionary<string, bool> _states = null!;
        private bool HandleConjunction(bool pulse, string from)
        {
            if (_states is null)
            {
                _states = Inputs.ToDictionary(i => i, _ => false);
            }

            _states[from] = pulse;

            if (_states.All(kv => kv.Value))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"{_type}{Name}";
        }
    }
}
