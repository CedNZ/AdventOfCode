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

			bool firstRun = true;
			Colour initialColour = Colour.White;

			while (!robot.Halted)
			{
				var currentSpaceColour = hull.ContainsKey((robot.X, robot.Y)) 
					? hull[(robot.X, robot.Y)]
					: Colour.Black;

				if (firstRun)
				{
					currentSpaceColour = initialColour;
					firstRun = false;
				}

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


			int minX = hull.Keys.Min(p => p.x);
			int maxX = hull.Keys.Max(p => p.x);
			int minY = hull.Keys.Min(p => p.y);
			int maxY = hull.Keys.Max(p => p.y);

			Console.WriteLine($"minX: {minX}, minY: {minY}\nmaxX: {maxX}, maxY: {maxY}");

			Colour[,] painting = new Colour[maxY + 1, maxX + 1];
			foreach(var position in hull.Keys)
			{
				painting[position.y, position.x] = hull[position];
			}

			for (int y = minY; y <= maxY; y++)
			{
				string row = "";
				for (int x = minX; x <= maxX; x++)
				{
					if (painting[y, x] is Colour colour)
					{
						row += colour == Colour.Black ? "█" : " ";
					}
					else
					{
						row += "█";
					}
				}
				Console.WriteLine(row);
			}


			stopwatch.Stop();
			Console.WriteLine($"Squares painted: {hull.Count}\nElapsed: {stopwatch.Elapsed}\n\n");	
			
			
		}
	}

	enum Colour
	{
		Black = 0,
		White = 1
	}
}
