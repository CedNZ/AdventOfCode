using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.GameConsole
{
    public class GameConsole_FixInfiniteLoops : GameConsole
    {
        public List<(string instruction, int hitCount)> BackupInstructions;
        public int AttemptedFixIndex;

        public GameConsole_FixInfiniteLoops(IEnumerable<string> inputs) : base(inputs)
        {
            Accumulator = 0;
            InstructionPointer = 0;
            Instructions = BuildInstructions(inputs);
            BackupInstructions = BuildInstructions(inputs);
            AttemptedFixIndex = Instructions.Count();
        }

        public override int Run()
        {
            var result = 0;
            do
            {
                result = ParseInstruction(Instructions);
            } while(result == 0);

            if (Looped)
            {
                Instructions = new List<(string instruction, int hitCount)>(BackupInstructions);

                for (int i = AttemptedFixIndex; i >= 0; i--)
                {
                    if 
                }
            }

            return result;
        }
    }
}
