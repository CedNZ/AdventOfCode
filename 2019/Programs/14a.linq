<Query Kind="Program" />

void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	//var input = System.IO.File.ReadAllLines(@"C:\Users\cBourneville\Code\AdventOfCode\2019\Inputs\7.txt");
	var input = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL".Split("\n");

	Dictionary<Fuel, int> fuels = new Dictionary<Fuel, int>();

	foreach (var line in input)
	{
		var parts = line.Split("=>");
		var name = parts[1].Trim().Split(' ').Last();
		var units = int.Parse(parts[1].Trim().Split(' ').First());

		var currentFuel = fuels.FirstOrDefault(x => x.Key.Name == name).Key;
		if (currentFuel == null)
		{
			currentFuel = new Fuel(name, units);
			fuels.Add(currentFuel, units);
		}
			
		foreach (var element in parts[0].Split(','))
		{
			var ingredientName = element.Trim().Split(' ').Last();
			var ingredientUnits = int.Parse(element.Trim().Split(' ').First());


			var ingredient = fuels.FirstOrDefault(x => x.Name == ingredientName);
			if (ingredient == null)
			{
				ingredient = new Fuel(ingredientName, ingredientUnits);
				fuels.Add(ingredient);
			}
				
			fuel.AddIngredient(ingredient);
		}
	}
	
	var fuel = fuels.First(f => f.Name == "FUEL");
	
	while (fuel.Ingredients.Any(x => x.Name != "ORE")
	{
		var ingredients = fuel.Ingredients.ToList();
		fuel.Ingredients.Clear();
		
		foreach (var ingredient in ingredients)
		{
			
		}
	}
}

// Define other methods, classes and namespaces here
class Fuel
{
	public string Name;
	public int Units;
	public List<Fuel> Ingredients;
	
	public Fuel(string name, int units)
	{
		Name = name;
		Units = units;
		Ingredients = new List<Fuel>();
	}
	
	public void AddIngredient(Fuel fuel)
	{
		Ingredients.Add(fuel);
	}
}