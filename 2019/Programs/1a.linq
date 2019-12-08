var stopwatch = System.Diagnostics.Stopwatch.StartNew();
var input = System.IO.File.ReadAllLines(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\1a.txt");
var result = input.Sum(x => ((int.Parse(x) / 3) - 2));
stopwatch.Stop();
$"Result: {result}\nCompleted in {stopwatch.Elapsed}".Dump();
