using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day13 : IDay<Crane>
    {
        public long A(List<Crane> inputs)
        {
            var tokens = 0L;
            foreach (var crane in inputs)
            {
                var Ax = crane.A.X;
                var Ay = crane.A.Y;
                var Bx = crane.B.X;
                var By = crane.B.Y;
                var Tx = crane.Target.x;
                var Ty = crane.Target.y;

                if (Intersect(Ax, Ay, Bx, By, Tx, Ty, out var ap, out var bp))
                {
                    var cost = ((long)ap * 3) + (long)bp;
                    tokens += cost;
                }
            }
            return tokens;
        }

        public long B(List<Crane> inputs)
        {
            var tokens = 0L;
            foreach (var crane in inputs)
            {
                var Ax = crane.A.X;
                var Ay = crane.A.Y;
                var Bx = crane.B.X;
                var By = crane.B.Y;
                var Tx = crane.Target.x + 10000000000000L;
                var Ty = crane.Target.y + 10000000000000L;

                if (Intersect(Ax, Ay, Bx, By, Tx, Ty, out var ap, out var bp))
                {
                    var cost = ((long)ap * 3) + (long)bp;
                    tokens += cost;
                }
            }
            return tokens;
        }

        private static bool Intersect(
            long ax, long ay,
            long bx, long by,
            long tx, long ty,
            out decimal ap, out decimal bp)
        {
            var am = ((decimal)ay) / ax;
            var bm = ((decimal)by) / bx;

            var bc = ty - (bm * tx);

            var X = bc / (am - bm);

            X = Math.Round(X, 5);

            if (X % ax != 0)
            {
                ap = 0;
                bp = 0;
                return false;
            }

            ap = X / ax;
            bp = (tx - X) / bx;

            if (bp % 1 != 0)
            {
                ap = 0;
                bp = 0;
                return false;
            }

            return true;
        }


        public List<Crane> SetupInputs(string[] inputs)
        {
            return inputs.Chunk(4)
                .Select(x =>
                {
                    var matchA = Regex.Matches(x[0], @"(\d+)");
                    var matchB = Regex.Matches(x[1], @"(\d+)");
                    var target = Regex.Matches(x[2], @"(\d+)");
                    return new Crane
                    (
                        new Vector(int.Parse(matchA[0].Value), int.Parse(matchA[1].Value)),
                        new Vector(int.Parse(matchB[0].Value), int.Parse(matchB[1].Value)),
                        (long.Parse(target[0].Value), long.Parse(target[1].Value))
                    );
                })
                .ToList();
        }
    }

    public record Vector(long X, long Y);

    public record Crane(Vector A, Vector B, (long x, long y) Target);
}
