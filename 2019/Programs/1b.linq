void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllLines(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\1a.txt");
	var result = input.Sum(x => calculateFuel(int.Parse(x)));
	stopwatch.Stop();
	$"Result: {result}\nCompleted in {stopwatch.Elapsed}".Dump();
	
}

// Define other methods and classes here
int calculateFuel(int mass) {
	int fuel = ((mass/3)-2);
	if (fuel <= 0) {
		return 0;
	}
	return fuel + calculateFuel(fuel);
}