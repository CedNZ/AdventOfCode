using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day2 : IDay<(string, int)>
    {
        public long A(List<(string, int)> inputs)
        {
            var coords = (X: 0, Y: 0);

            foreach (var command in inputs)
            {
                switch (command.Item1)
                {
                    case "forward":
                        coords.X += command.Item2;
                        break;
                    case "down":
                        coords.Y += command.Item2;
                        break;
                    case "up":
                        coords.Y += command.Item2;
                        break;
                }
            }

            return coords.X * coords.Y;
        }

        public long B(List<(string, int)> inputs)
        {
            throw new NotImplementedException();
        }

        public List<(string, int)> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => (x.Split(' ')[0], int.Parse(x.Split(' ')[1]))).ToList();
        }
    }
}
