using System;
using System.Collections.Generic;
using System.Linq;
using IntCode_Computer;
using System.Diagnostics;

namespace Day9
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = Stopwatch.StartNew();

			var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\9.txt");
			//var input = @"109, 1, 3, 3, 204, 2, 99";
			var program = input.Split(',').Select(x => double.Parse(x)).ToArray();

			IntcodeComputer computer = new IntcodeComputer(program, "BOOST");

			computer.QueueInput(1);

			computer.Run();

			var output = "";

			stopwatch.Stop();

			while (computer.HasOutput)
			{
				output += computer.GetOutput() + ", ";
			}

			Console.WriteLine($"\nOutput: {output}\nElapsed: {stopwatch.Elapsed}");
		}
	}
}
