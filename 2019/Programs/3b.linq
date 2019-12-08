void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var inputs = System.IO.File.ReadAllLines(@"C:\Users\cBourneville\Downloads\Advent of Code\Inputs\3.txt");
	//var inputs = new string[] {"R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83"};
	var wire1 = inputs[0].Split(',');
	var wire2 = inputs[1].Split(',');
	
	var path1 = CalculatePath(wire1);
	var path2 = CalculatePath(wire2);
	
	var intersections = path1.Intersect(path2);	
	
	var answer = GetDistance(intersections, path1, path2);

	stopwatch.Stop();
	$"Shortest distance: {answer}\nElapsed: {stopwatch.Elapsed}".Dump();
}

// Define other methods and classes here
List<System.Drawing.Point> CalculatePath(IEnumerable<string> instructions)
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

int GetDistance(IEnumerable<System.Drawing.Point> intersections, List<System.Drawing.Point> path1, List<System.Drawing.Point> path2)
{
	var minDistance = int.MaxValue;
	
	foreach (var intersection in intersections)
	{
		$"Intersection of {intersection.X}, {intersection.Y} at distance {(path1.IndexOf(intersection) + 1) + (path2.IndexOf(intersection) + 1)} - {path1.IndexOf(intersection) + 1}::{path2.IndexOf(intersection) + 1}".Dump();
		if ((path1.IndexOf(intersection) + 1) + (path2.IndexOf(intersection) + 1) < minDistance)
			minDistance = (path1.IndexOf(intersection) + 1) + (path2.IndexOf(intersection) + 1);
	}
	
	return minDistance;
}