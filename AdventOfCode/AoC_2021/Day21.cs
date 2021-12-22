﻿using System;
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
            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
