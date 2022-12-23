using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day11 : IDay<Monkey>
    {
        long IDayOut<Monkey, long>.A(List<Monkey> inputs)
        {
            var monkeyItemTouchCount = new int[inputs.Count()];
            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in inputs)
                {
                    while (monkey.Items.Count() > 0)
                    {
                        monkeyItemTouchCount[monkey.Id]++;
                        var item = monkey.Items.Dequeue();
                        item = monkey.Func(item);
                        item /= 3;
                        
                        var toMonkeyId = item % monkey.Divisor == 0 ? monkey.TrueMonkeyId : monkey.FalseMonkeyId;
                        var toMonkey = inputs.Find(m => m.Id == toMonkeyId);
                        toMonkey.Items.Enqueue(item);
                    }
                }
            }

            return monkeyItemTouchCount.OrderDescending().Take(2).Aggregate((agg, next) => agg * next);
        }

        long IDayOut<Monkey, long>.B(List<Monkey> inputs)
        {
            var lcm = inputs.Select(m => m.Divisor).Aggregate((agg, next) => agg * next);

            var monkeyItemTouchCount = new long[inputs.Count()];
            for (int i = 0; i < 10_000; i++)
            {
                foreach (var monkey in inputs)
                {
                    while (monkey.Items.Count > 0)
                    {
                        monkeyItemTouchCount[monkey.Id]++;
                        var item = monkey.Items.Dequeue();
                        item = monkey.Func(item);
                        item %= lcm;

                        var toMonkeyId = item % monkey.Divisor == 0 ? monkey.TrueMonkeyId : monkey.FalseMonkeyId;
                        var toMonkey = inputs.Find(m => m.Id == toMonkeyId);
                        toMonkey.Items.Enqueue(item);
                    }
                }
            }

            return monkeyItemTouchCount.OrderDescending().Take(2).Aggregate((agg, next) => agg * next);
        }

        static int Gfc(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static int Lcm(int a, int b)
        {
            return (a / Gfc(a, b)) * b;
        }

        List<Monkey> IDayOut<Monkey, long>.SetupInputs(string[] inputs)
        {
            List<Monkey> monkeys = new List<Monkey>();
            foreach (var lines in inputs.Chunk(7))
            {
                var monkey = new Monkey();
                var str = Regex.Replace(lines[0], "\\D+", "");
                monkey.Id = int.Parse(str);

                var itemsString = lines[1].Split(':')[1];
                foreach (var item in itemsString.Split(',').Select(int.Parse))
                {
                    monkey.Items.Enqueue(item);
                }

                var operationString = lines[2].Split('=')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int? opArg = int.TryParse(operationString[2], out var op) ? op : null;

                monkey.Func = operationString[1] switch
                {
                    "+" => x => x + (opArg ?? x),
                    "*" => x => x * (opArg ?? x),
                    "-" => x => x - (opArg ?? x),
                    "/" => x => x / (opArg ?? x),
                    _ => throw new NotImplementedException(),
                };

                str = Regex.Replace(lines[3], "\\D+", "");
                monkey.Divisor = int.Parse(str);

                str = Regex.Replace(lines[4], "\\D+", "");
                monkey.TrueMonkeyId = int.Parse(str);

                str = Regex.Replace(lines[5], "\\D+", "");
                monkey.FalseMonkeyId = int.Parse(str);


                monkeys.Add(monkey);
            }
            return monkeys;
        }
    }

    internal class Monkey
    {
        public int Id { get; set; }
        public Queue<long> Items { get; set; } = new();
        public Func<long, long> Func { get; set; }
        public long Divisor { get; set; }
        public int TrueMonkeyId { get; set; }
        public int FalseMonkeyId { get; set; }

        public override string ToString() => $"{Id}: {string.Join(", ", Items.Select(i => i.ToString()))}";
    }
}