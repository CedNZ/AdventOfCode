using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day2 : IDay<Game>
    {
        long IDayOut<Game, long>.A(List<Game> inputs)
        {
            Dictionary<Cube, int> limits = new Dictionary<Cube, int>
            {
                { Cube.Red, 12 },
                { Cube.Green, 13 },
                { Cube.Blue, 14 },
            };

            return inputs.Where(x => x.Games.All(g =>
                g.All(kv => kv.Value <= limits[kv.Key])
            ))
            .Sum(x => x.Id);
        }

        long IDayOut<Game, long>.B(List<Game> inputs)
        {
            return inputs.Select(i =>
            {
                var minR = i.Games.Max(x => x.TryGetValue(Cube.Red, out var r) ? r : r);
                var minG = i.Games.Max(x => x.TryGetValue(Cube.Green, out var g) ? g : g);
                var minB = i.Games.Max(x => x.TryGetValue(Cube.Blue, out var b) ? b : b);

                return minR * minG * minB;
            }).Sum();
        }

        List<Game> IDayOut<Game, long>.SetupInputs(string[] inputs)
        {
            return inputs.Select(x =>
            {
                var parts = x.Split(':');
                var id = int.Parse(parts[0].Split(' ')[1]);
                var games = parts[1].Split(';')
                    .Select(y => y.Split(',')
                        .Select(z => z.Trim().Split(' '))
                        .ToDictionary(kv => Enum.Parse<Cube>(kv[1], true), kv => int.Parse(kv[0])));


                return new Game
                {
                    Id = id,
                    Games = games.ToList(),
                };
            }).ToList();
        }
    }

    internal class Game
    {
        public int Id { get; set; }
        public List<Dictionary<Cube, int>> Games { get; set; } = [];
    }

    internal enum Cube
    {
        Red,
        Green,
        Blue,
    }
}
