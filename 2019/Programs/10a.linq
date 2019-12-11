<Query Kind="Program">
</Query>

void Main()
{
	var input = 
	@".#..#
	.....
	#####
	....#
	...##".Split('\n');
	
	List<Asteroid> asteroids = new List<Asteroid>();	
	
	for (int y = 0; y < input.Length; y++)
	{
		for (int x = 0; x < input[y].Length; x++)
		{
			if (input[y][x] == '#')
			{
				asteroids.Add(new Asteroid(x, y));
			}
		}
	}
	
	
	
}

// Define other methods and classes here
class Asteroid
{
	int X;
	int Y;
	Dictionary<Asteroid, (int relativeX, int relativeY)> relativeAsteroids;
	List<(Asteroid asteroid, double relativeMagnitude)> asteroidMagnitudeList;
	List<Asteroid> canSee;
	
	public Asteroid(int x, int y)
	{
		X = x;
		Y = y;
		relativeAsteroids = new Dictionary<Asteroid, (int relativeX, int relativeY)>();
		asteroidMagnitudeList = new List<(Asteroid asteroid, double relativeMagnitude)>();
		canSee = new List<Asteroid>();
	}
	
	public void AddRelatives(List<Asteroid> asteroids)
	{
		foreach (var asteroid in asteroids)
		{
			if (asteroid != this)
			{
				relativeAsteroids.Add(asteroid, RelativeToo(asteroid));
			}
		}
		foreach (var relativeAsteroid in relativeAsteroids.Keys)
		{
			var coords = relativeAsteroids[relativeAsteroid];
			
			var magnitude = Math.Sqrt(Math.Pow(Math.Abs(coords.relativeX), 2) + Math.Pow(Math.Abs(coords.relativeY), 2));
			
			asteroidMagnitudeList.Add((relativeAsteroid, magnitude));
		}
		asteroidMagnitudeList = asteroidMagnitudeList.OrderBy(x => x.relativeMagnitude).ToList();
	}
	
	public void CanSee()
	{
		foreach (var asteroid in asteroidMagnitudeList)
		{
			
		}
	}
	
	public (int relativeX, int relativeY) RelativeToo(Asteroid target)
	{		
		return (target.X - this.X, target.Y - this.Y);
	}
}