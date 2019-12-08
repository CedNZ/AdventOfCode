<Query Kind="Program" />

const int WIDTH = 25;
const int HEIGHT = 6;
const int PIXELS = WIDTH * HEIGHT;

void Main()
{
	var input = System.IO.File.ReadAllText(@"C:\Users\cbour\Documents\Code\AdventOfCode\2019\Inputs\8.txt");
	//var input = "123456789012";
	var counter = 0;
	var numLayers = input.Length / PIXELS;
	
	int[][,] Layers = new int[numLayers][,];
	
	for (int i = 0; i < numLayers; i++) 
	{
		Layers[i] = new int[WIDTH,HEIGHT];
		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++) 
			{
				Layers[i][x,y] = int.Parse(input[counter].ToString());
				counter++;
			}
		}
	}
	
	//Draw(Layers, numLayers);
}

// Define other methods, classes and namespaces here
void Draw(int[][,] Layers, int numLayers)
{
	string output = "";
	for (int i = 0; i < numLayers; i++)
	{
		output = $"Layer {i}: ";
		
		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				output += $"{Layers[i][x, y]}";
			}
			
			output += "\n";
			output += new string(' ', $"Layer {i}: ".Length);
		}
		output.Dump();
	}
}