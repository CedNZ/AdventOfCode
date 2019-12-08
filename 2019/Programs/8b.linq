<Query Kind="Program" />

const int WIDTH = 2;
const int HEIGHT = 2;
const int PIXELS = WIDTH * HEIGHT;

void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	//var input = System.IO.File.ReadAllText(@"C:\Users\cbour\Documents\Code\AdventOfCode\2019\Inputs\8.txt");
	var input = "0222112222120000";
	var counter = 0;
	var numLayers = input.Length / PIXELS;

	List<List<Pixel>> Layers = new List<List<Pixel>>(numLayers);

	for (int i = 0; i < numLayers; i++)
	{
		Layers.Add(new List<Pixel>(PIXELS));
		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				Layers[i].Add(new Pixel(x, y, int.Parse(input[counter].ToString())));
				counter++;
			}
		}
	}

	List<Pixel> final = Layers.Aggregate((curr, next) => 
	{
		List<Pixel> aggregate = new List<Pixel>();
		for (int i = 0; i < next.Count; i++)
		{
			var currentPixel = curr[i];
			var nextPixel = next[i];
			
			aggregate.Add(currentPixel.Value switch
			{
				0 => currentPixel,
				1 => currentPixel,
				2 => nextPixel,
				_ => currentPixel
			});
		}
		
		return aggregate;
	});
	
	Draw(final);

	stopwatch.Stop();
	$"Time taken: {stopwatch.Elapsed}".Dump();
	
}

// Define other methods, classes and namespaces here
void Draw(List<List<Pixel>> Layers, int numLayers)
{
	for (int i = 0; i < numLayers; i++)
	{
		Draw(Layers[i], i);
	}
}

void Draw(List<Pixel> layer, int layerNum = 0)
{
	var output = $"Layer {layerNum}: ";

	for (int y = 0; y < HEIGHT; y++)
	{
		for (int x = 0; x < WIDTH; x++)
		{
			output += layer.First(p => p.X == x && p.Y == y).Value;
		}

		output += "\n";
		output += new string(' ', $"Layer {layerNum}: ".Length);
	}
	output.Dump();
}

class Pixel
{
	public int X;
	public int Y;
	public int Value;

	public Pixel(int x, int y, int v)
	{
		X = x;
		Y = y;
		Value = v;
	}
}