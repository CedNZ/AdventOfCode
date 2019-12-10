using System;
using System.Linq;

namespace IntCode_Computer
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Code\AdventOfCode\2019\Inputs\5.txt");
			//var input = @"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
			var program = input.Split(',').Select(x => int.Parse(x)).ToArray();

			IntcodeComputer computer = new IntcodeComputer(program);

			computer.QueueInput(1);

			computer.Run();

			var output = computer.GetOutput();

			Console.WriteLine(output);
		}
	}
}
