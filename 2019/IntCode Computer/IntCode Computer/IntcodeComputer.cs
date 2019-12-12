using System;
using System.Collections.Generic;
using System.Text;

namespace IntCode_Computer
{
	public class IntcodeComputer
	{
		private double[] _program;
		private int _programCounter;
		private Queue<double> _inputs;
		private Queue<double> _outputs;
		private int _phase;
		private string _name;
		private int _relativeBase;

		public IntcodeComputer(double[] program, string name, int? phase = null)
		{
			_name = name;
			_program = new double[2056];
			for (int i = 0; i < _program.Length; i++)
			{
				_program[i] = i < program.Length 
					? program[i] 
					: 0;
			}

			_programCounter = 0;
			_inputs = new Queue<double>();
			_outputs = new Queue<double>();
			Halted = false;
			AwaitingInput = false;
			_relativeBase = 0;

			_phase = phase.GetValueOrDefault();

			if (phase.HasValue)
				_inputs.Enqueue(_phase);

			Paused = false;
		}

		public string Name => _name;

		public int Phase => _phase;

		public bool HasOutput => _outputs.Count > 0;

        public bool AwaitingInput { get; private set; }

		public bool Halted { get; private set; }

		public bool Processing => !Halted;

		public bool Paused;

        public void QueueInput(double input)
		{
			_inputs.Enqueue(input);
		}

		public double GetOutput()
		{
			return _outputs.Dequeue();
		}

		public void Run()
		{
			Halted = false;
			AwaitingInput = false;
			Paused = false;

			while (!Paused && !AwaitingInput && !Halted)
			{
				var instruction = _program[_programCounter];

				Console.WriteLine($"    Amp {Name} Executing instuction {instruction} at {_programCounter}, with params {Var1}, {Var2}, {Var3}");

				switch (instruction % 100)
				{
					case 1:
						one();
						break;
					case 2:
						two();
						break;
					case 3:
						if(_inputs.Count > 0)
						{							
							three();
						} else
						{
							AwaitingInput = true;
						}
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
					case 9:
						nine();
						break;
					case 99:
						Halted = true;
						break;
				}
			}
		}

		string Mode => ((int)(_program[_programCounter] / 100)).ToString();
		double Var1 => _program[_programCounter + 1];
		double Var2 => _program[_programCounter + 2];
		double Var3 => _program[_programCounter + 3];

		double param1 => (Mode[^1]) == '1' 
			? Var1 
			: (Mode[^1]) == '2' 
				? _program[(int)Var1 + _relativeBase]
				: _program[(int)Var1];

		double param2 => (Mode[^2]) == '1' 
			? Var2 
			: (Mode[^2]) == '2' 
				? _program[(int)Var2 + _relativeBase]
				:_program[(int)Var2];

		double param3
		{
			get
			{
				if (Mode.Length < 3)
					return Var3;
				return (Mode[^3]) == '1'
					? Var3
					: (Mode[^3]) == '2'
					   ? _program[(int)Var3 + _relativeBase]
					   : _program[(int)Var3];
			}
		}

		private void one()
		{
			_program[(int)param3] = param1 + param2;

			_programCounter += 4;

			return;
		}

		private void two()
		{
			_program[(int)param3] = param1 * param2;

			_programCounter += 4;

			return;
		}

		private void three()
		{
			var input = _inputs.Dequeue();

			var position = (Mode[^1]) == '2' ? _relativeBase + (int)Var1 : (int)Var1;

			_program[position] = input;

			Console.WriteLine($"      Amp {Name} Inserting {input} at {param1}");

			_programCounter += 2;

			AwaitingInput = false;

			return;
		}

		private void four()
		{
			_outputs.Enqueue(param1);

			Console.WriteLine($"      Amp {Name} Outputting {param1}");

			_programCounter += 2;

			//Paused = true;

			return;
		}

		private void five()
		{
			_programCounter = param1 != 0 ? (int)param2 : _programCounter + 3;

			return;
		}

		private void six()
		{
			_programCounter = param1 == 0 ? (int)param2 : _programCounter + 3;
			
			return;
		}

		private void seven()
		{
			_program[(int)param3] = param1 < param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}

		private void eight()
		{
			_program[(int)param3] = param1 == param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}

		private void nine()
		{
			_relativeBase += (int)param1;

			Console.WriteLine($"Shift relative base to {_relativeBase}");

			_programCounter += 2;

			return;
		}
	}
}
