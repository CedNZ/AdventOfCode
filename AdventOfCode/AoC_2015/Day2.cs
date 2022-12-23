using AdventOfCodeCore;

namespace AoC_2015
{
    public class Day2 : IDay<(int l, int w, int h)>
    {
        public long A(List<(int l, int w, int h)> inputs)
        {
            Func<(int l, int w, int h), int> area = b =>
            {
                var s1 = (2 * b.l * b.w);
                var s2 = (2 * b.h * b.w);
                var s3 = (2 * b.l * b.h);
                return s1 + s2 + s3 + (new[] { s1, s2, s3 }.Min() / 2);
            };
            return inputs.Sum(area);
        }

        public long B(List<(int l, int w, int h)> inputs)
        {
            Func<(int l, int w, int h), int> ribbons = b =>
            {
                var volume = b.l * b.w * b.h;
                var p1 = (2 * b.l) + (2 * b.w);
                var p2 = (2 * b.l) + (2 * b.h);
                var p3 = (2 * b.h) + (2 * b.w);
                return new[] { p1, p2, p3 }.Min() + volume;
            };
            return inputs.Sum(ribbons);
        }

        public List<(int l, int w, int h)> SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var p = l.Split('x');
                return (int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
            }).ToList();
        }
    }
}
