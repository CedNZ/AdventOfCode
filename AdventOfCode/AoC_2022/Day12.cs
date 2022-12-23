using System.Text;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day12 : IDay<Elevation>
    {
        long IDayOut<Elevation, long>.A(List<Elevation> inputs)
        {
            var start = inputs.Find(x => x.Start);
            var end = inputs.Find(x => x.End);
            return AstarFind(inputs, start, end);
        }

        static int Columns { get; set; }
        static int Rows { get; set; }

        long IDayOut<Elevation, long>.B(List<Elevation> inputs)
        {
            Thread.Sleep(2000);
            drawInitialised = false;
            var start = inputs.Find(x => x.End);
            return AstarFind(inputs, start, null, true);
        }

        public int AstarFind(List<Elevation> positions, Elevation start, Elevation end, bool partb = false)
        {
            List<Elevation> openSet = new()
            {
                start
            };

            Dictionary<Elevation, Elevation > cameFrom = new();

            Dictionary<Elevation, int> gScore = new()
            {
                { start, 0 }
            };

            int GScore(Elevation x) => gScore.TryGetValue(x, out var gscore) ? gscore : 100_000_000;
            int h(Elevation x) => (((Columns - x.Column) * (Rows - x.Row))) + (GScore(x) * 1000);

            Dictionary<Elevation, int> fScore = new()
            {
                { start, h(start) }
            };

            var draw = true;

            while (openSet.Count > 0)
            {
                if (draw)
                {
                    Draw(positions, cameFrom, openSet);
                }
                var current = openSet.OrderBy(n => fScore[n]).First();
                if ((partb && current.Height == 1) || current == end)
                {
                    var steps = 1;
                    for (var parent = cameFrom[current]; parent != start; )
                    {
                        if (draw)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(parent.Column, parent.Row);
                            Console.Write((char)(parent.Height + 96));
                        }

                        parent = cameFrom[parent];
                        steps++;
                    }
                    if (draw)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(0, Rows + 3);
                    }
                    return steps;
                }

                openSet.Remove(current);
                foreach (var neighbour in Neighbours(positions, current, partb))
                {
                    var tentativeScore = GScore(current) + neighbour.Height;
                    if (tentativeScore < GScore(neighbour))
                    {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = tentativeScore;
                        fScore[neighbour] = tentativeScore + h(neighbour);
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            return -1;
        }

        static bool drawInitialised = false;

        public void Draw(List<Elevation> positions, Dictionary<Elevation, Elevation> cameFrom, List<Elevation> openSet)
        {
            if (drawInitialised is false)
            {
                Console.Clear();
                positions.ForEach(p =>
                {
                    if (p.Start)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else if (p.End)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.SetCursorPosition(p.Column, p.Row);
                    Console.Write((char)(p.Height + 96));
                });
                drawInitialised = true;
            }

            positions.ForEach(p =>
            {
                if (openSet.Contains(p))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(p.Column, p.Row);
                    Console.Write((char)(p.Height + 96));

                    if (cameFrom.TryGetValue(p, out var from))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(from.Column, from.Row);
                        Console.Write((char)(from.Height + 96));
                    }
                }
            });
        }

        public IEnumerable<Elevation> Neighbours(List<Elevation> positions, Elevation position, bool partb = false)
        {
            return positions.Where(p => (partb ? p.Height >= position.Height - 1 : p.Height <= position.Height + 1)
                                && (((p.Column == position.Column
                                    && Math.Abs(p.Row - position.Row) == 1))
                                || ((p.Row == position.Row
                                    && Math.Abs(p.Column - position.Column) == 1))));
        }

        List<Elevation> IDayOut<Elevation, long>.SetupInputs(string[] inputs)
        {
            var elevations = new List<Elevation>();
            Rows = 0;
            Columns = 0;
            foreach (var row in inputs)
            {
                Columns = 0;
                foreach (var col in row)
                {
                    var elevation = new Elevation
                    {
                        Height = col - 96,
                        Row = Rows,
                        Column = Columns++,

                    };
                    if (col == 'S')
                    {
                        elevation.Start = true;
                        elevation.Height = 1;
                    }
                    else if (col == 'E')
                    {
                        elevation.End = true;
                        elevation.Height = 27;
                    }

                    elevations.Add(elevation);
                }
                Rows++;
            }
            return elevations;
        }
    }

    public class Elevation
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Height { get; set; }

        public bool Start { get; set; }
        public bool End { get; set; }

        public override string ToString() => $"{(char)(Height + 96)} {Row} {Column}";
    }
}
