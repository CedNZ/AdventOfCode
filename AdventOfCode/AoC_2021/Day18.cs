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

            return Magnitude(currentPairs.First());
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
            var leftValue = 3 * (pair.Left ?? Magnitude(pair.LeftPair));
            var rightValue = 2 * (pair.Right ?? Magnitude(pair.RightPair));
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
                //System.Diagnostics.Debug.WriteLine(BuildString(pairs.First()));
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
                        if (LeftMost(split.LeftPair) is Pair leftPair && (leftPair.Left >= 10 || leftPair.Right >= 10))
                        {
                            if (leftPair.Left >= 10)
                            {
                                int leftNum = (int)Math.Floor(leftPair.Left.Value / 2.0);
                                int rightNum = (int)Math.Ceiling(leftPair.Left.Value / 2.0);
                                leftPair.Left = null;

                                var left = new Pair
                                {
                                    Depth = leftPair.Depth + 1,
                                    Left = leftNum,
                                    Right = rightNum,
                                    Parent = leftPair,
                                };
                                leftPair.LeftPair = left;
                                pairs.Insert(index, left);
                            }
                            else
                            {
                                int leftNum = (int)Math.Floor(leftPair.Right.Value / 2.0);
                                int rightNum = (int)Math.Ceiling(leftPair.Right.Value / 2.0);
                                leftPair.Right = null;

                                var right = new Pair
                                {
                                    Depth = leftPair.Depth + 1,
                                    Left = leftNum,
                                    Right = rightNum,
                                    Parent = leftPair,
                                };
                                leftPair.RightPair = right;
                                pairs.Insert(index, right);
                            }
                        }
                        else
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
            if (from?.RightPair is Pair right)
            {
                return RightMost(right);
            }
            return from;
        }

        public Pair LeftMost(Pair from)
        {
            if (from?.LeftPair is Pair left)
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

/*
[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]
+ [7,[5,[[3, 8],[1, 4]]]]
= [[[[7, 7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]

[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,8],[0,8]],[[8,9],[9,0]]]]

[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[0,[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,0],[[14,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[0,[14,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,0]],[[[15,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,15]],[[0,[15,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,15]],[[15,0],[[15,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,15]],[[15,15],[0,[15,7]]]],[7,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,15]],[[15,15],[15,0]]],[14,[5,[[3,8],[1,4]]]]]
[[[[7,14],[14,15]],[[15,15],[15,0]]],[14,[8,[0,[9,4]]]]]
[[[[7,14],[14,15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[7,[7,7]],[14,15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[14,0],[21,15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[[7,7],0],[21,15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[21,15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[[10,11],15]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,17],[0,26]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,[8,9]],[0,26]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,0],[9,26]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,0],[9,[13,13]]],[[15,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,0],[22,0]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,0],[[11,11],0]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,11],[0,11]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[8,[5,6]],[0,11]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[13,0],[6,11]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[[6,7],0],[6,11]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[6,11]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[6,[5,6]]],[[28,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[11,0]],[[34,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,7],[[5,6],0]],[[34,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,12],[0,6]],[[34,15],[15,0]]],[14,[8,[9,0]]]]
[[[[0,[6,6]],[0,6]],[[34,15],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[6,6]],[[34,15],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[6,6]],[[[17,17],15],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[6,23]],[[0,32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[6,[11,12]]],[[0,32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[17,0]],[[12,32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,0],[[8,9],0]],[[12,32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[0,9]],[[12,32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[0,9]],[[[6,6],32],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[0,15]],[[0,38],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[0,[7,8]]],[[0,38],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,0]],[[8,38],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,0]],[[8,[19,19]],[15,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,0]],[[27,0],[34,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,0]],[[[13,14],0],[34,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,13]],[[0,14],[34,0]]],[14,[8,[9,0]]]]
[[[[6,8],[7,[6,7]]],[[0,14],[34,0]]],[14,[8,[9,0]]]]
[[[[6,8],[13,0]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[6,8],[[6,7],0]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[6,14],[0,7]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[6,[7,7]],[0,7]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[13,0],[7,7]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[[6,7],0],[7,7]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,7]],[[7,14],[34,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,7]],[[7,[7,7]],[34,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,7]],[[14,0],[41,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,7]],[[[7,7],0],[41,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,14]],[[0,7],[41,0]]],[14,[8,[9,0]]]]
[[[[0,7],[7,[7,7]]],[[0,7],[41,0]]],[14,[8,[9,0]]]]
[[[[0,7],[14,0]],[[7,7],[41,0]]],[14,[8,[9,0]]]]
[[[[0,7],[[7,7],0]],[[7,7],[41,0]]],[14,[8,[9,0]]]]
[[[[0,14],[0,7]],[[7,7],[41,0]]],[14,[8,[9,0]]]]
[[[[0,[7,7]],[0,7]],[[7,7],[41,0]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[7,7],[41,0]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[7,7],[[20,21],0]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[7,27],[0,21]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[7,[13,14]],[0,21]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[20,0],[14,21]]],[14,[8,[9,0]]]]
[[[[7,0],[7,7]],[[[10,10],0],[14,21]]],[14,[8,[9,0]]]]
[[[[7,0],[7,17]],[[0,10],[14,21]]],[14,[8,[9,0]]]]
[[[[7,0],[7,[8,9]]],[[0,10],[14,21]]],[14,[8,[9,0]]]]
[[[[7,0],[15,0]],[[9,10],[14,21]]],[14,[8,[9,0]]]]
[[[[7,0],[[7,8],0]],[[9,10],[14,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,8]],[[9,10],[14,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,8]],[[9,[5,5]],[14,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,8]],[[14,0],[19,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,8]],[[[7,7],0],[19,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,15]],[[0,7],[19,21]]],[14,[8,[9,0]]]]
[[[[7,7],[0,[7,8]]],[[0,7],[19,21]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[8,7],[19,21]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[8,7],[[9,10],21]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[8,16],[0,31]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[8,[8,8]],[0,31]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[16,0],[8,31]]],[14,[8,[9,0]]]]
[[[[7,7],[7,0]],[[[8,8],0],[8,31]]],[14,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,8],[8,31]]],[14,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,8],[8,[15,16]]]],[14,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,8],[23,0]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,8],[[11,12],0]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,19],[0,12]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[0,[9,10]],[0,12]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,0],[10,12]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,0],[[5,5],12]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[0,17]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[0,[8,9]]]],[30,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[39,[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[19,20],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[9,10],20],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[9,10],[10,10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[9,[5,5]],[10,10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[14,0],[15,10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,0]]],[[[[7,7],0],[15,10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[0,7],[15,10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[0,7],[[7,8],10]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[0,14],[0,18]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[0,[7,7]],[0,18]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,0],[7,18]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,0],[7,[9,9]]],[8,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,0],[16,0]],[17,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,0],[[8,8],0]],[17,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,8],[0,8]],[17,[9,0]]]]
[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,8],[0,8]],[[8,9],[9,0]]]]

/**/