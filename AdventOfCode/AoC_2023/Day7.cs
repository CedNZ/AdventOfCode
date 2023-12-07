using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day7 : IDay<Hand>
    {
        public long A(List<Hand> inputs)
        {
            return inputs.OrderDescending()
                .Select((x, i) => x.Bid * (i + 1))
                .Sum();
        }

        public long B(List<Hand> inputs)
        {
            throw new NotImplementedException();
        }

        public List<Hand> SetupInputs(string[] inputs)
        {
            return inputs.Select(l =>
            {
                var p = l.Split(' ');
                var bid = int.Parse(p[1]);
                var cards = p[0].Select(c => c switch
                {
                    'A' => Cards.A,
                    'K' => Cards.K,
                    'Q' => Cards.Q,
                    'J' => Cards.J,
                    'T' => Cards.T,
                    '9' => Cards.C9,
                    '8' => Cards.C8,
                    '7' => Cards.C7,
                    '6' => Cards.C6,
                    '5' => Cards.C5,
                    '4' => Cards.C4,
                    '3' => Cards.C3,
                    '2' => Cards.C2,
                    _ => throw new Exception("Invalid card")
                });
                return new Hand([.. cards], bid);
            }).ToList();
        }
    }

    public class Hand : IComparable<Hand>
    {
        public Cards[] Cards { get; init; } = [];
        public int Bid { get; init; }

        public Type Type { get; init; }

        public Hand(Cards[] cards, int bid)
        {
            Cards = cards;
            Bid = bid;

            var groups = cards.Order().GroupBy(c => c);
            var groupCount = groups.Count();
            if (groupCount == 1)
            {
                Type = Type.FiveOfAKind;
            }
            else if (groupCount == 2)
            {
                if (groups.Any(g => g.Count() == 4))
                {
                    Type = Type.FourOfAKind;
                }
                else
                {
                    Type = Type.FullHouse;
                }
            }
            else if (groupCount == 3)
            {
                if (groups.Any(g => g.Count() == 3))
                {
                    Type = Type.ThreeOfAKind;
                }
                else
                {
                    Type = Type.TwoPair;
                }
            }
            else if (groupCount == 4)
            {
                Type = Type.OnePair;
            }
            else
            {
                Type = Type.HighCard;
            }

        }

        public int CompareTo(Hand other)
        {
            if (Type == other.Type)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Cards[i] == other.Cards[i])
                    {
                        continue;
                    }
                    return Cards[i] - other.Cards[i];
                }
            }
            return Type - other.Type;
        }
    }

    public enum Cards
    {
        A,
        K,
        Q,
        J,
        T,
        C9,
        C8,
        C7,
        C6,
        C5,
        C4,
        C3,
        C2,
    }

    public enum Type
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard,
    }
}
