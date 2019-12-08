<Query Kind="Program" />

void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var inputs = System.IO.File.ReadAllLines(@"C:\Users\cbour\Documents\Code\AdventOfCode\2019\Inputs\6.txt");

	Planetoid COM = new Planetoid();
	Dictionary<string, Planetoid> AllPlanets = new Dictionary<string, UserQuery.Planetoid>();
	AllPlanets.Add("COM", COM);
	
	foreach (var line in inputs)
	{
		var orbits = line.Trim().Split(')');
		if (AllPlanets.ContainsKey(orbits[0])) 
		{
			if (AllPlanets.ContainsKey(orbits[1]))
			{
				AllPlanets[orbits[0]].AddMoon(AllPlanets[orbits[1]]);
			}
			else
			{
				AllPlanets.Add(orbits[1], AllPlanets[orbits[0]].AddMoon(orbits[1]));
			}
		}
		else
		{
			if (AllPlanets.ContainsKey(orbits[1]))
			{
				var moon = AllPlanets[orbits[1]];
				if (moon.Parent != COM)
				{
					$"EDGE CASE {line}".Dump();
				}
				AllPlanets.Add(orbits[0], moon.Orbits(orbits[0], COM));
			}
			else
			{
				var first = new Planetoid(orbits[0], COM);
				var second = first.AddMoon(orbits[1]);
				AllPlanets.Add(first.Name, first);
				AllPlanets.Add(second.Name, second);				
			}
		}		
	}
	
	AllPlanets.Dump();
	
	AllPlanets.Sum(x => x.Value.Distance).Dump();
	stopwatch.Stop();
	stopwatch.Elapsed.Dump();
	
}

// Define other methods, classes and namespaces here
class Planetoid
{
	private Planetoid _parent;
	private List<Planetoid> _moons;
	private int _distance;
	
	public string Name { get; set; }
	
	public Planetoid Parent 
	{
		get => _parent; 
	}
	
	public int Distance 
	{
		get => _distance;
	}
	
	public Planetoid(string name, Planetoid parent)
	{
		Name = name;
		_parent = parent;
		_moons = new List<Planetoid>();
		_distance = parent.Distance + 1;
	}
	
	public Planetoid(string name, Planetoid parent, Planetoid moon)
	{
		Name = name;
		_parent = parent;
		_distance = moon.Distance;
		moon._distance++;
		
		moon._parent._moons.Remove(moon);
		
		moon._parent = this;		
		_moons = new List<Planetoid> { moon };
	}
	
	public Planetoid()
	{
		Name = "COM";
		_parent = null;
		_distance = 0;
		_moons = new List<Planetoid>();
	}
	
	public Planetoid AddMoon(string name)
	{
		var moon = new Planetoid(name, this);
		_moons.Add(moon);
		return moon;
	}
	
	public void AddMoon(Planetoid moon)
	{
		_moons.Add(moon);
	}
	
	public Planetoid Orbits(string name, Planetoid COM)
	{
		return new Planetoid(name, COM, this);
	}
}