using AdventOfCodeCore;

namespace AoC_2022
{
    internal class Day2 : IDay<(RPS elf, RPS me)>
    {
        public long A(List<(RPS elf, RPS me)> inputs)
        {
            long score = 0;

            foreach (var item in inputs)
            {
                var result = item switch
                {
                    (RPS.Rock, RPS.Rock) => Result.Draw,
                    (RPS.Rock, RPS.Paper) => Result.Win,
                    (RPS.Rock, RPS.Scissors) => Result.Loss,

                    (RPS.Paper, RPS.Rock) => Result.Loss,
                    (RPS.Paper, RPS.Paper) => Result.Draw,
                    (RPS.Paper, RPS.Scissors) => Result.Win,

                    (RPS.Scissors, RPS.Rock) => Result.Win,
                    (RPS.Scissors, RPS.Paper) => Result.Loss,
                    (RPS.Scissors, RPS.Scissors) => Result.Draw,
                };
                score += (int)result + (int)item.me;
            }

            return score;
        }

        public long B(List<(RPS elf, RPS me)> inputs)
        {
            long score = 0;

            var game = inputs.Select(x => (x.elf, x.me switch
            {
                RPS.Rock => Result.Loss,
                RPS.Paper => Result.Draw,
                RPS.Scissors => Result.Win,
            }));

            foreach (var item in game)
            {
                var result = item switch
                {
                    (RPS.Rock, Result.Loss) => RPS.Scissors,
                    (RPS.Rock, Result.Draw) => RPS.Rock,
                    (RPS.Rock, Result.Win) => RPS.Paper,

                    (RPS.Paper, Result.Loss) => RPS.Rock,
                    (RPS.Paper, Result.Draw) => RPS.Paper,
                    (RPS.Paper, Result.Win) => RPS.Scissors,

                    (RPS.Scissors, Result.Loss) => RPS.Paper,
                    (RPS.Scissors, Result.Draw) => RPS.Scissors,
                    (RPS.Scissors, Result.Win) => RPS.Rock,
                };
                score += (int)result + (int)item.Item2;
            }

            return score;
        }

        public List<(RPS elf, RPS me)> SetupInputs(string[] inputs)
        {
            return inputs.Select(s =>
            {
                var parts = s.Split(' ');

                var elf = parts[0] switch
                {
                    "A" => RPS.Rock,
                    "B" => RPS.Paper,
                    "C" => RPS.Scissors,
                    _ => throw new ArgumentException(parts[0]),
                };

                var me = parts[1] switch
                {
                    "X" => RPS.Rock,
                    "Y" => RPS.Paper,
                    "Z" => RPS.Scissors,
                    _ => throw new ArgumentException(parts[1]),
                };

                return (elf, me);
            }).ToList();
        }
    }

    internal enum RPS
    {
        Rock = 1,
        Paper,
        Scissors,
    }

    internal enum Result
    {
        Loss = 0,
        Draw = 3,
        Win = 6,
    }
}
