using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.GameConsole
{
    public class GameConsole
    {
        List<(string instruction, int hitCount)> _instructions;

        public int Accumulator;

        private int _instructionPointer;
        private int _previousInstructionPointer;

        public int InstructionPointer
        {
            get => _instructionPointer;
            set
            {
                _previousInstructionPointer = _instructionPointer;
                _instructionPointer = value;
            }
        }

        public GameConsole(IEnumerable<string> inputs)
        {
            Accumulator = 0;
            InstructionPointer = 0;

            _instructions = new List<(string, int)>(inputs.Count());

            foreach(var input in inputs)
            {
                _instructions.Add((input, 0));
            }
        }

        public int Run()
        {
            var result = 0;
            do
            {
                result = ParseInstruction(_instructions);
            } while(result == 0);

            return result;
        }

        public int ParseInstruction(List<(string instruction, int hitCount)> instructions)
        {
            var instrInfo = instructions[InstructionPointer];

            instrInfo.hitCount++;

            instructions[InstructionPointer] = instrInfo;

            if (instrInfo.hitCount >= 2)
            {
                return Accumulator;
            }

            var instructionParts = instrInfo.instruction.Split(' ');

            var operation = instructionParts[0];
            var argument = int.Parse(instructionParts[1]);

            if (operation == "acc")
            {
                Accumulate(argument);
            }
            if (operation == "jmp")
            {
                Jump(argument);
            }
            if (operation == "nop")
            {
                InstructionPointer++;
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
    }
}
