using AdventOfCodeCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AoC_2025
{
    public class Day9 : IDay<(int X, int Y)>
    {
        public long A(List<(int X, int Y)> inputs)
        {
            var furthest = inputs
                .Pairs()
                .Select(p => new
                {
                    Pair = p,
                    Distance = p.ManhattanDistance(x => x.X, x => x.Y),
                    Area = Area(p.Item1.X, p.Item2.X, p.Item1.Y, p.Item2.Y),
                })
                .OrderByDescending(p => p.Distance)
                .ThenByDescending(p => p.Area)
                .FirstOrDefault();

            return furthest.Area;
        }

        long Area(long x1, long x2, long y1, long y2)
        {
            return ((Math.Abs(x1 - x2) + 1)
                    * (Math.Abs(y1 - y2) + 1));
        }

        public long B(List<(int X, int Y)> inputs)
        {
            var geoFact = GeometryFactory.Fixed;
            var coordinates = inputs.ConvertAll(x => new Coordinate(x.X, x.Y));
            coordinates.Add(coordinates[0]);
            var linearRing = new LinearRing([..coordinates]);
            var polygon = new Polygon(linearRing, geoFact);

            var largest = coordinates
                .Pairs()
                .Select(p => geoFact.ToGeometry(new Envelope(p.Item1, p.Item2)))
                .OrderByDescending(x => x.Area)
                .FirstOrDefault(x => x.CoveredBy(polygon));

            return Area((long)largest.Coordinates[0].X, (long)largest.Coordinates[2].X,
                (long)largest.Coordinates[0].Y, (long)largest.Coordinates[2].Y);
        }

        public List<(int X, int Y)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x =>
            {
                var parts = x.Split(',');
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }).ToList();
        }
    }
}
