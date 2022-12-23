using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.GameConsole
{
    public class GameConsole_FixInfiniteLoops : GameConsole
    {
        public List<Instruction> BackupInstructions;
        public int AttemptedFixIndex;

        public GameConsole_FixInfiniteLoops(IEnumerable<Instruction> instructions) : base(instructions)
        {
            Instructions = instructions.ToList().Clone().ToList();
            BackupInstructions = instructions.ToList().Clone().ToList();
            AttemptedFixIndex = Instructions.Count();
            Init();
        }

        public override int Run()
        {
            bool solved = false;
            int result;
            do
            {
                do
                {
                    result = HandleInstruction();
                } while(result == 0);

                if(Looped)
                {
                    Instructions = BackupInstructions.Clone().ToList();

                    for(int i = AttemptedFixIndex - 1; i >= 0; i--)
                    {
                        if(Instructions[i].Op == Op.jmp || Instructions[i].Op == Op.nop)
                        {
                            AttemptedFixIndex = i;

                            Looped = false;
                            if(Instructions[i].Op == Op.nop)
                            {
                                Instructions[i].Op = Op.jmp;
                            }
                            else
                            {
                                Instructions[i].Op = Op.nop;
                            }

                            Init();

                            break;
                        }
                    }
                }
                else
                {
                    solved = true;
                }
            } while(!solved);

            return result;
        }
    }
}
