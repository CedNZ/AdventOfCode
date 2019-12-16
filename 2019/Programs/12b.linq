<Query Kind="Program" />

void Main()
{
	var stopwatch = Stopwatch.StartNew();
	/*
	<x=14, y=15, z=-2>
	<x=17, y=-3, z=4>
	<x=6, y=12, z=-13>
	<x=-2, y=10, z=-8>
	*/
	Moon Io = new Moon(-1, 0, 2, "Io");
	Moon Europa = new Moon(2, -10, -7, "Europa");
	Moon Ganymede = new Moon(4, -8, 8, "Ganymede");
	Moon Callisto = new Moon(3, 5, -1, "Callisto");
	/*
	Moon Io = new Moon(14, 15, -2, "Io");
	Moon Europa = new Moon(17, -3, 4, "Europa");
	Moon Ganymede = new Moon(6, 12, -13, "Ganymede");
	Moon Callisto = new Moon(-2, 10, -8, "Callisto");
	*/

	List<Moon> moons = new List<Moon> { Io, Europa, Ganymede, Callisto };
	List<string> history = new List<string>();
	
	var pairs = from moon in moons
				from otherMoon in moons.Where(m => m != moon)
				select (moon, otherMoon);

	Func<string> moonState = () => $"{Io.ToString()}\n{Europa.ToString()}\n{Ganymede.ToString()}\n{Callisto.ToString()}";
	int count = 0;
	
	while (!history.Contains(moonState()))
	{
		count++;
		history.Add(moonState());
		
		foreach (var moonPair in pairs)
		{
			moonPair.moon.ApplyGravity(moonPair.otherMoon);
		}

		foreach (var moon in moons)
		{
			moon.Move();
		}
	}
	
	var totalEnergy = moons.Sum(m => m.Energy);
	stopwatch.Stop();
	$"Total Energy at end: {totalEnergy}\nRepeats at: {count}\nElapsed: {stopwatch.Elapsed}".Dump();
}

// Define other methods, classes and namespaces here
class Moon
{
	public int X, Y, Z;
	public int vX, vY, vZ;
	public string Name;
	
	public Moon(int x, int y, int z, string name)
	{
		X = x;
		Y = y;
		Z = z;
		
		Name = name;

		vX = 0;
		vY = 0;
		vZ = 0;
	}
	
	public void ApplyGravity(Moon other)
	{
		if (X > other.X)
			vX--;
		if (X < other.X)
			vX++;

		if (Y > other.Y)
			vY--;
		if (Y < other.Y)
			vY++;

		if (Z > other.Z)
			vZ--;
		if (Z < other.Z)
			vZ++;

	}
	
	public void Move()
	{
		X += vX;
		Y += vY;
		Z += vZ;
	}
	
	public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
	
	public int KineticEnergy => Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);
	
	public int Energy => PotentialEnergy * KineticEnergy;

	public override string ToString()
	{
		return $"pos=<x= {X}, y=  {Y}, z=  {Z}>, vel=<x=  {vX}, y=  {vY}, z=  {vZ}>";
	}
}

