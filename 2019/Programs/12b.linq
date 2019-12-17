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
	Dictionary<double, List<string>> states = new Dictionary<double, System.Collections.Generic.List<string>>();
	
	var pairs = from moon in moons
				from otherMoon in moons.Where(m => m != moon)
				select (moon, otherMoon);

	Func<string> moonState = () => $"{Io.ToString()}\n{Europa.ToString()}\n{Ganymede.ToString()}\n{Callisto.ToString()}";
	int count = 0;
	var totalEnergy = moons.Sum(m => m.Energy);
	
	var foundState = false;
	int countX = 0;
	int countY = 0;
	int countZ = 0;
	bool foundX = false;
	bool foundY = false;
	bool foundZ = false;
	Dimension? currentDimension = null;
	
	while (!foundX || !foundY || !foundZ)
	{
		currentDimension = !foundX 
			? Dimension.X
			: !foundY 
				? Dimension.Y
				: Dimension.Z;
		
		totalEnergy = moons.Sum(m => m.Energy);
		
		var state = moonState();
		
		if (states.ContainsKey(totalEnergy)) {
			if (states[totalEnergy].Contains(state))
			{				
				$"Repeated state:\n{state}\n".Dump();
				switch (currentDimension)
				{
					case Dimension.X:
						foundX = true;
						break;
					case Dimension.Y:
						foundY = true;
						break;
					case Dimension.Z:
						foundZ = true;
						break;
					default:
						count++;
						break;
				}

				Io.Reset();
				Europa.Reset();
				Ganymede.Reset();
				Callisto.Reset();

				states = new Dictionary<double, System.Collections.Generic.List<string>>();
				states.Add(totalEnergy, new List<string> { moonState() });
				currentDimension = !foundX
					? Dimension.X
					: !foundY
						? Dimension.Y
						: Dimension.Z;
			}
			else
			{
				states[totalEnergy].Add(state);
			}
		}
		else
		{
			states.Add(totalEnergy, new List<string> { state });
		}

		//moonState().Dump();
		//$"Total Energy: {moons.Sum(m => m.Energy)}".Dump();
		//$"----------------------------------------------{count}----------------------------------------------".Dump();
		
		foreach (var moonPair in pairs)
		{
			moonPair.moon.ApplyGravity(moonPair.otherMoon, currentDimension);
		}

		foreach (var moon in moons)
		{
			moon.Move(currentDimension);
		}
		
		switch (currentDimension) 
		{
			case Dimension.X:
				countX++;
				break;
			case Dimension.Y:
				countY++;
				break;
			case Dimension.Z:
				countZ++;
				break;
			default:
				count++;
				break;
		}
		$"CountX : {countX}, CountY : {countY}, CountZ: {countZ}".Dump();
	}
	
	$"X repeats in {countX}\nY repeats in {countY}\nZ repeats in {countZ}".Dump();
	$"LCM: {LCM(countX, LCM(countY, countZ))}".Dump();
	
	
	stopwatch.Stop();
	$"Total Energy at end: {totalEnergy}\nRepeats at: {count}\nElapsed: {stopwatch.Elapsed}".Dump();
}

// Define other methods, classes and namespaces here
class Moon
{
	public double X, Y, Z;
	public double vX, vY, vZ;
	public string Name;
	private readonly double initialX, initialY, initialZ;
	
	public Moon(int x, int y, int z, string name)
	{
		X = x;
		Y = y;
		Z = z;
		
		Name = name;

		vX = 0;
		vY = 0;
		vZ = 0;
		
		initialX = x;
		initialY = y;
		initialZ = z;		
	}
	
	public void Reset() 
	{
		X = initialX;
		Y = initialY;
		Z = initialZ;

		vX = 0;
		vY = 0;
		vZ = 0;
	}
	
	public void ApplyGravity(Moon other, Dimension? dimension = null)
	{
		if (dimension == null || dimension == Dimension.X)
		{
			if (X > other.X)
				vX--;
			if (X < other.X)
				vX++;
		}

		if (dimension == null || dimension == Dimension.Y)
		{
			if (Y > other.Y)
				vY--;
			if (Y < other.Y)
				vY++;
		}

		if (dimension == null || dimension == Dimension.Z)
		{
			if (Z > other.Z)
				vZ--;
			if (Z < other.Z)
				vZ++;
		}
	}
	
	public void Move(Dimension? dimension = null)
	{
		if (dimension == null || dimension == Dimension.X)
		{
			X += vX;
		}

		if (dimension == null || dimension == Dimension.Y)
		{
			Y += vY;
		}

		if (dimension == null || dimension == Dimension.Y)
		{
			Z += vZ;
		}
		}
	
	public double PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
	
	public double KineticEnergy => Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ);
	
	public double Energy => PotentialEnergy * KineticEnergy;

	public override string ToString()
	{
		return $"pos=<x= {X}, y=  {Y}, z=  {Z}>, vel=<x=  {vX}, y=  {vY}, z=  {vZ}>, energy=<pot= {PotentialEnergy}, kin= {KineticEnergy}, tot= {Energy}>";
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

enum Dimension
{
	X = 1,
	Y = 2,
	Z = 3
}