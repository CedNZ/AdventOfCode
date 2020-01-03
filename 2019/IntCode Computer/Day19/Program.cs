using System;
using System.Collections.Generic;
using System.Linq;
using IntCode_Computer;
using System.Diagnostics;

namespace Day19
{
	class Program
	{
		const int WIDTH = 50;
		const int HEIGHT = 50;


		static void Main(string[] args)
		{
			var stopwatch = Stopwatch.StartNew();
			var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\19.txt");
			//var input = @"109, 1, 3, 3, 204, 2, 99";
			var program = input.Split(',').Select(x => double.Parse(x)).ToArray();

			IntcodeComputer robot = new IntcodeComputer(program, "TRACTOR");

			int[,] beam = new int[WIDTH, HEIGHT];

			for (int y = 0; y < HEIGHT; y++)
			{
				for (int x = 0; x < WIDTH; x++)
				{
					robot = new IntcodeComputer(program, "TRACTOR");
					robot.QueueInput(x);
					robot.QueueInput(y);
					robot.Run();
					if (robot.HasOutput)
					{
						beam[x, y] = (int)robot.GetOutput();
					}
				}
			}

			var count = 0;
			var output = "";
			for (int y = 0; y < HEIGHT; y++)
			{
				for (int x = 0; x < WIDTH; x++)
				{
					output += beam[x, y];
					if (beam[x, y] == 1)
					{
						count++;
					}
				}
				output += "\n";
			}

			stopwatch.Stop();
			Console.WriteLine(output);
			Console.WriteLine($"Count: {count}\nElapsed: {stopwatch.Elapsed}");
		}
	}
}
