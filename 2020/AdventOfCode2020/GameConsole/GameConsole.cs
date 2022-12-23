using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.GameConsole
{
    public class GameConsole
    {
        public List<Instruction> Instructions;
        private List<int> _visitedInstructions;

        public int Accumulator;
        public bool Looped;

        public int InstructionPointer;

        public GameConsole(IEnumerable<Instruction> instructions)
        {
            Instructions = new List<Instruction>(instructions);
            _visitedInstructions = new List<int>();
            Init();
        }

        public void Init()
        {
            Accumulator = 0;
            InstructionPointer = 0;
            Looped = false;
            _visitedInstructions.Clear();
        }

        public virtual int Run()
        {
            int result;
            do
            {
                result = HandleInstruction();
            } while(result == 0);

            return result;
        }

        public int HandleInstruction()
        {
            if(InstructionPointer >= Instructions.Count())
            {
                return Accumulator;
            }

            if(_visitedInstructions.Contains(InstructionPointer))
            {
                Looped = true;
                return Accumulator;
            }

            _visitedInstructions.Add(InstructionPointer);

            var instruction = Instructions[InstructionPointer];

            switch (instruction.Op)
            {
                case Op.acc:
                    Accumulate(instruction.Arg);
                    break;
                case Op.jmp:
                    Jump(instruction.Arg);
                    break;
                case Op.nop:
                    Nop(instruction.Arg);
                    break;
            }

            return 0;
        }

        public void Accumulate(int argument)
        {
            Accumulator += argument;

            InstructionPointer++;
        }

        public void Jump(int argument)
        {
            InstructionPointer += argument;
        }

        public void Nop(int argument)
        {
            InstructionPointer++;
        }
    }

    public enum Op
    {
        nop,
        acc,
        jmp
    }

    public class Instruction : ICloneable
    {
        public Op Op;
        public int Arg;

        public Instruction(Op op, int arg)
        {
            Op = op;
            Arg = arg;
        }

        public Instruction(string instrInfo)
        {
            var instructionParts = instrInfo.Split(' ');

            var operation = instructionParts[0];
            Arg = int.Parse(instructionParts[1]);

            Op = operation switch
            {
                "acc" => Op.acc,
                "jmp" => Op.jmp,
                _ => Op.nop,
            };
        }

        public object Clone()
        {
            return new Instruction(Op, Arg);
        }

        public override string ToString()
        {
            var signChar = Arg >= 0 ? "+" : "";

            return $"{Op} {signChar}{Arg}";
        }
    }
}
