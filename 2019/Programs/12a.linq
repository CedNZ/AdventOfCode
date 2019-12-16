<Query Kind="Program" />

void Main()
{
	/*
	<x=-1, y=0, z=2>
	<x=2, y=-10, z=-7>
	<x=4, y=-8, z=8>
	<x=3, y=5, z=-1>	
	*/
	
	Moon Io = new Moon(-1, 0, 2, "Io");
	Moon Europa = new Moon(2, -10, -7, "Europa");
	Moon Ganymede = new Moon(4, -8, 8, "Ganymede");
	Moon Callisto = new Moon(3, 5, -1, "Callisto");

	List<Moon> moons = new List<Moon> { Io, Europa, Ganymede, Callisto };
	
	var pairs = from moon in moons
				from otherMoon in moons.Where(m => m != moon)
				select (moon, otherMoon);
	
	for (int i = 0; i < 10; i++)
	{
		foreach (var moonPair in pairs)
		{
			moonPair.moon.ApplyGravity(moonPair.otherMoon);
		}

		foreach (var moon in moons)
		{
			moon.Move();
		}
		
		moons.Dump();
	}
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
}

