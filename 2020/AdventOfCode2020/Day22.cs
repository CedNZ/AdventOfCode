using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day22 : IDay<List<int>>
    {
        public List<List<int>> SetupInputs(string[] inputs)
        {
            List<List<int>> output = new List<List<int>>();
            List<int> player = new List<int>();
            foreach(var item in inputs)
            {
                if (int.TryParse(item, out var num))
                {
                    player.Add(num);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(item))
                {
                    output.Add(player);
                    player = new List<int>();
                }
            }
            output.Add(player);
            return output;
        }

        public long A(List<List<int>> inputs)
        {
            Queue<int> player1 = new Queue<int>(inputs[0]);
            Queue<int> player2 = new Queue<int>(inputs[1]);

            while (player1.Count() > 0 && player2.Count() > 0)
            {
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }

            var winningPlayer = player1.Count() > 0 ? player1 : player2;

            var multiplier = winningPlayer.Count() + 1;

            return winningPlayer.Sum(x =>
            {
                multiplier--;
                return x * multiplier;
            });
        }

        public long B(List<List<int>> inputs)
        {
            Depth = 0;
            Queue<int> player1 = new Queue<int>(inputs[0]);
            Queue<int> player2 = new Queue<int>(inputs[1]);

            var player1Won = PlayRecursive(player1, player2);

            var winningPlayer = player1Won ? player1 : player2;

            var multiplier = winningPlayer.Count() + 1;

            return winningPlayer.Sum(x =>
            {
                multiplier--;
                return x * multiplier;
            });
        }

        public static int Depth;
        public static int TotalGames;

        public bool PlayRecursive(Queue<int> player1, Queue<int> player2)
        {
            int gamesThisRound = 0;
            System.Diagnostics.Debug.WriteLine($"Game: {Depth++}, Round: {gamesThisRound}, Total: {TotalGames}");
            List<(int[], int[])> seenCards = new List<(int[], int[])>();
            while(player1.Count() > 0 && player2.Count() > 0)
            {
                if (seenCards.Any( x => x.Item1.SequenceEqual(player1) && x.Item2.SequenceEqual(player2)))
                {
                    return true;
                }
                seenCards.Add((player1.ToArray(), player2.ToArray()));

                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                if(card1 <= player1.Count() && card2 <= player2.Count())
                {
                    var player1Won = PlayRecursive(new Queue<int>(player1.Take(card1)), new Queue<int>(player2.Take(card2)));
                    System.Diagnostics.Debug.WriteLine($"Game: {Depth--}, Round: {gamesThisRound}, Total: {TotalGames}");

                    if(player1Won)
                    {
                        player1.Enqueue(card1);
                        player1.Enqueue(card2);
                    }
                    else
                    {
                        player2.Enqueue(card2);
                        player2.Enqueue(card1);
                    }
                }
                else
                {
                    if(card1 > card2)
                    {
                        player1.Enqueue(card1);
                        player1.Enqueue(card2);
                    }
                    else
                    {
                        player2.Enqueue(card2);
                        player2.Enqueue(card1);
                    }
                }
                gamesThisRound++;
                TotalGames++;
            }
            return player1.Count() > player2.Count();
        }
    }
}
