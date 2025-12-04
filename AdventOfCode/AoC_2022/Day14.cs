using Raylib_cs;
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

        static bool windowInitialised = false;
        const int screenWidth = 800;
        const int screenHeight = 450;


        public void Draw(List<Line> lines, List<Sand> sand)
        {
            if (windowInitialised is false)
            {
                Raylib.InitWindow(screenWidth, screenHeight, "AoC 2022 - Day 14");
                windowInitialised = true;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            foreach (var line in lines)
            {
                Raylib.DrawRectangle(line.X1, line.Y1, line.X2 - line.X1, line.Y2 - line.Y1, Color.Black);
            }
        }

        public long B(List<Line> inputs)
        {
            List<Sand> settledSand = new();
            List<Sand> fallingSand = new()
            {
                new Sand(),
            };

            //HashSet<(int X, int Y, bool CanMove)> sandHash = new();
            //sandHash.Add((500, 0, true));

            //while (sandHash.Any(s => s.CanMove))
            //{
            //    for (int i = 0; i < sandHash; i++)
            //    {

            //    }
            //}

            var maxY = inputs.Max(l => Math.Max(l.Y1, l.Y2)) + 2;

            inputs.Add(new Line
            {
                Y1 = maxY,
                Y2 = maxY,
                X1 = -1000,
                X2 = 1000,
            });

            bool drop = true;

            while (fallingSand.Count > 0)
            {
                for (int i = 0; i < fallingSand.Count; i++)
                {
                    var sand = fallingSand[i];
                    if (sand.Move(inputs, settledSand) is false) //came to rest
                    {
                        fallingSand.Remove(sand);
                        if (sand.Y > 0)
                        {
                            Console.WriteLine($"Landed Sand {settledSand.Count}");
                            fallingSand.Add(new Sand());
                        } 
                        else
                        {
                            return settledSand.Count();
                        }
                    }
                }
                if (drop)
                {
                    Console.WriteLine($"Dropping Sand {fallingSand.Count}");
                    fallingSand.Add(new Sand());
                }
                drop = !drop;
            }

            return settledSand.Count();
        }

        public List<Line> SetupInputs(string[] inputs)
        {
            return inputs.SelectMany(l =>
                l.Split(" -> ", StringSplitOptions.RemoveEmptyEntries)
                    .OverlappingSets(2)
                    .Where(x => x.Count() == 2)
                    .Select(x => new Line(x)))
                .DistinctBy(l => new { l.X1, l.X2, l.Y1, l.Y2 })
                .ToList();
        }
    }

    public class Sand
    {
        public int X { get; set; } = 500;
        public int Y { get; set; } = 0;

        public int OldX { get; set; }
        public int OldY { get; set; }

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

        public Line()
        {
            
        }

        public Line(IEnumerable<string> args)
        {
            var l1 = args.First().Split(',').Select(int.Parse);
            var l2 = args.Skip(1).First().Split(',').Select(int.Parse);

            X1 = l1.First();
            Y1 = l1.Skip(1).First();

            X2 = l2.First();
            Y2 = l2.Skip(1).First();

            (X1, X2) = (Math.Min(X1, X2), Math.Max(X1, X2));
            (Y1, Y2) = (Math.Min(Y1, Y2), Math.Max(Y1, Y2));
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
