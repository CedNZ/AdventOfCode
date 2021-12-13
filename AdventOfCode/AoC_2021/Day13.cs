using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day13 : IDay<string>
    {
        List<(int x, int y)> paper = new ();

        public long A(List<string> inputs)
        {
            foreach (var line in inputs.Where(l => !string.IsNullOrEmpty(l) && !l.StartsWith("fold")))
            {
                var coords = line.Split(',').Select(int.Parse).ToList();
                if (!paper.Contains((coords[0], coords[1])))
                {
                    paper.Add((coords[0], coords[1]));
                }
            }
            //Draw();
            List<(int x, int y)> belowFold;
            foreach (var item in inputs.Where(l => l.StartsWith("fold")))
            {
                var fold = item.Split("fold along ", StringSplitOptions.RemoveEmptyEntries)[0].Split('=');
                if (fold[0] == "y")
                {
                    var foldCoord = int.Parse(fold[1]);
                    belowFold = paper.Where(p => p.y >= foldCoord).ToList();
                    paper.RemoveAll(p => belowFold.Contains(p));

                    belowFold = belowFold.Select(c => (c.x, c.y = foldCoord - (c.y - foldCoord))).ToList();

                    paper.AddRange(belowFold);
                }
                else
                {
                    var foldCoord = int.Parse(fold[1]);
                    belowFold = paper.Where(p => p.x >= foldCoord).ToList();
                    paper.RemoveAll(p => belowFold.Contains(p));

                    belowFold = belowFold.Select(c => (c.x = foldCoord - (c.x - foldCoord), c.y)).ToList();

                    paper.AddRange(belowFold);
                }

                paper = paper.GroupBy(p => p).Select(g => g.First()).ToList();
                break;
            }

            return paper.Count();
        }

        public void Draw()
        {
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            for (int i = 0; i <= paper.Max(p => p.y); i++)
            {
                for (int j = 0; j <= paper.Max(p => p.x); j++)
                {
                    var output = paper.Contains((j, i)) ? "#" : ".";
                    Console.Write(output);
                }
                Console.WriteLine();
            }
        }

        public long B(List<string> inputs)
        {
            foreach (var line in inputs.Where(l => !string.IsNullOrEmpty(l) && !l.StartsWith("fold")))
            {
                var coords = line.Split(',').Select(int.Parse).ToList();
                if (!paper.Contains((coords[0], coords[1])))
                {
                    paper.Add((coords[0], coords[1]));
                }
            }
            //Draw();
            List<(int x, int y)> belowFold;
            foreach (var item in inputs.Where(l => l.StartsWith("fold")))
            {
                var fold = item.Split("fold along ", StringSplitOptions.RemoveEmptyEntries)[0].Split('=');
                if (fold[0] == "y")
                {
                    var foldCoord = int.Parse(fold[1]);
                    belowFold = paper.Where(p => p.y >= foldCoord).ToList();
                    paper.RemoveAll(p => belowFold.Contains(p));

                    belowFold = belowFold.Select(c => (c.x, c.y = foldCoord - (c.y - foldCoord))).ToList();

                    paper.AddRange(belowFold);
                }
                else
                {
                    var foldCoord = int.Parse(fold[1]);
                    belowFold = paper.Where(p => p.x >= foldCoord).ToList();
                    paper.RemoveAll(p => belowFold.Contains(p));

                    belowFold = belowFold.Select(c => (c.x = foldCoord - (c.x - foldCoord), c.y)).ToList();

                    paper.AddRange(belowFold);
                }

                paper = paper.GroupBy(p => p).Select(g => g.First()).ToList();
            }
            Draw();

            return paper.Count();
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
