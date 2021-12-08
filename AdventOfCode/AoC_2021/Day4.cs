using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day4 : IDay<int>
    {
        List<BingoBoard> bingoBoards = new();

        public long A(List<int> inputs)
        {
            foreach (var number in inputs)
            {
                foreach (var board in bingoBoards)
                {
                    if (board.CheckNumber(number))
                    {
                        return board.GetScore(number);
                    }
                }
            }

            return default;
        }

        public long B(List<int> inputs)
        {
            return default;
        }

        public List<int> SetupInputs(string[] inputs)
        {
            var ints = inputs.First();

            for (int i = 2; i < inputs.Length; i += 6)
            {
                bingoBoards.Add(new BingoBoard(inputs.Skip(i).Take(5).ToArray()));
            }

            return ints.Split(',').Select(int.Parse).ToList();
        }
    }

    class BingoBoard
    {
        int[] board;
        Dictionary<int, int> seenNumbers;

        public BingoBoard(string[] lines)
        {
            seenNumbers = new();

            board = new int[25];

            var line = lines.First().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < board.Length; i++)
            {             
                if (i > 0 && i % 5 == 0)
                {
                    line = lines[i / 5].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
                board[i] = int.Parse(line[i % 5]);
   
            }
        }

        public int SeenNumberCount => seenNumbers.Count();

        List<List<int>> WinningLineIndex = new List<List<int>>
        {
            //rows
            new List<int> {  0,  1,   2,   3,   4 },
            new List<int> {  5,  6,   7,   8,   9 },
            new List<int> { 10, 11,  12,  13,  14 },
            new List<int> { 15, 16,  17,  18,  19 },
            new List<int> { 20, 21,  22,  23,  24 },
            //columns
            new List<int> {  0,  5,  10,  15,  20 },
            new List<int> {  1,  6,  11,  16,  21 },
            new List<int> {  2,  7,  12,  17,  22 },
            new List<int> {  3,  8,  13,  18,  23 },
            new List<int> {  4,  9,  14,  19,  24 },
            //diags
            new List<int> {  0,  6,  12,  18,  24 },
            new List<int> {  4,  8,  12,  16,  20 },
        };

        public bool CheckNumber(int num)
        {
            if (board.Contains(num))
            {
                seenNumbers.Add(num, Array.IndexOf(board, num));
            }

            if (SeenNumberCount >= 5)
            {
                var seenIndexes = seenNumbers.Values.ToList();

                return WinningLineIndex.Any(x => x.All(y => seenIndexes.Contains(y)));
            }
            return false;
        }

        public long GetScore(int drawnNumber)
        {
            var unseen = board.Except(seenNumbers.Keys);
            return unseen.Sum() * drawnNumber;
        }
    }
}
