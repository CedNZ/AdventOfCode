<Query Kind="Program" />

void Main()
{
	var stopwatch = Stopwatch.StartNew();
	var input = "109165-576723";
	
	var min = input.Split('-').Select(x => int.Parse(x)).First();
	var max = input.Split('-').Select(x => int.Parse(x)).Last();
	
	var answer = GeneratePasswords(min, max).Count();
	stopwatch.Stop();

	$"Possible passwords: {answer}\nElapsed: {stopwatch.Elapsed}".Dump();
}

// Define other methods, classes and namespaces here
bool CheckPassword(int num) {
	var password = num.ToString();
	var pairs = password.Skip(1).Zip(password, (first, second) => new[] {first, second});

	if (pairs.All(x => x[0] != x[1])) // check there contains at least one matching pair
		return false;

	if (pairs.Any(x => x[0] < x[1])) // check numbers never decrease
		return false;
		
	if (pairs.Count(x => x[0] == x[1]) == 1) //if only 1 pair, return true
		return true;
		
	var pairNums = pairs.Where(x => x[0] == x[1]).Select(x => x[0]).Distinct();
	if (pairNums.All(x => password.Count(c => c == x) > 2))
		return false;
	
	return true;
}

IEnumerable<int> GeneratePasswords(int min, int max) {
	int current = min;
	while (current < max)
	{
		current++;
		if (CheckPassword(current))
			yield return current;	
	}	
}