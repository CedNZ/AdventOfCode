
void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Code\AdventOfCode\2019\Inputs\7.txt");	
	//var input = @"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
	var numArray = input.Split(',').Select(x => int.Parse(x)).ToArray();

	var programInputInstruction = 0;
	var maxOutput = 0;
	var maxPhase = "";

	foreach (var phaseInputs in MyExtensions.Permutate("01234"))
	{
		programInputInstruction = 0;
		for (int i = 0; i < 5; i++)
		{
			var program = numArray.ToArray();
			programInputInstruction = RunAmp(int.Parse(phaseInputs.ElementAt(i).ToString()), programInputInstruction, program);
		}
		$"Phase: {phaseInputs}, Output: {programInputInstruction}".Dump();
		if (programInputInstruction > maxOutput)
		{
			maxOutput = programInputInstruction;
			maxPhase = phaseInputs;
		}
	}

	stopwatch.Stop();
	$"Largest output: {maxOutput}, Phase: {maxPhase}, Elapsed: {stopwatch.Elapsed}".Dump();
	//numArray.Dump();
}

// Define other methods and classes here
int RunAmp(int phase, int inputInstruction, int[] program)
{
	int counter = 0;
	var output = 0;
	bool secondInput = false;
	int programInput = 0;

	while (counter < program.Length)
	{
		switch (program[counter] % 100)
		{
			case 1:
				one(program[counter + 1], program[counter + 2], program[counter + 3], program, program[counter] / 100);
				counter += 4;
				break;
			case 2:
				two(program[counter + 1], program[counter + 2], program[counter + 3], program, program[counter] / 100);
				counter += 4;
				break;
			case 3:
				programInput = secondInput ? inputInstruction : phase;
				three(programInput, program[counter + 1], program);
				secondInput = true;
				counter += 2;
				break;
			case 4:
				output = four(program[counter + 1], program, program[counter] / 100);
				counter += 2;
				break;
			case 5:
				five(program[counter + 1], program[counter + 2], program, program[counter] / 100, ref counter);
				break;
			case 6:
				six(program[counter + 1], program[counter + 2], program, program[counter] / 100, ref counter);
				break;
			case 7:
				seven(program[counter + 1], program[counter + 2], program[counter + 3], program, program[counter] / 100);
				counter += 4;
				break;
			case 8:
				eight(program[counter + 1], program[counter + 2], program[counter + 3], program, program[counter] / 100);
				counter += 4;
				break;
			case 99:
				counter = program.Length + 1;
				break;
		}
	}
	return output;
}

void one(int pos1, int pos2, int pos3, int[] numArray, int mode) 
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];
	
	numArray[pos3] = param1 + param2;
	return;
}

void two(int pos1, int pos2, int pos3, int[] numArray, int mode)
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];
	
	numArray[pos3] = param1 * param2;
	return;
}

void three(int input, int position, int[] numArray)
{
	numArray[position] = input;
	return;
}

int four(int position, int[] numArray, int mode)
{
	var answer = mode > 0 ? position : numArray[position];
	//$"4: Outputting - {answer}".Dump();
	return answer;
}

void five(int pos1, int pos2, int[] numArray, int mode, ref int counter)
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];
	
	counter = param1 != 0
		? param2
		: counter + 3;
	return;
}

void six(int pos1, int pos2, int[] numArray, int mode, ref int counter)
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];

	counter = param1 == 0
		? param2
		: counter + 3;
	return;
}

void seven(int pos1, int pos2, int pos3, int[] numArray, int mode)
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];
	//int param3 = (mode & 100) > 0 ? pos3 : numArray[pos3];
	
	numArray[pos3] = param1 < param2 ? 1 : 0;
	return;
}

void eight(int pos1, int pos2, int pos3, int[] numArray, int mode)
{
	int param1 = (mode & 1) > 0 ? pos1 : numArray[pos1];
	int param2 = (mode & 10) > 0 ? pos2 : numArray[pos2];
	//int param3 = (mode & 100) > 0 ? pos3 : numArray[pos3];
	
	numArray[pos3] = param1 == param2 ? 1 : 0;
	return;
}