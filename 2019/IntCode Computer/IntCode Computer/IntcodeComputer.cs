using System;
using System.Collections.Generic;
using System.Text;

namespace IntCode_Computer
{
	public class IntcodeComputer
	{
		private int[] _program;
		private int _programCounter;
		private Queue<int> _inputs;
		private Queue<int> _outputs;

		public IntcodeComputer(int[] program, int? phase = null)
		{
			_program = program;
			_programCounter = 0;
			_inputs = new Queue<int>();
			_outputs = new Queue<int>();

			if (phase.HasValue)
			{
				_inputs.Enqueue(phase.Value);
			}
		}

		public void QueueInput(int input)
		{
			_inputs.Enqueue(input);
		}

		public int GetOutput()
		{
			return _outputs.Dequeue();
		}

		public void Run()
		{
			while (_programCounter < _program.Length)
			{
				var instruction = _program[_programCounter];

				switch (instruction % 100)
				{
					case 1:
						one();
						break;
					case 2:
						two();
						break;
					case 3:
						if (_inputs.Count > 0)
							three();
						break;
					case 4:
						four();
						break;
					case 5:
						five();
						break;
					case 6:
						six();
						break;
					case 7:
						seven();
						break;
					case 8:
						eight();
						break;
					case 99:
						_programCounter = _program.Length + 1;
						break;
				}
			}

		}

		int Mode => _program[_programCounter] / 100;

		int Var1 => _program[_programCounter + 1];
		int Var2 => _program[_programCounter + 2];
		int Var3 => _program[_programCounter + 3];

		private void one()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_program[Var3] = param1 + param2;

			_programCounter += 4;

			return;
		}

		private void two()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_program[Var3] = param1 * param2;

			_programCounter += 4;

			return;
		}

		private void three()
		{
			_program[Var1] = _inputs.Dequeue();

			_programCounter += 2;

			return;
		}

		private void four()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];

			_outputs.Enqueue(param1);

			_programCounter += 2;

			return;
		}

		private void five()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_programCounter = param1 != 0
				? param2
				: _programCounter + 3;
			return;
		}

		private void six()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_programCounter = param1 == 0
				? param2
				: _programCounter + 3;
			return;
		}

		private void seven()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_program[Var3] = param1 < param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}

		private void eight()
		{
			int param1 = (Mode & 1) > 0 ? Var1 : _program[Var1];
			int param2 = (Mode & 10) > 0 ? Var2 : _program[Var2];

			_program[Var3] = param1 == param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}
	}
}
