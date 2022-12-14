using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day14 : IDay<Line>
    {
        public long A(List<Line> inputs)
        {
            List<Sand> settledSand = new();
            var sand = new Sand();
            
            while (true)
            {
                sand.Move(inputs, settledSand);
                if (sand.Y > inputs.Max(l => Math.Max(l.Y1, l.Y2)))
                {
                    return settledSand.Count();
                }
                if (settledSand.Contains(sand))
                {
                    sand = new Sand();
                }
            }

            return 0;
        }

        public void Draw(List<Line> inputs)
        {
            int maxY = inputs.Max(l => Math.Max(l.Y1, l.Y2));
            int maxX = inputs.Max(l => Math.Max(l.X1, l.X2));

            Console.Clear();
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (inputs.Any(l => l.ContainsPoint(i, j)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public long B(List<Line> inputs)
        {
            throw new NotImplementedException();
        }

        public List<Line> SetupInputs(string[] inputs)
        {
            return inputs.SelectMany(l =>
                l.Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                    .OverlappingSets(2)
                    .Where(x => x.Count() == 2)
                    .Select(x => new Line(x)))
                .ToList();
        }
    }

    public class Sand
    {
        public int X { get; set; } = 500;
        public int Y { get; set; } = 0;

        public bool Move(List<Line> lines, List<Sand> sand)
        {
            var tempY = Y + 1;
            var tempX = X;
            if (lines.Any(l => l.ContainsPoint(tempX, tempY)) || sand.Any(s => s.Y == tempY && s.X == tempX))
            {
                tempX = X - 1;
            }
            if (lines.Any(l => l.ContainsPoint(tempX, tempY)) || sand.Any(s => s.Y == tempY && s.X == tempX))
            {
                tempX = X + 1;
            }
            if (lines.Any(l => l.ContainsPoint(tempX, tempY)) || sand.Any(s => s.Y == tempY && s.X == tempX))
            {
                sand.Add(this);
                return false; //Came to rest at previous point
            }

            Y = tempY;
            X = tempX;
            return true;
        }

        public override string ToString() => $"{X},{Y}";
    }

    public class Line
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }

        public int X2 { get; set; }
        public int Y2 { get; set; }

        public Line(IEnumerable<string> args)
        {
            var l1 = args.First().Split(',').Select(int.Parse);
            var l2 = args.Skip(1).First().Split(',').Select(int.Parse);

            X1 = l1.First();
            Y1 = l1.Skip(1).First();

            X2 = l2.First();
            Y2 = l2.Skip(1).First();
        }

        public bool ContainsPoint(int X, int Y)
        {
            if (X == X1 && X == X2)
            {
                return (Y - Y1) * (Y2 - Y) >= 0;
            }
            if (Y == Y1 && Y == Y2)
            {
                return (X - X1) * (X2 - X) >= 0;
            }
            return false;
        }
    }
}
