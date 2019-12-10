using System;
using System.Linq;
using System.Collections.Generic;

namespace IntCode_Computer
{
	class Program
	{
		static void Main(string[] args)
		{
			//var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\7.txt");
			var input = @"3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
			var program = input.Split(',').Select(x => int.Parse(x)).ToArray();

			IntcodeComputer[] amps = new IntcodeComputer[5];

			int maxOutput = 0;
			string maxPhase = "";

			foreach(var phaseInputs in Permutate("56789")) 
			{
				int nextInput = 0;

				for(int i = 0; i < phaseInputs.Length; i++)
				{
					if(amps[i] == null)
					{
						amps[i] = new IntcodeComputer(program, int.Parse(phaseInputs[i].ToString()));
					}
					if(amps[i].Processing)
					{
						amps[i].QueueInput(nextInput);
						amps[i].Run();
						if(amps[i].HasOutput)
						{
							nextInput = amps[i].GetOutput();
						}
					}
				}

				Console.WriteLine($"Phase: {phaseInputs}, Output: {nextInput}");

				if(nextInput > maxOutput) {
					maxOutput = nextInput;
					maxPhase = phaseInputs;
				}
			}

			Console.WriteLine($"Max Output: {maxOutput}, Phase: {maxPhase}");
		}

		private static IEnumerable<string> Permutate(string source) 
		{
			if(source.Length == 1) return new List<string> { source };

			var permutations = from c in source
							   from p in Permutate(new String(source.Where(x => x != c).ToArray()))
							   select c + p;

			return permutations;
		}
	}	
}
