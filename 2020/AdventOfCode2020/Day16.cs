using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day16 : IDay<Ticket>
    {
        static Dictionary<string, Func<int, bool>> Rules;

        public List<Ticket> SetupInputs(string[] inputs)
        {
            Rules = new Dictionary<string, Func<int, bool>>();

            var inputGroups = inputs.GroupsUntilWhiteSpace().ToList();

            var rulesText = inputGroups[0];
            var myTicketText = inputGroups[1];
            var otherTicketsText = inputGroups[2];


            var rulesPattern = @"(\w+.+\w+): (\d+)-(\d+) or (\d+)-(\d+)";
            foreach(var ruleString in rulesText.Split('\n').Where(x => !string.IsNullOrEmpty(x)))
            {
                var match = Regex.Match(ruleString, rulesPattern);
                var name = match.Groups[1].Value;
                var lLower = int.Parse(match.Groups[2].Value);
                var lUpper = int.Parse(match.Groups[3].Value);
                var uLower = int.Parse(match.Groups[4].Value);
                var uUpper = int.Parse(match.Groups[5].Value);

                Func<int, bool> rule = x =>
                {
                    return x >= lLower && x <= lUpper
                        || x >= uLower && x <= uUpper;
                };

                Rules.Add(name, rule);
            }

            List<Ticket> tickets = new List<Ticket>();

            tickets.Add(new Ticket(myTicketText.Split('\n')[1]));

            foreach(var ticketText in otherTicketsText.Split('\n').Skip(1))
            {
                tickets.Add(new Ticket(ticketText));
            }


            return tickets;
        }

        public long A(List<Ticket> inputs)
        {
            List<int> invalidFields = new List<int>();

            foreach(var ticket in inputs)
            {
                foreach (var field in ticket.Fields.Values)
                {
                    if (!Rules.Any(r => r.Value(field)))
                    {
                        invalidFields.Add(field);
                    }
                }
            }

            return invalidFields.Sum();
        }

        public List<Ticket> RemoveInvalid(List<Ticket> tickets)
        {
            List<Ticket> validTickets = new List<Ticket>();

            foreach(var ticket in tickets)
            {
                if (ticket.Fields.Any(f => !Rules.Any(r => r.Value(f.Value))))
                {
                    continue;
                }
                validTickets.Add(ticket);
            }

            return validTickets;
        }

        public long B(List<Ticket> inputs)
        {
            Dictionary<string, int> ticketFields = new Dictionary<string, int>();
            Ticket myTicket = inputs.First();

            var tickets = RemoveInvalid(inputs.Skip(1).ToList());

            var fieldIndexOffset = 0;

            while (Rules.Any() && fieldIndexOffset < myTicket.Fields.Count())
            {
                var field = myTicket.Fields.Skip(fieldIndexOffset).First();

                var matchingRule = Rules.Where(r => tickets.All(t => r.Value(t.Fields[field.Key])));

                if(matchingRule.Count() == 1 && matchingRule.First().Value != null)
                {
                    ticketFields.Add(matchingRule.First().Key, field.Value);
                    Rules.Remove(matchingRule.First().Key);
                    myTicket.Fields.Remove(field.Key);
                    fieldIndexOffset = 0;
                }
                else
                {
                    fieldIndexOffset++;
                }
            }

            return ticketFields.Where(t => t.Key.Contains("departure")).Select(t => (long)t.Value).Aggregate((curr, next) => curr * next);
        }
    }

    public class Ticket
    {
        public Dictionary<string, int> Fields;

        public Ticket(string fields)
        {
            Fields = new Dictionary<string, int>();

            var nums = fields.Split(',').Select(int.Parse).ToList();

            char name = 'a';

            foreach(var num in nums)
            {
                Fields.Add(name.ToString(), num);
                name++;
            }
        }
    }
}
