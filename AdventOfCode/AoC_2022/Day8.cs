using System.Security.Cryptography.X509Certificates;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day8 : IDay<int[]>
    {
        static int Rows, Cols;

        public long A(List<int[]> inputs)
        {
            Rows = inputs.Count;
            Cols = inputs[0].Length;

            for (int row = 0; row < inputs.Count; row++)
            {
                GetVisible(inputs[row], row, true);
            }

            for (int col = 0; col < inputs[0].Length; col++)
            {
                var line = inputs.Select(l => l[col]).ToArray();
                GetVisible(line, col, false);
            }

            return VisibleTrees.Count();
        }

        static HashSet<(int, int, int)> VisibleTrees = new();

        internal void GetVisible(int[] treeline, int index, bool row)
        {
            if (row)
            {
                VisibleTrees.Add((index, 0, treeline[0]));
                VisibleTrees.Add((index, Cols - 1, treeline[^1]));
            }
            else
            {
                VisibleTrees.Add((0, index, treeline[0]));
                VisibleTrees.Add((Rows - 1, index, treeline[^1]));
            }

            var visibleIndexesTop = new List<int> { 0 };
            for (int i = 1; i < treeline.Length - 1; i++)
            {
                if (treeline[i] > visibleIndexesTop.Max(x => treeline[x]))
                {
                    visibleIndexesTop.Add(i);
                }
            }

            var visibleIndexesBottom = new List<int> { treeline.Length - 1 };
            for (int i = treeline.Length - 2; i > 0; i--)
            {
                if (treeline[i] > visibleIndexesBottom.Max(x => treeline[x]))
                {
                    visibleIndexesBottom.Add(i);
                }
            }

            var visibleIndexes = visibleIndexesTop.Union(visibleIndexesBottom).ToList();

            foreach (var i in visibleIndexes)
            {
                if (row)
                {
                    VisibleTrees.Add((index, i, treeline[i]));
                }
                else
                {
                    VisibleTrees.Add((i, index, treeline[i]));
                }
            }
        }

        public long B(List<int[]> inputs)
        {
            var notEdge = ((int, int, int) x) => x.Item1 != 0 && x.Item1 != Rows - 1
                                                    && x.Item2 != 0 && x.Item2 != Cols - 1;


            var row = VisibleTrees.Where(notEdge).GroupBy(x => x.Item1).OrderBy(g => g.Count()).First().Key;
            var col = VisibleTrees.Where(notEdge).GroupBy(x => x.Item2).OrderBy(g => g.Count()).First().Key;

            var rowVisible = VisibleTrees.Where(notEdge).Where(x => x.Item1 == row).ToList();
            var colVisible = VisibleTrees.Where(notEdge).Where(x => x.Item2 == col).ToList();

            return rowVisible.Union(colVisible).Max(x => ScenicScore(inputs, x));
        }

        internal int ScenicScore(List<int[]> inputs, (int, int, int) tree)
        {
            var row = inputs[tree.Item1];
            var col = inputs.Select(l => l[tree.Item2]).ToArray();

            var (top, left, bottom, right) = (0,0,0,0);
            for (int i = tree.Item2 + 1; i < row.Length; i++)
            {
                right++;
                if (row[i] >= tree.Item3)
                {
                    break;
                }
            }
            for (int i = tree.Item2 - 1; i >= 0; i--)
            {
                left++;
                if (row[i] >= tree.Item3)
                {
                    break;
                }
            }

            for (int i = tree.Item1 + 1; i < col.Length; i++)
            {
                bottom++;
                if (col[i] >= tree.Item3)
                {
                    break;
                }
            }
            for (int i = tree.Item1 - 1; i >= 0; i--)
            {
                top++;
                if (col[i] >= tree.Item3)
                {
                    break;
                }
            }

            return top * bottom * right * left;
        }

        public List<int[]> SetupInputs(string[] inputs)
        {
            return inputs.Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToList();
        }
    }
}
