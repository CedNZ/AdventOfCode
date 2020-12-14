using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14 : IDay<string>
    {
        long[] memory;
        string mask;
        int bitCount = 36;

        public List<string> SetupInputs(string[] inputs)
        {
            var maxAddress = inputs
                .Select(x =>
                {
                    int y;
                    int.TryParse(Regex.Match(x, @"mem\[(\d+)\].*").Groups[1].Value, out y);
                    return y;
                })
                .OrderByDescending(y => y).First();

            memory = new long[maxAddress + 1];

            return inputs.ToList();
        }        

        public long A(List<string> inputs)
        {
            foreach(var instruction in inputs)
            {
                var parts = instruction.Split(" = ");
                if (parts[0].StartsWith("mask"))
                {
                    mask = parts[1];
                } 
                else if (parts[0].StartsWith("mem"))
                {
                    var address = long.Parse(Regex.Match(parts[0], @"mem\[(\d+)\].*").Groups[1].Value);
                    var value = Convert.ToString(long.Parse(parts[1]), 2).PadLeft(bitCount, '0').ToCharArray();

                    for (int i = 0; i < bitCount; i++)
                    {
                        if (mask[i] != 'X')
                        {
                            value[i] = mask[i];
                        }
                    }

                    memory[address] = Convert.ToInt64(new string(value), 2);
                }
            }

            return memory.Sum();
        }

        public long B(List<string> inputs)
        {
            return -1;
        }
    }
}
