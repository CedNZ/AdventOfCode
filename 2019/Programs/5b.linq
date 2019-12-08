
void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\5.txt");	
	//var input = @"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
	var numArray = input.Split(',').Select(x => int.Parse(x)).ToArray();
	
	var programInputInstruction = 5;	
	//numArray[1] = 12;
	//numArray[2] = 2;
	
	int counter = 0;
	
	while (counter < numArray.Length)
	{
		switch (numArray[counter] % 100)
		{
			case 1:
				one(numArray[counter + 1], numArray[counter + 2], numArray[counter + 3], numArray, numArray[counter] / 100);
				counter += 4;
				break;
			case 2:
				two(numArray[counter + 1], numArray[counter + 2], numArray[counter + 3], numArray, numArray[counter] / 100);
				counter += 4;
				break;
			case 3:
				three(programInputInstruction, numArray[counter +1], numArray);
				counter += 2;
				break;
			case 4:
				four(numArray[counter + 1], numArray, numArray[counter] / 100);
				counter += 2;
				break;
			case 5:
				five(numArray[counter + 1], numArray[counter + 2], numArray, numArray[counter] / 100, ref counter);
				break;
			case 6:
				six(numArray[counter + 1], numArray[counter + 2], numArray, numArray[counter] / 100, ref counter);
				break;
			case 7:
				seven(numArray[counter + 1], numArray[counter + 2], numArray[counter + 3], numArray, numArray[counter] / 100);
				counter += 4;
				break;
			case 8:
				eight(numArray[counter + 1], numArray[counter + 2], numArray[counter + 3], numArray, numArray[counter] / 100);
				counter += 4;
				break;
			case 99:
				counter = numArray.Length + 1;
				break;
		}
	}
	stopwatch.Stop();
	$"Number at position 0: {numArray[0]}\nElapsed: {stopwatch.Elapsed}".Dump();
	numArray.Dump();
}

// Define other methods and classes here
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

void four(int position, int[] numArray, int mode)
{
	var answer = mode > 0 ? position : numArray[position];
	$"4: Outputting - {answer}".Dump();
	return;
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