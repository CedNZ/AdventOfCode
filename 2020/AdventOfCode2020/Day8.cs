using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.GameConsole;

namespace AdventOfCode2020
{
    public class Day8 : IDay<Instruction>
    {
        public List<Instruction> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => new Instruction(x)).ToList();
        }

        public long A(List<Instruction> inputs)
        {
            var gameConsole = new GameConsole.GameConsole(inputs);

            return gameConsole.Run();
        }

        public long B(List<Instruction> inputs)
        {
            var gameConsole = new GameConsole.GameConsole_FixInfiniteLoops(inputs);

            return gameConsole.Run();
        }
    }
}
