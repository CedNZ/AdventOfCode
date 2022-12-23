using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day12 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }

        public long A(List<string> inputs)
        {
            var ship = new Ship();
            foreach (var input in inputs)
            {
                ship.Move(input);
            }
            return Math.Abs(ship.X) + Math.Abs(ship.Y);
        }

        public long B(List<string> inputs)
        {
            var ship = new Ship();
            foreach(var input in inputs)
            {
                ship.Move2(input);
            }
            return Math.Abs(ship.X) + Math.Abs(ship.Y);
        }
    }

    public class Ship
    {
        public int X;
        public int Y;
        public int Bearing;

        public int WaypointX;
        public int WaypointY;

        public Ship()
        {
            X = 0;
            Y = 0;
            Bearing = 0;

            WaypointX = 10;
            WaypointY = -1;
        }

        public void Move(string instruction)
        {
            var letter = instruction[0];
            var number = int.Parse(instruction.Substring(1));

            switch (letter)
            {
                case 'N':
                    Y -= number;
                    break;
                case 'S':
                    Y += number;
                    break;
                case 'E':
                    X += number;
                    break;
                case 'W':
                    X -= number;
                    break;
                case 'L':
                    Bearing -= number;
                    Bearing %= 360;
                    break;
                case 'R':
                    Bearing += number;
                    Bearing %= 360;
                    break;
                case 'F':
                    if (Bearing == 0) // East
                    {
                        X += number;
                    }
                    else if (Math.Abs(Bearing) == 180) // West
                    {
                        X -= number;
                    }
                    else if(Bearing == -90 || Bearing == 270) //North
                    {
                        Y -= number;
                    }
                    else if(Bearing == 90 || Bearing == -270) //South
                    {
                        Y += number;
                    }
                    break;
            }
        }

        public void Move2(string instruction)
        {
            var letter = instruction[0];
            var number = int.Parse(instruction.Substring(1));
            int quadrants;

            switch(letter)
            {
                case 'N':
                    WaypointY -= number;
                    break;
                case 'S':
                    WaypointY += number;
                    break;
                case 'E':
                    WaypointX += number;
                    break;
                case 'W':
                    WaypointX -= number;
                    break;
                case 'L':
                    quadrants = (number / 90) % 4;
                    for (int i = 0; i < quadrants; i++)
                    {
                        var temp = -WaypointX;
                        WaypointX = WaypointY;
                        WaypointY = temp;
                    }
                    break;
                case 'R':
                    quadrants = (number / 90) % 4;
                    for(int i = 0; i < quadrants; i++)
                    {
                        var temp = -WaypointY;
                        WaypointY = WaypointX;
                        WaypointX = temp;
                    }
                    break;
                case 'F':
                    X += (number * WaypointX);
                    Y += (number * WaypointY);
                    break;
            }
        }
    }
}
