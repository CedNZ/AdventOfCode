using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day10 : IDay<List<Pipe>>
    {
        public long A(List<List<Pipe>> inputs)
        {
            var start = inputs.Select(l => l.FirstOrDefault(p => p.Start))
                .Where(p => p != null)
                .Single();

            var openSet = new List<Pipe>
            { 
                start
            };
            var visited = new HashSet<Pipe>();
            while (openSet.Count > 0)
            {
                var groups = openSet.GroupBy(p => new { p.X, p.Y, p.Steps })
                    .OrderByDescending(g => g.Count());
                if (groups.First().Count() == 2)
                {
                    return groups.First().Key.Steps;
                }
                var current = openSet.First();
                visited.Add(current);
                openSet.Remove(current);
                var (x, y) = (current.X, current.Y);
                if (current.North && y > 0)
                {
                    var north = inputs[y - 1][x];
                    if (north.South && visited.Contains(north) is false)
                    {
                        north.Steps = current.Steps + 1;
                        openSet.Add(north);
                    }
                }
                if (current.South && y < inputs.Count - 1)
                {
                    var south = inputs[y + 1][x];
                    if (south.North && visited.Contains(south) is false)
                    {
                        south.Steps = current.Steps + 1;
                        openSet.Add(south);
                    }
                }
                if (current.East && x < inputs[y].Count - 1)
                {
                    var east = inputs[y][x + 1];
                    if (east.West && visited.Contains(east) is false)
                    {
                        east.Steps = current.Steps + 1;
                        openSet.Add(east);
                    }
                }
                if (current.West && x > 0)
                {
                    var west = inputs[y][x - 1];
                    if (west.East && visited.Contains(west) is false)
                    {
                        west.Steps = current.Steps + 1;
                        openSet.Add(west);
                    }
                }
            }

            return default;
        }

        public long B(List<List<Pipe>> inputs)
        {
            var start = inputs.Select(l => l.FirstOrDefault(p => p.Start))
                .Where(p => p != null)
                .Single();

            Dictionary<Pipe, Pipe> pipes = new Dictionary<Pipe, Pipe>();
            var current = start;
            Pipe? previous = null;
            while (pipes.ContainsKey(current) is false)
            {
                if (current.North)
                {
                    var north = inputs[current.Y - 1][current.X];
                    if (north.South && previous != north)
                    {
                        previous = current;
                        current = north;
                        pipes.Add(previous, current);
                        continue;
                    }
                }
                if (current.South)
                {
                    var south = inputs[current.Y + 1][current.X];
                    if (south.North && previous != south)
                    {
                        previous = current;
                        current = south;
                        pipes.Add(previous, current);
                        continue;
                    }
                }
                if (current.East)
                {
                    var east = inputs[current.Y][current.X + 1];
                    if (east.West && previous != east)
                    {
                        previous = current;
                        current = east;
                        pipes.Add(previous, current);
                        continue;
                    }
                }
                if (current.West)
                {
                    var west = inputs[current.Y][current.X - 1];
                    if (west.East && previous != west)
                    {
                        previous = current;
                        current = west;
                        pipes.Add(previous, current);
                        continue;
                    }
                }
            }

            start.North = false;
            start.South = false;
            start.East = false;
            start.West = false;

            if (pipes[start].North || previous.South)
            {
                start.South = true;
            }
            if (pipes[start].South || previous.North)
            {
                start.North = true;
            }
            if (pipes[start].East || previous.West)
            {
                start.West = true;
            }
            if (pipes[start].West || previous.East)
            {
                start.East = true;
            }


            var containCount = 0;

            foreach (var line in inputs)
            {
                var pipeCount = 0;
                for (int i = 0; i < line.Count; i++)
                {
                    var p = line[i];
                    if (pipes.ContainsKey(p))
                    {
                        if (p.South)
                        {
                            pipeCount++;
                        }
                    }
                    else
                    {
                        if (pipeCount % 2 == 1)
                        {
                            containCount++;
                        }
                    }
                }
            }

            return containCount;
        }

        public List<List<Pipe>> SetupInputs(string[] inputs)
        {
            return inputs.Select((l, y) => l.Select((c, x) => new Pipe(c, x, y)).ToList()).ToList();
        }
    }

    public class Pipe
    {
        public int X { get; init; }
        public int Y { get; init; }
        public bool North { get; set; }
        public bool South { get; set; }
        public bool East { get; set; }
        public bool West { get; set; }

        public bool Start { get; init; }
        public char Char { get; init; }
        public int Steps { get; set; } = 0;

        public Pipe(char p, int x, int y)
        {
            X = x;
            Y = y;
            Char = p;
            switch (p)
            {
                case '|':
                    North = true;
                    South = true;
                    break;
                case '-':
                    East = true;
                    West = true;
                    break;
                case 'L':
                    North = true;
                    East = true;
                    break;
                case 'J':
                    North = true;
                    West = true;
                    break;
                case '7':
                    South = true;
                    West = true;
                    break;
                case 'F':
                    South = true;
                    East = true;
                    break;
                case '.':
                    break;
                case 'S':
                    Start = true;
                    North = true;
                    South = true;
                    East = true;
                    West = true;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Char}";
        }
    }
}
