using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day18 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            List<List<Pair>> AllPairs = new List<List<Pair>>();
            Pair currentPair = null;
            List<Pair> pairs;
            foreach (var line in inputs)
            {
                var expanded = Parse(line);
                Reduce(ref expanded);
                AllPairs.Add(expanded);
            }

            var currentPairs = AllPairs.First();
            foreach (var nextPair in AllPairs.Skip(1))
            {
                currentPair = new Pair
                {
                    LeftPair = currentPairs.FirstOrDefault(p => p.Depth == 0),
                    RightPair = nextPair.FirstOrDefault(p => p.Depth == 0),
                    Depth = 0,
                };
                Nest(currentPair.LeftPair);
                Nest(currentPair.RightPair);

                currentPairs = Parse(BuildString(currentPair));
                Reduce(ref currentPairs);
            }

            return Magnitude(currentPair);
        }

        private List<Pair> Parse(string line)
        {
            int index = 0;
            var pairs = new List<Pair>();
            Pair currentPair = null;

            while (index < line.Count())
            {
                if (line[index] == '[')
                {
                    var newPair = new Pair();
                    pairs.Add(newPair);
                    if (currentPair is not null)
                    {
                        newPair.Depth = currentPair.Depth + 1;
                        newPair.Parent = currentPair;
                        if (currentPair.Left.HasValue || currentPair.LeftPair != null)
                        {
                            currentPair.RightPair = newPair;
                        }
                        else
                        {
                            currentPair.LeftPair = newPair;
                        }
                    }
                    currentPair = newPair;
                }
                else if (line[index] == ']')
                {
                    currentPair = currentPair.Parent;
                }
                else if (char.IsDigit(line[index]))
                {
                    int offset = 0;
                    while (char.IsDigit(line[index+offset]))
                    {
                        offset++;
                    }
                    int num = int.Parse(line.Substring(index, offset));

                    if (currentPair.Left.HasValue || currentPair.LeftPair != null)
                    {
                        currentPair.Right = num;
                    }
                    else
                    {
                        currentPair.Left = num;
                    }
                    index += offset - 1;
                }
                index++;
            }

            //Reduce(pairs);
            return pairs.ToList();
        }

        public long Magnitude(Pair pair)
        {
            var leftValue = 3 * pair.Left ?? Magnitude(pair.LeftPair);
            var rightValue = 2 * pair.Right ?? Magnitude(pair.RightPair);
            return leftValue + rightValue;
        }

        private void Nest(Pair pair)
        {
            pair.Depth++;
            if (pair.LeftPair is Pair leftPair)
            {
                leftPair.Parent = pair;
                Nest(leftPair);
            }
            if (pair.RightPair is Pair rightPair)
            {
                rightPair.Parent = pair;
                Nest(rightPair);
            }
        }

        private void BuildList(Pair pair, ref List<Pair> pairs)
        {
            if (pair.LeftPair is Pair leftPair)
            {
                pairs.Add(leftPair);
                BuildList(leftPair, ref pairs);
            }
            if (pair.RightPair is Pair rightPair)
            {
                pairs.Add(rightPair);
                BuildList(rightPair, ref pairs);
            }
        }

        public string BuildString(Pair pair)
        {
            var leftString = pair.Left?.ToString() ?? BuildString(pair.LeftPair);
            var rightString = pair.Right?.ToString() ?? BuildString(pair.RightPair);
            return $"[{leftString},{rightString}]";
        }

        private void Reduce(ref List<Pair> pairs)
        {
            var reducing = true;
            while (reducing)
            {
                if (pairs.FirstOrDefault(p => p.Depth >= 4 && p.IsNumbers) is Pair explode)
                {
                    Explode(explode, true, explode.Left.Value);
                    Explode(explode, false, explode.Right.Value);
                    var parent = explode.Parent;

                    if (parent.LeftPair == explode)
                    {
                        parent.LeftPair = null;
                        parent.Left = 0;
                    }
                    else
                    {
                        parent.RightPair = null;
                        parent.Right = 0;
                    }
                    pairs.Remove(explode);
                }
                else if (pairs.FirstOrDefault(p => p.Left >= 10 || p.Right >= 10) is Pair split)
                {
                    var index = pairs.IndexOf(split);
                    if (split.Left >= 10)
                    {
                        int leftNum = (int)Math.Floor(split.Left.Value / 2.0);
                        int rightNum = (int)Math.Ceiling(split.Left.Value / 2.0);
                        split.Left = null;

                        var left = new Pair
                        {
                            Depth = split.Depth + 1,
                            Left = leftNum,
                            Right = rightNum,
                            Parent = split,
                        };
                        split.LeftPair = left;
                        pairs.Insert(index, left);
                    }
                    else if (split.Right >= 10)
                    {
                        int leftNum = (int)Math.Floor(split.Right.Value / 2.0);
                        int rightNum = (int)Math.Ceiling(split.Right.Value / 2.0);
                        split.Right = null;
                        var right = new Pair
                        {
                            Depth = split.Depth + 1,
                            Left = leftNum,
                            Right = rightNum,
                            Parent = split,
                        };
                        split.RightPair = right;
                        pairs.Insert(index + 1, right);
                    }
                }
                else
                {
                    reducing = false;
                }

                pairs = Parse(BuildString(pairs.First()));
            }
        }

        public int? Explode(Pair explode, bool left, int num)
        {
            if (explode.Parent == null)
            {
                return null;
            }
            if (left)
            {
                if (explode.Parent.Left.HasValue)
                {
                    explode.Parent.Left += num;
                    return explode.Parent.Left;
                }
                else if (explode.Parent.LeftPair is Pair leftPair && leftPair != explode)
                {
                    var pair = RightMost(leftPair);
                    pair.Right += num;
                    return pair.Right;
                }
                else
                {
                    return Explode(explode.Parent, left, num);
                }
            }
            else
            {

                if (explode.Parent.Right.HasValue)
                {
                    explode.Parent.Right += num;
                    return explode.Parent.Right;
                }
                else if (explode.Parent.RightPair is Pair rightPair && rightPair != explode)
                {
                    var pair = LeftMost(rightPair);
                    pair.Left += num;
                    return pair.Left;
                }
                else
                {
                    return Explode(explode.Parent, left, num);
                }
            }
        }

        public Pair RightMost(Pair from)
        {
            if (from.RightPair is Pair right)
            {
                return RightMost(right);
            }
            return from;
        }

        public Pair LeftMost(Pair from)
        {
            if (from.LeftPair is Pair left)
            {
                return LeftMost(left);
            }
            return from;
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

    public class Pair
    {
        public int? Left;
        public int? Right;
        public Pair? LeftPair;
        public Pair? RightPair;
        public Pair? Parent;
        public int Depth;

        public bool IsNumbers => Left.HasValue && Right.HasValue;

        public override string ToString()
        {
            return $"{Depth} - {Left},{Right}";
        }
    }
}
