using System.Collections.Frozen;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day4 : IDay<Ticket>
    {
        public long A(List<Ticket> inputs)
        {
            return inputs.Select(x => (long)Math.Pow(2, x.Numbers.Count(n => x.Chosen.Contains(n)) - 1)).Sum();
        }

        public long B(List<Ticket> inputs)
        {
            FrozenDictionary<int, int> scores = inputs.ToFrozenDictionary(x => x.Id, x => x.Numbers.Count(n => x.Chosen.Contains(n)));
            int[] tickets = Enumerable.Repeat(1, inputs.Count).ToArray();
            for (int i = 0; i < inputs.Count; i++)
            {
                var item = inputs[i];
                var score = scores[item.Id];
                for (int j = 1; j <= score; j++)
                {
                    tickets[i + j] += tickets[i];
                }
            }

            return tickets.Sum();
        }

        public List<Ticket> SetupInputs(string[] inputs)
        {
            return inputs.Select((x, i) =>
            {
                var parts = x.Split(new char[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries);
                return new Ticket
                {
                    Id = i + 1,
                    Numbers = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                    Chosen = parts[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                };
            }).ToList();
        }
    }

    public class Ticket
    {
        public int Id { get; set; }
        public List<int> Numbers { get; set; } = [];
        public List<int> Chosen { get; set; } = [];
    }
}
