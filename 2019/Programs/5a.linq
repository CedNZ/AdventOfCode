void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\5.txt");	
	//var input = "1002,4,3,4,33";
	var numArray = input.Split(',').Select(x => int.Parse(x)).ToArray();
	
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
				three(1, numArray[counter +1], numArray);
				counter += 2;
				break;
			case 4:
				four(numArray[counter + 1], numArray);
				counter += 2;
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

void four(int position, int[] numArray)
{
	$"4: Outputting - {numArray[position]}".Dump();
	return;
}