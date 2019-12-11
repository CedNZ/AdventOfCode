using System;
using System.Linq;
using System.Collections.Generic;

namespace IntCode_Computer
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\7.txt");
			//var input = @"3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
			var program = input.Split(',').Select(x => int.Parse(x)).ToArray();

			var permutationSeed = "56789";
			var ampNames = new string[permutationSeed.Length];
			var nameIndex = 0;

			for (int n = ((int)'A'); n < ((int)'A' + permutationSeed.Length); n++)
			{
				ampNames[nameIndex] = ((char)n).ToString();
				nameIndex++;
			}

			IntcodeComputer[] amps;

			int? maxOutput = null;
			string maxPhase = "";

			foreach(var phaseInputs in Permutate(permutationSeed)) 
			{
				Console.WriteLine($"Begin Run Phase: {phaseInputs}");
				int nextInput = 0;
				amps = new IntcodeComputer[phaseInputs.Length];

				while(amps.All(x => x == null) || amps.Any(a => !a.Halted))
				{
					for(int i = 0; i < phaseInputs.Length; i++)
					{
						if(amps[i] == null)
						{
							amps[i] = new IntcodeComputer(program, ampNames[i], int.Parse(phaseInputs[i].ToString()));
							Console.WriteLine($"Created Amp {amps[i].Name}, with Phase: {amps[i].Phase}");
						}
						if(amps[i].Processing)
						{
							Console.WriteLine($"Running Amp {ampNames[i]}, Input: {nextInput}");
							amps[i].QueueInput(nextInput);
							amps[i].Run();
							if(amps[i].HasOutput)
							{
								nextInput = amps[i].GetOutput();
								Console.WriteLine($"Retreieved {nextInput} from Amp {ampNames[i]}");
							}
						}
					}
					Console.WriteLine("------------------------------------------------------------------");
				}

				Console.WriteLine($"Phase: {phaseInputs}, Output: {nextInput}");

				if(maxOutput == null || nextInput > maxOutput) {
					maxOutput = nextInput;
					maxPhase = phaseInputs;
				}
			}

			stopwatch.Stop();
			Console.WriteLine($"Max Output: {maxOutput}, Phase: {maxPhase}, Elapsed: {stopwatch.Elapsed}");
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
