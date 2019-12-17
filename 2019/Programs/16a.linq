<Query Kind="Program" />

List<List<int>> phasePatterns;

void Main()
{
	var input = "12345678";
	var basePattern = new List<int> { 0, 1, 0, -1};
	phasePatterns = new List<List<int>> ();
	
	//Populate phase patterns
	for (int i = 1; i <= input.Length; i++)
	{
		var phasePattern = new List<int>();
		foreach (var element in basePattern)
		{
			phasePattern.AddRange(Enumerable.Repeat(element, i));
		}
		
		phasePatterns.Add(phasePattern.ToList());
	}
	
	long workingValue = long.Parse(input);
	for (int i = 0; i < 10; i++)
	{
		workingValue = FFT(workingValue);
		$"After {i} phase: {workingValue}".Dump();
	}
}

// Define other methods and classes here
public long FFT(long input)
{
	var inputString = input.ToString();
	var outputString = "";
	foreach (var phase in phasePatterns)
	{
		int i = 0;
		var num = 0;
		var charPosition = 0;
		for (int outer = 0; outer < inputString.Count(); outer++)
		{
			var current = int.Parse(inputString[charPosition].ToString());
			current *= phase[(charPosition + 1) % phase.Count];
			num += current;
			charPosition++;
		}	
		i++;
		outputString += Math.Abs(num % 10);
	}
	
	return long.Parse(outputString);
}