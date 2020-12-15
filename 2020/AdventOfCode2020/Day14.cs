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
        char[] mask;
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
                    mask = parts[1].ToCharArray();
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
            Dictionary<long, long> memoryDict = new Dictionary<long, long>();

            foreach(var instruction in inputs)
            {
                var parts = instruction.Split(" = ");
                if(parts[0].StartsWith("mask"))
                {
                    mask = parts[1].ToCharArray();
                }
                else if(parts[0].StartsWith("mem"))
                {
                    var address = long.Parse(Regex.Match(parts[0], @"mem\[(\d+)\].*").Groups[1].Value);
                    var value = long.Parse(parts[1]);

                    foreach(var possibleAddress in PossibleAddresses(address))
                    {
                        memoryDict[possibleAddress] = value;
                    }
                }
            }

            return memoryDict.Sum(kvp => kvp.Value);
        }

        public IEnumerable<long> PossibleAddresses(long address)
        {
            var value = Convert.ToString(address, 2).PadLeft(bitCount, '0').ToCharArray();


            for(int i = 0; i < bitCount; i++)
            {
                if(mask[i] != '0')
                {
                    value[i] = mask[i];
                }
            }

            var floatingCount = mask.Count(x => x == 'X');

            for(int i = 0; i < Math.Pow(2, floatingCount); i++)
            {
                var swapAddressString = Convert.ToString(i, 2).PadLeft(floatingCount, '0').ToCharArray();

                var returnAddress = (char[])value.Clone();

                foreach(var swap in value.Where(x => x == 'X')
                                        .Select((x, i) => (x, i)))
                {
                    var index = Array.IndexOf(returnAddress, swap.x);
                    returnAddress[index] = swapAddressString[swap.i];
                }

                yield return Convert.ToInt64(new string(returnAddress), 2);
            }
        }
    }
}
