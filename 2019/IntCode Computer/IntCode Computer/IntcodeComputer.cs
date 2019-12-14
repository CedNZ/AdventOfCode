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
		private Modes[] _modes;
		private IntOps _intOp;

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
			_modes = new Modes[3];

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

		public void PopulateModes(double instruction)
		{
			_modes = new Modes[] { Modes.Null, Modes.Null, Modes.Null };
			var modeString = ((int)instruction / 100).ToString();
			for (int i = 0; i < modeString.Length; i++)
			{
				_modes[i] = (Modes)int.Parse(modeString[^(i+1)].ToString());
			}
		}

		public void Run()
		{
			Halted = false;
			AwaitingInput = false;
			Paused = false;

			while (!Paused && !AwaitingInput && !Halted)
			{
				var instruction = _program[_programCounter];

				//Console.WriteLine($"    {Name} Executing instuction {instruction} at {_programCounter}, with params {Var1}, {Var2}, {Var3}");

				_intOp = (IntOps)(instruction % 100);

				PopulateModes(instruction);

				switch (_intOp)
				{
					case IntOps.Add:
						Add();
						break;
					case IntOps.Multiply:
						Multiply();
						break;
					case IntOps.Input:
						if(_inputs.Count > 0)
						{							
							Input();
						} else
						{
							AwaitingInput = true;
						}
						break;
					case IntOps.Output:
						Output();
						break;
					case IntOps.JumpIfTrue:
						JumpIfTrue();
						break;
					case IntOps.JumpIfFalse:
						JumpIfFalse();
						break;
					case IntOps.LessThan:
						LessThan();
						break;
					case IntOps.Equals:
						Equals();
						break;
					case IntOps.AdjustRelativeBase:
						AdjustRelativeBase();
						break;
					case IntOps.Halt:
						Halted = true;
						break;
				}
			}
		}

		string Mode => ((int)(_program[_programCounter] / 100)).ToString();
		double Var1 => _program[_programCounter + 1];
		double Var2 => _program[_programCounter + 2];
		double Var3 => _program[_programCounter + 3];

		double param1
		{
			get
			{
				switch(_modes[0])
				{
					case Modes.Null:
					case Modes.Position:
						if(_intOp == IntOps.Input)
							return Var1;
						return _program[(int)Var1];
					case Modes.Immediate:
						return Var1;
					case Modes.Relative:
						if (_intOp == IntOps.Input)
							return Var1 + _relativeBase;
						return _program[(int)Var1 + _relativeBase];
				}
				return 0;
			}
		}

		double param2
		{
			get
			{
				switch(_modes[1])
				{
					case Modes.Null:
					case Modes.Position:
						return _program[(int)Var2];
					case Modes.Immediate:
						return Var2;
					case Modes.Relative:
						return _program[(int)Var2 + _relativeBase];
				}
				return 0;
			}
		}

		double param3
		{
			get
			{
				switch(_modes[2])
				{
					case Modes.Position:
						return _program[(int)Var3];
					case Modes.Null:
					case Modes.Immediate:
						return Var3;
					case Modes.Relative:
						return Var3 + _relativeBase;
				}
				return 0;
			}
		}

		//	1
		private void Add()
		{
			_program[(int)param3] = param1 + param2;

			_programCounter += 4;

			return;
		}

		//	2
		private void Multiply()
		{
			_program[(int)param3] = param1 * param2;

			_programCounter += 4;

			return;
		}

		//	3
		private void Input()
		{
			var input = _inputs.Dequeue();

			_program[(int)param1] = input;

			//Console.WriteLine($"      {Name} Inserting {input} at {param1}");

			_programCounter += 2;

			AwaitingInput = false;

			return;
		}

		//	4
		private void Output()
		{
			_outputs.Enqueue(param1);

			//Console.WriteLine($"      {Name} Outputting {param1}");

			_programCounter += 2;

			//Paused = true;

			return;
		}

		//	5
		private void JumpIfTrue()
		{
			_programCounter = param1 != 0 ? (int)param2 : _programCounter + 3;

			return;
		}

		//	6
		private void JumpIfFalse()
		{
			_programCounter = param1 == 0 ? (int)param2 : _programCounter + 3;
			
			return;
		}

		//	7
		private void LessThan()
		{
			_program[(int)param3] = param1 < param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}

		//	8
		private void Equals()
		{
			_program[(int)param3] = param1 == param2 ? 1 : 0;

			_programCounter += 4;

			return;
		}

		//	9
		private void AdjustRelativeBase()
		{
			_relativeBase += (int)param1;

			//Console.WriteLine($"Shift relative base to {_relativeBase}");

			_programCounter += 2;

			return;
		}
	}

	public enum IntOps
	{
		Add							=	1,
		Multiply					=	2,				
		Input						=	3,
		Output						=	4,
		JumpIfTrue					=	5,
		JumpIfFalse					=	6,
		LessThan					=	7,
		Equals						=	8,
		AdjustRelativeBase			=	9,
		Halt						=	99,
	}

	public enum Modes
	{
		Null = -1,
		Position = 0,
		Immediate = 1,
		Relative = 2,
	}
}
