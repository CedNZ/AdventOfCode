using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day17 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var target = SetTarget(inputs[0]);
            var probe = new Probe(target);

            var xLB = FindLowerBound(target.x.x1);
            var maxHeight = 0;

            for (var x = xLB; x <= target.x.x2; x++)
            {
                for (var y = 0; y <= Math.Abs(target.y.y1); y++)
                {
                    probe.SetProbe(x, y);
                    while (!probe.TargetCheck().HasValue)
                    {
                        probe.Step();
                    }
                    if (probe.TargetCheck() == true)
                    {
                        maxHeight = Math.Max(maxHeight, probe.MaxHeight);
                    }
                }
            }

            return maxHeight;
        }

        public int FindLowerBound(int target, int lower = 0, int steps = 0)
        {
            if (lower >= target)
            {
                return steps + 1;
            }
            return  FindLowerBound(target, lower * 2 + 1, steps + 1);
        }

        public ((int x1, int x2) x, (int y1, int y2) y) SetTarget(string target)
        {
            var targetCoords = target.Split(':')[1].Split(',');

            var xCoords = targetCoords[0].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var yCoords = targetCoords[1].Split('=')[1].Split('.', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            return ((xCoords[0], xCoords[1]), (yCoords[0], yCoords[1]));
        }

        public long B(List<string> inputs)
        {
            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }

    public class Probe
    {
        public int PositionX;
        public int PositionY;
        public int VelocityX;
        public int VelocityY;
        public ((int x1, int x2) x, (int y1, int y2) y) Target;
        public int MaxSteps;
        public int MaxHeight;

        public Probe(((int x1, int x2) x, (int y1, int y2) y) target)
        {
            Target = target;
            PositionX = 0;
            PositionY = 0;
        }

        public void SetProbe(int velocityX, int velocityY)
        {
            PositionX = 0;
            PositionY = 0;
            VelocityX = velocityX;
            VelocityY = velocityY;
            MaxHeight = 0;
        }

        public void Step()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
            MaxHeight = Math.Max(MaxHeight, PositionY);
            
            if (VelocityX > 0)
            {
                VelocityX--;
            }
            if (VelocityX < 0)
            {
                VelocityX++;
            }

            VelocityY--;
        }

        public int InBounds((int l, int u) b, int v)
        {
            return InBounds(b.l, v, b.u);
        }

        public int InBounds(int xl, int x, int xu)
        {
            if (x < xl)
            {
                return -1;
            }
            if (x > xu)
            {
                return 1;
            }
            return 0;
        }

        public bool? TargetCheck()
        {
            var xCheck = InBounds(Target.x, PositionX);
            var yCheck = InBounds(Target.y, PositionY);

            if (xCheck == 0 && yCheck == 0)
            {
                return true;
            }

            if (yCheck < 0)
            {
                return false;
            }
            if (xCheck > 0)
            {
                return false;
            }
            return null;
        }

        public override string ToString()
        {
            return $"{PositionX}:{PositionY} => {PositionX + VelocityX}:{PositionY + VelocityY}";
        }
    }
}
