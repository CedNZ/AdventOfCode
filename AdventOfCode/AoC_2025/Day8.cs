using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day8 : IDay<JunctionBox>
    {
        public long A(List<JunctionBox> inputs)
        {
            var pairs = inputs.Pairs()
                    .Select(p => new
                    {
                        Box1 = p.Item1,
                        Box2 = p.Item2,
                        Distance = p.EuclideanDistance([b => b.X, b => b.Y, b => b.Z])
                    })
                    .OrderBy(x => x.Distance)
                    .Take(1000); //10 for test, 1_000 for real

            List<HashSet<JunctionBox>> circuits = [];
            foreach (var pair in pairs)
            {
                var inCircuits = circuits.Where(x => x.Any(b => b == pair.Box1 || b == pair.Box2)).ToList();
                //remove
                circuits = circuits.Except(inCircuits).ToList();

                var circuit = new HashSet<JunctionBox> { pair.Box1, pair.Box2 };
                foreach (var inCircuit in inCircuits)
                {
                    circuit.UnionWith(inCircuit);
                }

                //readd to circuits
                circuits.Add(circuit);
            }

            return circuits.OrderByDescending(x => x.Count).Take(3).Aggregate(1, (agg, next) => agg * next.Count);
        }

        public long B(List<JunctionBox> inputs)
        {
            var pairs = inputs.Pairs()
                .Select(p => new
                {
                    Box1 = p.Item1,
                    Box2 = p.Item2,
                    Distance = p.EuclideanDistance([b => b.X, b => b.Y, b => b.Z])
                })
                .OrderBy(x => x.Distance);

            List<HashSet<JunctionBox>> circuits = [];

            foreach (var pair in pairs)
            {
                var inCircuits = circuits.Where(x => x.Any(b => b == pair.Box1 || b == pair.Box2)).ToList();
                //remove
                circuits = circuits.Except(inCircuits).ToList();

                var circuit = new HashSet<JunctionBox> { pair.Box1, pair.Box2 };
                foreach (var inCircuit in inCircuits)
                {
                    circuit.UnionWith(inCircuit);
                }

                //readd to circuits
                circuits.Add(circuit);

                if (circuits.Count == 1 && circuit.Count == inputs.Count)
                {
                    return pair.Box1.X * pair.Box2.X;
                }
            }

            return -1;
        }

        public List<JunctionBox> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => new JunctionBox(x)).ToList();
        }
    }

    public class JunctionBox
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public JunctionBox(string coords)
        {
            var nums = coords.Split(',').Select(long.Parse).ToList();
            X = nums[0];
            Y = nums[1];
            Z = nums[2];
        }
    }
}
