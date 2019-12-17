<Query Kind="Program" />

List<List<int>> phasePatterns;

void Main()
{
	var stopwatch = Stopwatch.StartNew();
	var input = "59766832516471105169175836985633322599038555617788874561522148661927081324685821180654682056538815716097295567894852186929107230155154324411726945819817338647442140954601202408433492208282774032110720183977662097053534778395687521636381457489415906710702497357756337246719713103659349031567298436163261681422438462663511427616685223080744010014937551976673341714897682634253850270219462445161703240957568807600494579282412972591613629025720312652350445062631757413159623885481128914333982571503540357043736821931054029305931122179293220911720263006705242490442826574028623201238659548887822088996956559517179003476743001815465428992906356931239533104";
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
	
	string workingValue = input;
	for (int i = 0; i < 100; i++)
	{
		workingValue = FFT(workingValue);
	}
	stopwatch.Stop();
	$"First 8 digits of FFT: {workingValue.ToString().Substring(0, 8)}\nCompleted in: {stopwatch.Elapsed}".Dump();
}

// Define other methods and classes here
public string FFT(string input)
{
	var outputString = "";
	foreach (var phase in phasePatterns)
	{
		var num = 0;
		System.Threading.Tasks.Parallel.ForEach(input, (character, loopState, index) =>
		{
			var current = int.Parse(character.ToString());
			var phaseNum = phase[(int)((index + 1) % phase.Count)];
			num += current * phaseNum;
		});
		outputString += Math.Abs(num % 10);
	}
	
	return outputString;
}