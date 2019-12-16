﻿using System;
using System.Collections.Generic;
using System.Linq;
using IntCode_Computer;
using System.Diagnostics;

namespace Day15
{
	class Program
	{
		static void Main(string[] args)
		{
			var stopwatch = Stopwatch.StartNew();
			var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\15.txt");
			//var input = @"109, 1, 3, 3, 204, 2, 99";
			var program = input.Split(',').Select(x => double.Parse(x)).ToArray();

			IntcodeComputer robot = new IntcodeComputer(program, "PATHFINDER");
			int robotX = 0;
			int robotY = 0;

			List<Path> visitedPaths = new List<Path>();

			Path start = new Path(robotX, robotY);

			visitedPaths.Add(start);

			Path currentPath = start;

			bool searching = true;
			while (searching)
			{
				var directionToCheck = currentPath.GetNextDirection();
				//Console.WriteLine($"Moving {directionToCheck.ToString()}");
				robot.QueueInput((int)directionToCheck);
				robot.Run();
				var result = (int)robot.GetOutput();
				if (result == 0) //hit a wall
				{
					//Console.WriteLine($"Hit a wall");
					if (directionToCheck == Direction.North)
						currentPath.wallNorth = true;
					if (directionToCheck == Direction.South)
						currentPath.wallSouth = true;
					if (directionToCheck == Direction.East)
						currentPath.wallEast = true;
					if (directionToCheck == Direction.West)
						currentPath.wallWest = true;
				}
				if (result == 1) //moved
				{
					if (directionToCheck == Direction.North)
						robotY--;
					if (directionToCheck == Direction.South)
						robotY++;
					if (directionToCheck == Direction.East)
						robotX++;
					if (directionToCheck == Direction.West)
						robotX--;
					//Console.WriteLine($"Moved {directionToCheck.ToString()}, Robot now at {robotX}:{robotY}");
					var previousPath = currentPath;
					currentPath = visitedPaths.FirstOrDefault(x => x.X == robotX && x.Y == robotY) ?? new Path(robotX, robotY, previousPath);

					if (directionToCheck == Direction.North)
						previousPath.North = currentPath;
					if (directionToCheck == Direction.South)
						previousPath.South = currentPath;
					if (directionToCheck == Direction.East)
						previousPath.East = currentPath;
					if (directionToCheck == Direction.West)
						previousPath.West = currentPath;

					if (!visitedPaths.Contains(currentPath))
					{
						visitedPaths.Add(currentPath);
					}
				}
				if (result == 2)
				{
					if (directionToCheck == Direction.North)
						robotY--;
					if (directionToCheck == Direction.South)
						robotY++;
					if (directionToCheck == Direction.East)
						robotX++;
					if (directionToCheck == Direction.West)
						robotX--;
					searching = false;
					Console.WriteLine($"Found exit at {robotX}:{robotY}, depth of {currentPath.Distance + 1}");
				}
			}
			stopwatch.Stop();
			Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
		}

		public static void Paint<T>(Dictionary<(int x, int y), T> pixels) where T : Enum
		{
			int minX = pixels.Keys.Min(p => p.x);
			int maxX = pixels.Keys.Max(p => p.x);
			int minY = pixels.Keys.Min(p => p.y);
			int maxY = pixels.Keys.Max(p => p.y);

			//Console.WriteLine($"minX: {minX}, minY: {minY}\nmaxX: {maxX}, maxY: {maxY}");

			T[,] painting = new T[maxY + 1, maxX + 1];

			foreach (var position in pixels.Keys)
			{
				painting[position.y, position.x] = pixels[position];
			}

			Console.Clear();
			string maze = "";
			for (int y = 0; y <= maxY; y++)
			{
				string row = "";
				for (int x = 0; x <= maxX; x++)
				{
					if (painting[y, x] != null)
					{
						row += painting[y, x];
					}
					else
					{
						row += " ";
					}
				}
				maze += row;
				maze += "\n";
			}
			Console.Write(maze);
		}
	}

	enum Direction
	{
		Nowhere = 0,
		North = 1,
		South = 2,
		West = 3,
		East = 4
	}

	class Path
	{
		public int X;
		public int Y;
		bool Deadend;
		public int Distance;
		Path Parent;
		public Path North, South, East, West;
		public bool wallNorth, wallSouth, wallEast, wallWest;

		public Path(int x, int y, Path parent)
		{
			X = x;
			Y = y;
			Parent = parent;
			Distance = parent.Distance + 1;
		}

		public Path(int x, int y)
		{
			X = x;
			Y = y;
			Distance = 0;
		}

		public Direction GetNextDirection()
		{
			var directionToParent = DirectionToParent();
			if (North == null && !wallNorth && directionToParent !=  Direction.North)
				return Direction.North;
			if (East == null && !wallEast && directionToParent != Direction.East)
				return Direction.East;
			if (South == null && !wallSouth && directionToParent != Direction.South)
				return Direction.South;
			if (West == null && !wallWest && directionToParent != Direction.West)
				return Direction.West;

			Deadend = true;
			return directionToParent;
		}

		public Direction DirectionToParent()
		{
			if (Parent == null)
				return Direction.Nowhere;
			if (Parent.X < X)
				return Direction.West;
			if (Parent.X > X)
				return Direction.East;
			if (Parent.Y < Y)
				return Direction.North;
			return Direction.South;
		}
	}
}
