using AdventOfCodeCore;

namespace AoC_2025
{
    public class Day4 : IDay<PaperRoll>
    {
        public long A(List<PaperRoll> inputs)
        {
            var canMove = 0;
            foreach (var paperRoll in inputs)
            {
                var neighbours = inputs
                    .Where(r => r != paperRoll
                                && (r.Y == paperRoll.Y
                                    || r.Y == paperRoll.Y + 1
                                    || r.Y == paperRoll.Y - 1)
                                && (r.X == paperRoll.X
                                    || r.X == paperRoll.X + 1
                                    || r.X == paperRoll.X - 1))
                    .ToList();
                if (neighbours.Count < 4)
                {
                    paperRoll.CanMove = true;
                    canMove++;
                }
            }

            return canMove;
        }

        public long B(List<PaperRoll> inputs)
        {
            var canMove = 0;
            var moved = true;

            while (moved)
            {
                moved = false;
                foreach (var paperRoll in inputs)
                {
                    var neighbours = inputs
                        .Where(r => r != paperRoll
                                    && (r.Y == paperRoll.Y
                                        || r.Y == paperRoll.Y + 1
                                        || r.Y == paperRoll.Y - 1)
                                    && (r.X == paperRoll.X
                                        || r.X == paperRoll.X + 1
                                        || r.X == paperRoll.X - 1))
                        .ToList();
                    if (neighbours.Count < 4)
                    {
                        paperRoll.CanMove = true;
                        canMove++;
                        moved = true;
                    }
                }

                foreach (var paperRoll in inputs.Where(r => r.CanMove).ToList())
                {
                    inputs.Remove(paperRoll);
                }
            }

            return canMove;
        }

        public List<PaperRoll> SetupInputs(string[] inputs)
        {
            List<PaperRoll> rolls = [];
            for (int y = 0; y < inputs.Length; y++)
            {
                rolls.AddRange(inputs[y]
                    .Select((c, x) => c == '@' ? new PaperRoll(x, y) : null)
                    .Where(r => r != null)!);
            }
            return rolls;
        }

    }

    public class PaperRoll
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool CanMove { get; set; }

        public PaperRoll(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}