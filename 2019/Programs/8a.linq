<Query Kind="Program" />

const int WIDTH = 25;
const int HEIGHT = 6;
const int PIXELS = WIDTH * HEIGHT;

void Main()
{
	var stopwatch = System.Diagnostics.Stopwatch.StartNew();
	var input = System.IO.File.ReadAllText(@"C:\Users\cbour\Documents\Code\AdventOfCode\2019\Inputs\8.txt");
	//var input = "123456789012";
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
	
	//Draw(Layers, numLayers);
	var checksumLayer = Layers.OrderBy(l => l.Count(p => p.Value == 0)).First();
	
	int checksum = checksumLayer.Count(p => p.Value == 1) * checksumLayer.Count(p => p.Value == 2);

	stopwatch.Stop();
	$"Answer: {checksum}, Time taken: {stopwatch.Elapsed}".Dump();
	
}

// Define other methods, classes and namespaces here
void Draw(List<List<Pixel>> Layers, int numLayers)
{
	string output = "";
	for (int i = 0; i < numLayers; i++)
	{
		output = $"Layer {i}: ";
		
		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				output += Layers[i].First(p => p.X == x && p.Y == y).Value;
			}

			output += "\n";
			output += new string(' ', $"Layer {i}: ".Length);
		}
		output.Dump();
	}
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