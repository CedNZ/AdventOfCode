using AdventOfCodeCore;
using NetTopologySuite.Geometries;

namespace AoC_2023
{
    public class Day18_1 : IDay<Pixel>
    {
        public long A(List<Pixel> inputs)
        {
            return 0;
        }

        public long B(List<Pixel> inputs)
        {
            //calculate area of polygon using shoelace formula
            //https://en.wikipedia.org/wiki/Shoelace_formula
            long area = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                if (i == inputs.Count - 1)
                {
                    area += inputs[i].X * inputs[0].Y;
                    area -= inputs[0].X * inputs[i].Y;
                    continue;
                }
                area += inputs[i].X * inputs[i + 1].Y;
                area -= inputs[i + 1].X * inputs[i].Y;
            }
            area = Math.Abs(area / 2);


            //return (long)area + (inputs.Count / 2) - 1;

            var geoFactory = new GeometryFactory(new PrecisionModel(PrecisionModels.Fixed));

            var coords = inputs.ConvertAll(p => new Coordinate
            {
                X = p.X,
                Y = p.Y,
            });

            coords.Add(coords[0]);

            var poly = geoFactory.CreatePolygon([.. coords]);

            return (long)(poly.Area + (poly.Length / 2) + 1);
        }

        public List<Pixel> SetupInputs(string[] inputs)
        {
            int x = 0, y = 0;
            List<Pixel> pixels = new();
            foreach (var line in inputs)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var rgb = parts[2].Trim('(', ')', '#');
                var d = int.Parse(rgb[0..5], System.Globalization.NumberStyles.HexNumber);
                var (dx, dy) = rgb[^1] switch
                {
                    '3' => (0, -d),
                    '1' => (0, d),
                    '2' => (-d, 0),
                    '0' => (d, 0),
                    _ => throw new NotImplementedException(),
                };
                //for (int i = 1; i <= d; i++)
                //{
                //    x += dx;
                //    y += dy;
                //    pixels.Add(new Pixel { X = x, Y = y, R = 120, G = 120, B = 120 });
                //}
                x += dx;
                y += dy;
                pixels.Add(new Pixel { X = x, Y = y, R = 120, G = 120, B = 120 });
            }

            var minX = pixels.Min(p => p.X);
            var minY = pixels.Min(p => p.Y);

            return pixels.ConvertAll(p => p with
            {
                X = p.X - minX,
                Y = p.Y - minY
            });
        }
    }
}
