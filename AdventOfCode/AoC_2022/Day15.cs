using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCodeCore;
using Raylib_cs;

namespace AoC_2022
{
    public class Day15 : IDay<((int X, int Y) Sensor, (int X, int Y) Beacon)>
    {
        public long A(List<((int X, int Y) Sensor, (int X, int Y) Beacon)> inputs)
        {
            HashSet<(int X, int Y)> Sensors = new();
            HashSet<(int X, int Y)> Beacons = new();

            int yIntercept = inputs.Count < 20 ? 10 : 2_000_000;
            HashSet<int> xPositions = new();

            foreach (var input in inputs)
            {
                Sensors.Add(input.Sensor);
                Beacons.Add(input.Beacon);

                var sensor = input.Sensor;
                var beacon = input.Beacon;

                var yDist = Math.Abs(sensor.Y - beacon.Y);
                var xDist = Math.Abs(sensor.X - beacon.X);
                var MD = xDist + yDist;

                var ydiff = Math.Abs(yIntercept - sensor.Y);
                var xRange = MD - ydiff;

                for (int d = sensor.X - xRange; d <= sensor.X + xRange; d++)
                {
                    xPositions.Add(d);
                }
            }

            foreach (var beacon in Beacons.Where(b => b.Y == yIntercept))
            {
                xPositions.Remove(beacon.X);
            }
            foreach (var sensor in Sensors.Where(s => s.Y == yIntercept))
            {
                xPositions.Remove(sensor.X);
            }

            return xPositions.Count;
        }

        public long B(List<((int X, int Y) Sensor, (int X, int Y) Beacon)> inputs)
        {
            throw new NotImplementedException();
        }

        public List<((int X, int Y) Sensor, (int X, int Y) Beacon)> SetupInputs(string[] inputs)
        {
            List<((int X, int Y) Sensor, (int X, int Y) Beacon)> sensors = new();
            foreach (var line in inputs)
            {
                var match = Regex.Match(line, @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
                var sX = int.Parse(match.Groups[1].Value);
                var sY = int.Parse(match.Groups[2].Value);
                var bX = int.Parse(match.Groups[3].Value);
                var bY = int.Parse(match.Groups[4].Value);

                sensors.Add(((sX, sY), (bX, bY)));
            }
            return sensors;
        }
    }
}
