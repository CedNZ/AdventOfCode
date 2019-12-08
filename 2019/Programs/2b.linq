void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\2.txt");	
	var initArray = input.Split(',').Select(x => int.Parse(x)).ToArray();
	
	for (int x = 0; x < 99; x++)
	{
		for (int y = 0; y < 99; y++)
		{
			var numArray = initArray.ToArray();
			
			numArray[1] = x;
			numArray[2] = y;

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
			if (numArray[0] == 19690720)
			{
				stopwatch.Stop();
				$"noun: {x}, verb: {y}\nAnswer: {100 * x + y}".Dump();	
				x = 1000;
				$"Number at position 0: {numArray[0]}\nElapsed: {stopwatch.Elapsed}".Dump();
				numArray.Dump();
				break;
			}
		}
	}
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