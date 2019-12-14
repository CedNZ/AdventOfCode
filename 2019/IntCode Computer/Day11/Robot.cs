using System;
using System.Collections.Generic;
using System.Text;
using IntCode_Computer;

namespace Day11
{
	public class Robot : IntcodeComputer
	{
		private Direction _direction;

		public Robot(double[] program, string name, int? phase = null) : base(program, name, phase)
		{
			X = 0;
			Y = 0;
			_direction = Direction.UP;
		}

		public int X;
		public int Y;

		public void Move(bool turnRight)
		{
			//First Rotate the robot
			switch (_direction)
			{
				case Direction.UP:
					_direction = turnRight ? Direction.RIGHT : Direction.LEFT;
					break;
				case Direction.RIGHT:
					_direction = turnRight ? Direction.DOWN : Direction.UP;
					break;
				case Direction.DOWN:
					_direction = turnRight ? Direction.LEFT : Direction.RIGHT;
					break;
				case Direction.LEFT:
					_direction = turnRight ? Direction.UP : Direction.DOWN;
					break;
			}

			//Now move the robot
			switch (_direction)
			{
				case Direction.UP:
					Y--;
					break;
				case Direction.RIGHT:
					X++;
					break;
				case Direction.DOWN:
					Y++;
					break;
				case Direction.LEFT:
					X--;
					break;
			}
		}

	}

	enum Direction
	{
		UP = 0,
		RIGHT = 1,
		DOWN = 2,
		LEFT = 3
	}
}
