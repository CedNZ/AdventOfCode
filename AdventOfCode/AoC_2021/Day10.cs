using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day10 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            Stack<char> stack;
            long points = 0;
            foreach (var line in inputs)
            {
                stack = new();
                bool exitLoop = false;
                foreach (var bracket in line)
                {
                    if (new[] {'(', '[', '{', '<' }.Contains(bracket))
                    {
                        stack.Push(bracket);
                    }
                    else
                    {
                        var open = stack.Pop();
                        switch (bracket)
                        {   
                            case ')':
                                if (open == '(')
                                {

                                }
                                else
                                {
                                    points += 3;
                                    exitLoop = true;
                                }
                                break;
                            case ']':
                                if (open == '[')
                                {

                                }
                                else
                                {
                                    points += 57;
                                    exitLoop = true;
                                }
                                break;
                            case '}':
                                if (open == '{')
                                {

                                }
                                else
                                {
                                    points += 1197;
                                    exitLoop = true;
                                }
                                break;
                            case '>':
                                if (open == '<')
                                {

                                }
                                else
                                {
                                    points += 25137;
                                    exitLoop = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (exitLoop)
                    {
                        break;
                    }
                }
            }


            return points;
        }

        public long B(List<string> inputs)
        {

            Stack<char> stack;
            List<long> points = new List<long>();
            foreach (var line in inputs)
            {
                stack = new();
                bool corrupted = false;
                foreach (var bracket in line)
                {
                    if (new[] { '(', '[', '{', '<' }.Contains(bracket))
                    {
                        stack.Push(bracket);
                    }
                    else
                    {
                        var open = stack.Pop();
                        switch (bracket)
                        {
                            case ')':
                                if (open == '(')
                                {

                                }
                                else
                                {
                                    corrupted = true;
                                }
                                break;
                            case ']':
                                if (open == '[')
                                {

                                }
                                else
                                {
                                    corrupted = true;
                                }
                                break;
                            case '}':
                                if (open == '{')
                                {

                                }
                                else
                                {
                                    corrupted = true;
                                }
                                break;
                            case '>':
                                if (open == '<')
                                {

                                }
                                else
                                {
                                    corrupted = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (corrupted)
                    {
                        break;
                    }
                }
                if (!corrupted)
                {
                    long point = 0;
                    foreach (var open in stack)
                    {
                        point *= 5;
                        switch (open)
                        {
                            case '(':
                                point += 1;
                                break;
                            case '[':
                                point += 2;
                                break;
                            case '{':
                                point += 3;
                                break;
                            case '<':
                                point += 4;
                                break;
                            default:
                                break;
                        }
                    }
                    points.Add(point);
                }
            }

            points.Sort();

            return points[points.Count()/2];
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
