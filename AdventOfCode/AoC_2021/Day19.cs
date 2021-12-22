using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day19 : IDay<string>
    {
        List<Scanner> _scanners;

        public long A(List<string> inputs)
        {
            _scanners = new List<Scanner>();
            Scanner scanner = null;
            foreach (var line in inputs)
            {
                if (line.StartsWith("---")) //Scanner
                {
                    scanner = new Scanner
                    {
                        Id = _scanners.Count
                    };
                    _scanners.Add(scanner);
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    scanner.AddBeacons(new Beacon(line, scanner.Beacons.Count));
                }
            }

            foreach (var s in _scanners)
            {
                s.BuildPairs();
            }

            var mainScanner = _scanners[0];

            mainScanner.X = 0;
            mainScanner.Y = 0;
            mainScanner.Z = 0;

            var mappedScanners = new List<Scanner> { mainScanner };
            var mappedBeacons = mainScanner.Beacons.ToList();
            _scanners.Remove(mainScanner);

            while (_scanners.Any())
            {
                var mappedPair = new[] { _scanners, mappedScanners }
                    .CartesianProduct()
                    .First(s => new[] { s.ToArray()[0].BeaconPairs, s.ToArray()[1].BeaconPairs }
                                                .CartesianProduct()
                                                    .Select(x => x.ToArray())
                                                    .Count(x => Transform(x, (1,1,1)) != null) >= 13).ToArray();

                var matchedScanner = mappedScanners.Contains(mappedPair[0]) ? mappedPair[1] : mappedPair[0];
                var mappedScanner = mappedScanners.Contains(mappedPair[0]) ? mappedPair[0] : mappedPair[1];

                _scanners.Remove(matchedScanner);

                var matchingBeacons = new[] { mappedScanner.BeaconPairs, matchedScanner.BeaconPairs }
                                            .CartesianProduct()
                                            .Where(x => Transform(x.ToArray(), (1,1,1)) != null)
                                            .ToList();

                var l0 = matchingBeacons.First().ToArray();

                var offset1 = CloneOffset(l0[0].Offsets);
                var offset2 = CloneOffset(l0[1].Offsets);

                foreach (var beacon in matchedScanner.Beacons)
                {
                    beacon.SetPosition(Transform(offset2, offset1, beacon.Position));
                }

                var translation = new BeaconPairs(new[] { l0[0].BeaconA, l0[1].BeaconA });

                matchedScanner.SetOffset(translation.Offsets);

                 //matchedScanner.SetOffset((beaconOffset.Value.x + b0.Position.X, beaconOffset.Value.y + b0.Position.Y, beaconOffset.Value.z + b0.Position.Z));

                mappedScanners.Add(matchedScanner);
                mappedBeacons.AddRange(matchedScanner.Beacons);

                mappedBeacons = mappedBeacons.GroupBy(x => x.Position).Select(g => g.First()).ToList();
            }

            return mappedBeacons.Count();
        }

        public (int x, int y, int z) CloneOffset((int, int, int) offset)
        {
            return new (offset.Item1, offset.Item2, offset.Item3);
        }

        public bool OffsetsEqual((int, int, int) o1, (int, int, int) o2)
        {
            var x1 = o1.Item1;
            var x2 = o2.Item1;
            var y1 = o1.Item2;
            var y2 = o2.Item2;
            var z1 = o1.Item3;
            var z2 = o2.Item3;

            if (o1 == o2)
            {
                return true;
            }
            if ((x1 == x2 && y1 == y2 && z1 == z2)
                || (x1 == x2 && y1 == z2 && z1 == y2)
                || (x1 == y2 && y1 == x2 && z1 == z2)
                || (x1 == y2 && y1 == z2 && z1 == x2)
                || (x1 == z2 && y1 == x2 && z1 == y2)
                || (x1 == z2 && y1 == y2 && z1 == x2))
            {
                return true;
            }
            return false;
        }

        public (int x, int y, int z)? Transform(BeaconPairs[] pairs, (int x, int y, int z) b)
        {
            if (pairs[0].Length == pairs[1].Length)
            {
                return Transform(pairs[0].Offsets, pairs[1].Offsets, b);
            }
            return null;
        }

        public (int x, int y, int z)? Transform((int x, int y, int z) l0, (int x, int y, int z) l1, (int x, int y, int z) b)
        {
            int? X = null, Y = null, Z = null;
            if (l0.x == l1.x)
            {
                X = b.x;
                if (l0.y == l1.y)
                {
                    Y = b.y;
                    if (l0.z == l1.z)
                    {
                        Z = b.z;
                    }
                }
                else if (l0.y == -l1.y)
                {
                    Y = -b.y;
                    if (l0.z == -l1.z)
                    {
                        Z = -b.z;
                    }
                }
                else if (l0.y == l1.z)
                {
                    Z = b.y;
                    if (l0.z == -l1.y)
                    {
                        Y = -b.z;
                    }
                }
                else if (l0.y == -l1.z)
                {
                    Z = -b.y;
                    if (l0.z == l1.y)
                    {
                        Y = b.z;
                    }
                }
            }
            else if (l0.x == -l1.x)
            {
                X = -b.x;
                if (l0.y == l1.y)
                {
                    Y = b.y;
                    if (l0.z == -l1.z)
                    {
                        Z = -b.z;
                    }
                }
                else if (l0.y == -l1.y)
                {
                    Y = -b.y;
                    if (l0.z == l1.z)
                    {
                        Z = b.z;
                    }
                }
                else if (l0.y == l1.z)
                {
                    Z = b.y;
                    if (l0.z == l1.y)
                    {
                        Y = b.z;
                    }
                }
                else if (l0.y == -l1.z)
                {
                    Z = -b.y;
                    if (l0.z == -l1.y)
                    {
                        Y = -b.z;
                    }
                }
            }
            else if (l0.x == l1.y)
            {
                Y = b.x;
                if (l0.y == l1.x)
                {
                    X = b.y;
                    if (l0.z == -l1.z)
                    {
                        Z = -b.z;
                    }
                }
                else if (l0.y == -l1.x)
                {
                    X = -b.y;
                    if (l0.z == l1.z)
                    {
                        Z = b.z;
                    }
                }
                else if (l0.y == l1.z)
                {
                    Z = b.y;
                    if (l0.z == l1.x)
                    {
                        X = b.z;
                    }
                }
                else if (l0.y == -l1.z)
                {
                    Z = -b.y;
                    if (l0.z == -l1.x)
                    {
                        X = -b.z;
                    }
                }
            }
            else if (l0.x == -l1.y)
            {
                Y = -b.x;
                if (l0.y == l1.x)
                {
                    X = b.y;
                    if (l0.z == l1.z)
                    {
                        Z = b.z;
                    }
                }
                else if (l0.y == -l1.x)
                {
                    X = -b.y;
                    if (l0.z == -l1.z)
                    {
                        Z = -b.z;
                    }
                }
                else if (l0.y == l1.z)
                {
                    Z = b.y;
                    if (l0.z == -l1.x)
                    {
                        X = -b.z;
                    }
                }
                else if (l0.y == -l1.z)
                {
                    Z = -b.y;
                    if (l0.z == l1.x)
                    {
                        X = b.z;
                    }
                }
            }
            else if (l0.x == l1.z)
            {
                Z = b.x;
                if (l0.y == l1.y)
                {
                    Y = b.y;
                    if (l0.z == -l1.x)
                    {
                        X = -b.z;
                    }
                }
                else if (l0.y == -l1.y)
                {
                    Y = -b.y;
                    if (l0.z == l1.x)
                    {
                        X = b.z;
                    }
                }
                else if (l0.y == l1.x)
                {
                    X = b.y;
                    if (l0.z == l1.y)
                    {
                        Y = b.z;
                    }
                }
                else if (l0.y == -l1.x)
                {
                    X = -b.y;
                    if (l0.z == -l1.y)
                    {
                        Y = -b.z;
                    }
                }
            }
            else if (l0.x == -l1.z)
            {
                Z = -b.x;
                if (l0.y == l1.y)
                {
                    Y = b.y;
                    if (l0.z == l1.x)
                    {
                        X = b.z;
                    }
                }
                else if (l0.y == -l1.y)
                {
                    Y = -b.y;
                    if (l0.z == -l1.x)
                    {
                        X = -b.z;
                    }
                }
                else if (l0.y == l1.x)
                {
                    X = b.y;
                    if (l0.z == -l1.y)
                    {
                        Y = -b.z;
                    }
                }
                else if (l0.y == -l1.x)
                {
                    X = -b.y;
                    if (l0.z == l1.y)
                    {
                        Y = b.z;
                    }
                }
            }


            if (X.HasValue && Y.HasValue && Z.HasValue)
            {
                return (X.Value, Y.Value, Z.Value);
            }
            return null;
        }

        public long B(List<string> inputs)
        {

            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }

    public class Scanner
    {
        public List<Beacon> Beacons { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Id { get; set; }

        public Scanner()
        {
            Beacons = new List<Beacon>();
            BeaconPairs = new();
        }

        public void SetOffset((int x, int y, int z) offset)
        {
            X = offset.x;
            Y = offset.y;
            Z = offset.z;

            foreach (var beacon in Beacons)
            {
                beacon.ApplyOffset(offset);
            }
        }

        public void AddBeacons(Beacon beacon)
        {
            Beacons.Add(beacon);
            beacon.Scanner = this;
        }

        public List<BeaconPairs> BeaconPairs { get; set; }

        public List<BeaconPairs> BuildPairs()
        {
            BeaconPairs = Beacons.Pairs().Select(x => new BeaconPairs(x)).ToList();

            return BeaconPairs;
        }

        public override string ToString()
        {
            return $"S{Id}: {X},{Y},{Z}";
        }
    }

    public class BeaconPairs
    {
        public Beacon BeaconA { get; set; }
        public Beacon BeaconB { get; set; }

        public BeaconPairs(IEnumerable<Beacon> beacons)
        {
            var b = beacons.ToList();
            BeaconA = b[0];
            BeaconB = b[1];
        }

        public BeaconPairs(Beacon a, Beacon b)
        {
            BeaconA = a;
            BeaconB = b;
        }

        public (int, int, int) Offsets => (BeaconA.X - BeaconB.X, BeaconA.Y - BeaconB.Y, BeaconA.Z - BeaconB.Z);

        public double Length => Math.Sqrt((Math.Pow(BeaconA.X - BeaconB.X, 2) + Math.Pow(BeaconA.Y - BeaconB.Y, 2) + Math.Pow(BeaconA.Z - BeaconB.Z, 2)));

        public override string ToString()
        {
            return $"{BeaconA} - {BeaconB}\t{Offsets}";
        }
    }

    public class Beacon
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Id { get; set; }

        public (int X, int Y, int Z) Position => (X, Y, Z);

        public Scanner Scanner { get; set; }

        public Beacon(string line, int id)
        {
            var nums = line.Split(',').Select(int.Parse).ToList();
            X = nums[0];
            Y = nums[1];
            Z = nums[2];
            Id = id;
        }

        public Beacon((int x, int y, int z)? b)
        {
            X = b?.x ?? 1;
            Y = b?.y ?? 1;
            Z = b?.z ?? 1;
            Scanner = new Scanner();
        }

        public void SetPosition((int x, int y, int z)? pos)
        {
            if (pos.HasValue)
            {
                X = pos.Value.x;
                Y = pos.Value.y;
                Z = pos.Value.z;
            }
        }

        public void ApplyOffset((int x, int y, int z) offset)
        {
            X += offset.x;
            Y += offset.y;
            Z += offset.z;
        }

        public override string ToString()
        {
            return $"S:{Scanner.Id}-B{Id}: {X},{Y},{Z}";
        }
    }
}
