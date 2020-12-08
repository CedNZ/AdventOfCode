using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.GameConsole;

namespace AdventOfCode2020
{
    public class Day8 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }

        public long A(List<string> inputs)
        {
            var gameConsole = new GameConsole.GameConsole(inputs);

            return gameConsole.Run();
        }

        public long B(List<string> inputs)
        {
            var gameConsole = new GameConsole.GameConsole(inputs);

            return gameConsole.Run(true);
        }
    }
}
