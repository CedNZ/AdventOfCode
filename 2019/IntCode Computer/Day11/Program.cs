using System;
using System.Collections.Generic;
using System.Linq;
using IntCode_Computer;
using System.Diagnostics;

namespace Day11
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = Stopwatch.StartNew();

			var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\11.txt");
			//var input = @"109, 1, 3, 3, 204, 2, 99";
			var program = input.Split(',').Select(x => double.Parse(x)).ToArray();

			Robot robot = new Robot(program, "ROBOT");

			Dictionary<(int x, int y), Colour> hull = new Dictionary<(int x, int y), Colour>();

			while (!robot.Halted)
			{
				var currentSpaceColour = hull.ContainsKey((robot.X, robot.Y)) 
					? hull[(robot.X, robot.Y)]
					: Colour.Black;

				robot.QueueInput((int)currentSpaceColour);

				robot.Run();

				var colourToPaint =(Colour)robot.GetOutput();
				var directionToTurn = (int)robot.GetOutput();

				if (hull.ContainsKey((robot.X, robot.Y)))
				{
					hull[(robot.X, robot.Y)] = colourToPaint;
				}
				else
				{
					hull.Add((robot.X, robot.Y), colourToPaint);
				}

				robot.Move(directionToTurn == 1);
			}

			stopwatch.Stop();
			Console.WriteLine($"Squares painted: {hull.Count}\nElapsed: {stopwatch.Elapsed}");
		}
	}

	enum Colour
	{
		Black = 0,
		White = 1
	}
}
