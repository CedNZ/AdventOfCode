
void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\2.txt");	
	var numArray = input.Split(',').Select(x => int.Parse(x)).ToArray();
	
	numArray[1] = 12;
	numArray[2] = 2;
	
	for (int i = 0; i < numArray.Length; i += 4)
	{
		switch (numArray[i])
		{
			case 1:
				one(numArray[i + 1], numArray[i + 2], numArray[i + 3], numArray);
				break;
			case 2:
				two(numArray[i + 1], numArray[i + 2], numArray[i + 3], numArray);
				break;
			case 99:
				i = numArray.Length + 1;
				break;
		}
	}
	stopwatch.Stop();
	$"Number at position 0: {numArray[0]}\nElapsed: {stopwatch.Elapsed}".Dump();
	numArray.Dump();
}

// Define other methods and classes here
void one(int pos1, int pos2, int pos3, int[] numArray) 
{
	numArray[pos3] = numArray[pos1] + numArray[pos2];
	return;
}

void two(int pos1, int pos2, int pos3, int[] numArray)
{
	numArray[pos3] = numArray[pos1] * numArray[pos2];
	return;
}