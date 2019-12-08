void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var inputs = System.IO.File.ReadAllLines(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\3.txt");
	//var inputs = new string[] {"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"};
	var wire1 = inputs[0].Split(',');
	var wire2 = inputs[1].Split(',');
	
	var path1 = CalculatePath(wire1);
	var path2 = CalculatePath(wire2);
	
	var intersections = path1.Intersect(path2);	
	
	var answer = GetDistance(intersections);

	stopwatch.Stop();
	$"Shortest distance: {answer}\nElapsed: {stopwatch.Elapsed}".Dump();
}

// Define other methods and classes here
IEnumerable<System.Drawing.Point> CalculatePath(IEnumerable<string> instructions)
{
	var path = new List<System.Drawing.Point>();
	path.Add(new System.Drawing.Point(0, 0));
	foreach (var move in instructions)
	{
		var steps = int.Parse(move.Substring(1));
		switch (move[0])
		{
			case 'L':
				for (int i = 0; i < steps; i++)
				{
					path.Add(new System.Drawing.Point(path.Last().X - 1, path.Last().Y));
				}
				break;
			case 'R':
				for (int i = 0; i < steps; i++)
				{
					path.Add(new System.Drawing.Point(path.Last().X + 1, path.Last().Y));
				}
				break;
			case 'U':
				for (int i = 0; i < steps; i++)
				{
					path.Add(new System.Drawing.Point(path.Last().X, path.Last().Y - 1));
				}
				break;
			case 'D':
				for (int i = 0; i < steps; i++)
				{
					path.Add(new System.Drawing.Point(path.Last().X, path.Last().Y + 1));
				}
				break;
		}
	}
	path.RemoveAt(0);
	return path;
}

int GetDistance(IEnumerable<System.Drawing.Point> intersections)
{
	var minDistance = int.MaxValue;
	
	foreach (var intersection in intersections)
	{
		if (Math.Abs(intersection.X) + Math.Abs(intersection.Y) < minDistance)
			minDistance = Math.Abs(intersection.X) + Math.Abs(intersection.Y);
	}
	
	return minDistance;
}