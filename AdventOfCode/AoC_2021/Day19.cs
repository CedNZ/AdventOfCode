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
                                                    .Count(x => OffsetsEqual(x[0].Offsets, x[1].Offsets)) >= 12).ToArray();

                var matchedScanner = mappedScanners.Contains(mappedPair[0]) ? mappedPair[1] : mappedPair[0];
                var mappedScanner = mappedScanners.Contains(mappedPair[0]) ? mappedPair[0] : mappedPair[1];

                _scanners.Remove(matchedScanner);

                var matchingBeacons = new[] { mappedScanner.BeaconPairs, matchedScanner.BeaconPairs }
                                            .CartesianProduct()
                                            .Where(x => OffsetsEqual(x.ToArray()[0].Offsets, x.ToArray()[1].Offsets))
                                            .ToList();

                var l0 = matchingBeacons.First().ToArray();
                var l1 = matchingBeacons.Skip(1).First(mb => mb.Any(b => b.BeaconA == l0[0].BeaconA)).ToArray();

                Beacon b0 = null;
                Beacon b1 = null;

                if (l0[0].BeaconA == l1[0].BeaconA)
                {
                    b0 = l0[0].BeaconA;
                    b1 = l0[1].BeaconA;
                }

                //need to figure out beacon rotation

                var offset = new BeaconPairs(b0, b1).Offsets;
                matchedScanner.SetOffset(offset);

                mappedScanners.Add(matchedScanner);
                mappedBeacons.AddRange(matchedScanner.Beacons);

                mappedBeacons = mappedBeacons.GroupBy(x => x.Position).Select(g => g.First()).ToList();
            }

            return mappedBeacons.Count();
        }

        public bool OffsetsEqual((int, int, int) o1, (int, int, int) o2)
        {
            var x1 = Math.Abs(o1.Item1);
            var x2 = Math.Abs(o2.Item1);
            var y1 = Math.Abs(o1.Item2);
            var y2 = Math.Abs(o2.Item2);
            var z1 = Math.Abs(o1.Item3);
            var z2 = Math.Abs(o2.Item3);

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
