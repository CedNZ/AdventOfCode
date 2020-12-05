using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day5 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        public long A(List<string> inputs)
        {
            long maxId = 0;
            foreach(var input in inputs)
            {
                var row = binaryPartition(input.Substring(0, 7), 0, 127);
                var column = binaryPartition(input.Substring(7), 0, 7);

                var seatId = (row * 8) + column;

                if (seatId > maxId)
                {
                    maxId = seatId;
                }
            }
            return maxId;
        }

        public int binaryPartition(string seat, int low, int high)
        {
            var delta = (high - low) / 2;
             if (seat == "F" || seat == "L")
            {
                return low;
            }
            if (seat == "B" || seat == "R")
            {
                return high;
            }
            if (seat[0] == 'F' || seat[0] == 'L')
            {
                return binaryPartition(seat.Substring(1), low, high - delta - 1);
            }
            return binaryPartition(seat.Substring(1), low + delta + 1, high);

        }

        public long B(List<string> inputs)
        {
            throw new NotImplementedException();
        }        
    }
}
