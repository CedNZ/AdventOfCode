<Query Kind="Program" />

void Main()
{
	var stopwatch = Stopwatch.StartNew();
	/**/
	//Example 1
	Moon Io = new Moon(-1, 0, 2, "Io");
	Moon Europa = new Moon(2, -10, -7, "Europa");
	Moon Ganymede = new Moon(4, -8, 8, "Ganymede");
	Moon Callisto = new Moon(3, 5, -1, "Callisto");
	/**/
	/*
	//Example 2
	Moon Io = new Moon(-8, -10, 0, "Io");
	Moon Europa = new Moon(5, 5, 10, "Europa");
	Moon Ganymede = new Moon(2, -7, 3, "Ganymede");
	Moon Callisto = new Moon(9, -8, -3, "Callisto");
	/**/
	/*
	// Real Input
	Moon Io = new Moon(14, 15, -2, "Io");
	Moon Europa = new Moon(17, -3, 4, "Europa");
	Moon Ganymede = new Moon(6, 12, -13, "Ganymede");
	Moon Callisto = new Moon(-2, 10, -8, "Callisto");
	/**/

	List<Moon> moons = new List<Moon> { Io, Europa, Ganymede, Callisto };
	
	var pairs = from moon in moons
				from otherMoon in moons.Where(m => m != moon)
				select (moon, otherMoon);

	int count = 0;
	bool searching = true;
	
	int cycleX = -1;
	int cycleY = -1;
	int cycleZ = -1;
	
	while (searching)
	{
		count++;
		
		foreach (var moonPair in pairs)
		{
			moonPair.moon.ApplyGravity(moonPair.otherMoon);
		}

		foreach (var moon in moons)
		{
			moon.Move();
		}
		
		if (cycleX == -1 && moons.All(m => m.vX == 0))
		{
			cycleX = count * 2;
		}
		if (cycleY == -1 && moons.All(m => m.vY == 0))
		{
			cycleY = count * 2;
		}
		if (cycleZ == -1 && moons.All(m => m.vZ == 0))
		{
			cycleZ = count * 2;
		}
		
		if (cycleX > 0 && cycleY > 0 && cycleZ > 0)
		{
			searching = false;
		}
	}

	var totalEnergy = moons.Sum(m => m.Energy);
	$"Repeats at X: {cycleX}, Y: {cycleY}, Z: {cycleZ}\nLCM: {LCM(cycleX, LCM(cycleY, cycleZ))}".Dump();
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

static int GCD(int a, int b)
{
	if (a % b == 0) return b;
	return GCD(b, a % b);
}

static int LCM(int a, int b)
{
	return a * b / GCD(a, b);
}