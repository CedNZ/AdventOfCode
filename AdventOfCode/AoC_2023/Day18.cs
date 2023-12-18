using AdventOfCodeCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AoC_2023
{
    public class Day18 : IDay<Pixel>
    {
        public long A(List<Pixel> inputs)
        {
            var maxX = inputs.Max(x => x.X) + 1;
            var maxY = inputs.Max(x => x.Y) + 1;

            var pixels = inputs.ToDictionary(p => (p.X, p.Y), p => p);
            var inside = false;
            var count = 0;

            var image = new Image<Rgba32>(maxX, maxY, new Rgba32(0, 0, 0));

            Pixel last = null!;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (pixels.TryGetValue((x, y), out var pixel))
                    {
                        last = pixel;
                        count++;
                        image[pixel.X, pixel.Y] = new Rgba32(pixel.R, pixel.G, pixel.B);

                        if (pixels.TryGetValue((x, y + 1), out var south))
                        {
                            inside = !inside;
                        }
                    }
                    else if (inside)
                    {
                        try
                        {

                        image[x, y] = new Rgba32(last.R, last.G, last.B);
                        }
                        catch (Exception e)
                        {

                        }
                        count++;
                    }
                }
            }

            image.SaveAsBmp(@"D:\Temp\Day18-A.bmp");

            return count;
        }

        public long B(List<Pixel> inputs)
        {
            var maxX = inputs.Max(x => x.X) + 1;
            var maxY = inputs.Max(x => x.Y) + 1;

            //new bitmap
            var image = new Image<Rgba32>(maxX, maxY);

            inputs.ForEach(p => image[p.X, p.Y] = new Rgba32(p.R, p.G, p.B));

            image.SaveAsBmp(@"D:\Temp\Day18.bmp");

            return inputs.Count;
        }

        public List<Pixel> SetupInputs(string[] inputs)
        {
            int x = 0, y = 0;
            List<Pixel> pixels = new();
            foreach (var line in inputs)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var d = int.Parse(parts[1]);
                var (dx, dy) = parts[0] switch
                {
                    "U" => (0, -1),
                    "D" => (0, 1),
                    "L" => (-1, 0),
                    "R" => (1, 0),
                    _ => throw new NotImplementedException(),
                };
                for (int i = 1; i <= d; i++)
                {
                    x += dx;
                    y += dy;
                    var rgb = parts[2].Trim('(', ')');
                    rgb = rgb[1..];
                    var r = int.Parse(rgb[0..2], System.Globalization.NumberStyles.HexNumber);
                    var g = int.Parse(rgb[2..4], System.Globalization.NumberStyles.HexNumber);
                    var b = int.Parse(rgb[4..6], System.Globalization.NumberStyles.HexNumber);
                    pixels.Add(new Pixel { X = x, Y = y, R = r, G = g, B = b });
                }
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

    public record Pixel
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int R { get; init; }
        public int G { get; init; }
        public int B { get; init; }
    }
}
