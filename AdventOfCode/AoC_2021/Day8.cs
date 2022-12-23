using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day8 : IDay<(List<string>, List<string>)>
    {
        public long A(List<(List<string>, List<string>)> inputs)
        {
            return inputs.Sum(x => x.Item2.Count(y =>
                y.Length == 2 //1
                || y.Length == 3 //7
                || y.Length == 4 //4 
                || y.Length == 7 //8
                ));
        }

        public long B(List<(List<string>, List<string>)> inputs)
        {
            List<(string, int)> segmentMap;
            long sum = 0;
            foreach (var item in inputs)
            {
                segmentMap = new ();

                var input = item.Item1;
                var outputs = item.Item2;

                var inputGroups = input.GroupBy(x => x);

                foreach (var group in inputGroups)
                {
                    if (group.Key.Length == 2) //1
                    {
                        segmentMap.Add((group.Key, 1));
                    }
                    if (group.Key.Length == 3) //7
                    {
                        segmentMap.Add((group.Key, 7));
                    }
                    if (group.Key.Length == 4) //4
                    {
                        segmentMap.Add((group.Key, 4));
                    }
                    if (group.Key.Length == 7) //8
                    {
                        segmentMap.Add((group.Key, 8));
                    }
                }

                var cf = segmentMap.Single(x => x.Item2 == 1).Item1;
                var a = segmentMap.Single(x => x.Item2 == 7).Item1.Except(cf);
                var bd = segmentMap.Single(x => x.Item2 == 4).Item1.Except(cf);
                var eg = segmentMap.Single(x => x.Item2 == 8).Item1.Except(cf).Except(bd).Except(a);



                foreach (var group in inputGroups.Where(g => !segmentMap.Any(x => x.Item1 == g.Key)))
                {
                    if (group.Key.Length == 5) //2,3,5
                    {
                        if (eg.All(s => group.Key.Contains(s))) //2
                        {
                            segmentMap.Add((group.Key, 2));
                        }
                        else if (bd.All(s => group.Key.Contains(s))) //5
                        {
                            segmentMap.Add((group.Key, 5));
                        }
                        else //3
                        {
                            segmentMap.Add((group.Key, 3));
                        }
                    }

                    if (group.Key.Length == 6) //0,6,9
                    {
                        if (eg.All(s => group.Key.Contains(s))) //0,6
                        {
                            if (bd.All(s => group.Key.Contains(s))) //6
                            {
                                segmentMap.Add((group.Key, 6));
                            }
                            else //0
                            {
                                segmentMap.Add((group.Key, 0));
                            }
                        }
                        else //9
                        {
                            segmentMap.Add((group.Key, 9));
                        }
                    }

                    if (segmentMap.Count() == 10)
                    {
                        break;
                    }
                }

                var num = 0;
                foreach (var output in outputs)
                {
                    num += segmentMap.Single(s => s.Item1.Length == output.Length && !s.Item1.Except(output).Any()).Item2;
                    num *= 10;
                }
                num /= 10;

                sum += num;
            }
            return sum;
        }

        public List<(List<string>, List<string>)> SetupInputs(string[] inputs)
        {
            var split = inputs.Select(x => x.Split('|', StringSplitOptions.RemoveEmptyEntries));
            return split.Select(x => (x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(), 
                x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())).ToList();
        }
    }
}
