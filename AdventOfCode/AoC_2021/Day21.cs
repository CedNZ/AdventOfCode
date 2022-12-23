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

            Dictionary<Game, long> games = new ();

            games.Add(new Game(_player1Position, _player2Position), 1);

            var player1Wins = 0L;
            var player2Wins = 0L;

            while (games.Any())
            {
                games = RollQuantumDice(games, ref player1Wins, ref player2Wins);
            }

            return Math.Max(player1Wins, player2Wins);
        }


        //dictionary: Score, (Universes, Position)
        public Dictionary<Game, long> RollQuantumDice(Dictionary<Game, long> games, ref long playerOneWins, ref long playerTwoWins)
        {
            Dictionary<Game, long> newGames = new();
            foreach (var game in games)
            {
                foreach (var roll in GetRolls(game.Value, game.Key.PlayerOnePosition))
                {
                    var score = game.Key.PlayerOneScore;
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

                    if (score >= 21)
                    {
                        playerOneWins += roll.universeCount;
                        continue;
                    }

                    foreach (var player2Roll in GetRolls(roll.universeCount, game.Key.PlayerTwoPosition))
                    {
                        var score2 = game.Key.PlayerTwoScore;
                        var newPosition2 = player2Roll.position;
                        if (newPosition2 > 10)
                        {
                            newPosition2 %= 10;
                            if (newPosition2 == 0)
                            {
                                newPosition2 = 10;
                            }
                        }

                        score2 += newPosition2;

                        if (score2 >= 21)
                        {
                            playerTwoWins += player2Roll.universeCount;
                            continue;
                        }

                        var newGame = game.Key with { PlayerOnePosition = newPosition, PlayerOneScore = score, PlayerTwoPosition = newPosition2, PlayerTwoScore = score2 };

                        if (newGames.ContainsKey(newGame))
                        {
                            newGames[newGame] += player2Roll.universeCount;
                        }
                        else
                        {
                            newGames.Add(newGame, player2Roll.universeCount);
                        }
                    }
                }
            }

            return newGames;
        }

        public IEnumerable<(long universeCount, int position)> GetRolls(long universeCount, int position)
        {
            yield return (universeCount * 1, position + 3);
            yield return (universeCount * 3, position + 4);
            yield return (universeCount * 6, position + 5);
            yield return (universeCount * 7, position + 6);
            yield return (universeCount * 6, position + 7);
            yield return (universeCount * 3, position + 8);
            yield return (universeCount * 1, position + 9);
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

    public readonly struct Game
    {
        public Game(int pos1, int pos2)
        {
            PlayerOnePosition = pos1;
            PlayerTwoPosition = pos2;
            PlayerOneScore = 0;
            PlayerTwoScore = 0;
        }

        public int PlayerOneScore { get; init; }
        public int PlayerTwoScore { get; init; }
        public int PlayerOnePosition { get; init; }
        public int PlayerTwoPosition { get; init; }
    }
}
