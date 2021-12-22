using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day21 : IDay<string>
    {
        int _player1Score;
        int _player1Position;
        int _player2Score;
        int _player2Position;

        public long A(List<string> inputs)
        {
            _player1Position = int.Parse(inputs.First().Last().ToString());
            _player2Position = int.Parse(inputs.Last().Last().ToString());

            _rolls = 0;
            _dice = 1;
            bool player1Turn = true;

            while (_player1Score < 1000 && _player2Score < 1000)
            {
                var steps = GetRoll();
                if (player1Turn)
                {
                    _player1Position += steps;
                    if (_player1Position > 10)
                    {
                        _player1Position %= 10;
                        if (_player1Position == 0)
                        {
                            _player1Position = 10;
                        }
                    }
                    _player1Score += _player1Position;
                }
                else
                {
                    _player2Position += steps;
                    if (_player2Position > 10)
                    {
                        _player2Position %= 10;
                        if (_player2Position == 0)
                        {
                            _player2Position = 10;
                        }
                    }
                    _player2Score += _player2Position;

                }
                player1Turn = !player1Turn;
            }

            return _rolls * Math.Min(_player1Score, _player2Score);
        }

        int _dice;
        int _rolls;
        public int GetRoll()
        {
            int value = 0;
            for (int i = 0; i < 3; i++)
            {
                value += _dice;
                _rolls++;
                _dice++;
                if (_dice > 100)
                {
                    _dice %= 100;
                }
            }
            return value;
        }

        public long B(List<string> inputs)
        {
            _player1Position = int.Parse(inputs.First().Last().ToString());
            _player2Position = int.Parse(inputs.Last().Last().ToString());

            Dictionary<int, (long universeCount, int position)> player1 = new ();
            Dictionary<int, (long universeCount, int position)> player2 = new ();

            player1.Add(0, (1, _player1Position));
            player2.Add(0, (1, _player2Position));

            var player1Wins = 0L;
            var player2Wins = 0L;

            bool player1Turn = true;

            while (player1.Any() || player2.Any())
            {
                if (player1Turn)
                {
                    player1 = RollQuantumDice(player1);
                    foreach (var item in player1)
                    {
                        if (item.Key >= 21)
                        {
                            player1Wins += item.Value.universeCount;
                        }
                    }
                }
                else
                {
                    player2 = RollQuantumDice(player2);
                    foreach (var item in player2)
                    {
                        if (item.Key >= 21)
                        {
                            player2Wins += item.Value.universeCount;
                        }
                    }
                }

                player1Turn = !player1Turn;
            }

            return Math.Max(player1Wins, player2Wins);
        }


        //dictionary: Score, (Universes, Position)
        public Dictionary<int, (long universeCount, int position)> RollQuantumDice(Dictionary<int, (long universeCount, int position)> player)
        {
            Dictionary<int, (long universeCount, int position)> newPositions = new();
            foreach (var item in player)
            {
                if (item.Key >= 21)
                {
                    continue;
                }
                foreach (var roll in GetRolls(item.Value))
                {
                    var score = item.Key;
                    var newPosition = roll.position;
                    if (newPosition > 10)
                    {
                        newPosition %= 10;
                        if (newPosition == 0)
                        {
                            newPosition = 10;
                        }
                    }

                    score += newPosition;

                    if (newPositions.ContainsKey(score))
                    {
                        var value = newPositions[score];
                        newPositions[score] = (value.universeCount * roll.universeCount, roll.position);
                    }
                    else
                    {
                        newPositions.Add(score, (roll.universeCount, newPosition));
                    }
                }
            }

            return newPositions;
        }

        public IEnumerable<(long universeCount, int position)> GetRolls((long universeCount, int position) playerPostion)
        {
            yield return (playerPostion.universeCount * 1, playerPostion.position + 3);
            yield return (playerPostion.universeCount * 3, playerPostion.position + 4);
            yield return (playerPostion.universeCount * 6, playerPostion.position + 5);
            yield return (playerPostion.universeCount * 7, playerPostion.position + 6);
            yield return (playerPostion.universeCount * 6, playerPostion.position + 7);
            yield return (playerPostion.universeCount * 3, playerPostion.position + 8);
            yield return (playerPostion.universeCount * 1, playerPostion.position + 9);
        }

        /*********************
         Steps -> Count
         3  ->  1
         4  ->  3
         5  ->  6
         6  ->  7
         7  ->  6
         8  ->  3
         9  ->  1
         *********************/

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
