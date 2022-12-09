using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day9 : IDay<Instruction>
    {
        long IDayOut<Instruction, long>.A(List<Instruction> inputs)
        {
            (int X, int Y) Head = (0, 0);
            (int X, int Y) Tail = (0, 0);

            HashSet<(int X, int Y)> visitedLocations = new()
            {
                Tail
            };

            foreach (var instruction in inputs)
            {
                for (int i = 0; i < instruction.Steps; i++)
                {
                    Head = instruction.Direction switch
                    {
                        Direction.Left => (Head.X - 1, Head.Y),
                        Direction.Right => (Head.X + 1, Head.Y),
                        Direction.Up => (Head.X, Head.Y - 1),
                        Direction.Down => (Head.X, Head.Y + 1),
                    };

                    if (AreAdjacent(Head, Tail) is false) //move tail
                    {
                        var diffX = Head.X - Tail.X;
                        var diffY = Head.Y - Tail.Y;

                        if (diffX != 0)
                        {
                            Tail.X += diffX > 0 ? 1 : -1;
                        }
                        if (diffY != 0)
                        {
                            Tail.Y += diffY > 0 ? 1 : -1;
                        }
                        visitedLocations.Add(Tail);
                    }
                }
            }

            return visitedLocations.Count();
        }

        private bool AreAdjacent((int X, int Y) head, (int X, int Y) tail)
        {
            return Math.Abs(head.X - tail.X) <= 1 && Math.Abs(head.Y - tail.Y) <= 1;
        }

        long IDayOut<Instruction, long>.B(List<Instruction> inputs)
        {
            throw new NotImplementedException();
        }

        List<Instruction> IDayOut<Instruction, long>.SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var parts = l.Split(' ');
                var direction = parts[0] switch
                {
                    "U" => Direction.Up,
                    "D" => Direction.Down,
                    "L" => Direction.Left,
                    "R" => Direction.Right,
                    _ => throw new NotImplementedException()
                };

                return new Instruction
                {
                    Direction = direction,
                    Steps = int.Parse(parts[1])
                };
            }).ToList();
        }
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal struct Instruction
    {
        internal Direction Direction { get; set; }
        internal int Steps { get; set; }
    }

}
