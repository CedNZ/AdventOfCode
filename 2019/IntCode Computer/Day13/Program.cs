using System;
using System.Collections.Generic;
using System.Linq;
using IntCode_Computer;
using System.Diagnostics;
using System.Windows.Input;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();

            var input = System.IO.File.ReadAllText(@"..\..\..\..\..\Inputs\13.txt");
            //var input = @"109, 1, 3, 3, 204, 2, 99";
            var program = input.Split(',').Select(x => double.Parse(x)).ToArray();

            program[0] = 2;

            IntcodeComputer computer = new IntcodeComputer(program, "ARCADE");

            Dictionary<(int x, int y), TileId> screen = new Dictionary<(int x, int y), TileId>();
            int score = 0;

            

            while (!computer.Halted)
            {
                computer.Run();

                while (computer.HasOutput)
                {
                    var x = (int)computer.GetOutput();
                    var y = (int)computer.GetOutput();
                    if(x == -1)
                    {
                        score = (int)computer.GetOutput();
                    }
                    else
                    {
                        var tileId = (TileId)((int)computer.GetOutput());


                        if(screen.ContainsKey((x, y)))
                        {
                            screen[(x, y)] = tileId;
                        }
                        else
                        {
                            screen.Add((x, y), tileId);
                        }
                    }
                }

                Paint<TileId>(screen);

                if (computer.AwaitingInput)
                {
                    var ballPos = screen.First(kvp => kvp.Value == TileId.Ball).Key;
                    var paddlePos = screen.First(kvp => kvp.Value == TileId.Paddle).Key;

                    int direction = 0;
                    if(paddlePos.x > ballPos.x)
                        direction = -1; //LEFT
                    if(paddlePos.x < ballPos.x)
                        direction = 1; //RIGHT


                    computer.QueueInput(direction);
                }
            }

            Paint<TileId>(screen);

            var blocks = screen.Count(p => p.Value == TileId.Block);

            stopwatch.Stop();

            Console.WriteLine($"Score: {score}\nBlocks: {blocks}, Elapsed: {stopwatch.Elapsed}");
        }

        public static void Paint<T>(Dictionary<(int x, int y), T> pixels) where T : Enum
        {
            int minX = pixels.Keys.Min(p => p.x);
            int maxX = pixels.Keys.Max(p => p.x);
            int minY = pixels.Keys.Min(p => p.y);
            int maxY = pixels.Keys.Max(p => p.y);

            //Console.WriteLine($"minX: {minX}, minY: {minY}\nmaxX: {maxX}, maxY: {maxY}");

            T[,] painting = new T[maxY + 1, maxX + 1];

            foreach(var position in pixels.Keys)
            {
                painting[position.y, position.x] = pixels[position];
            }

            Console.Clear();
            for(int y = 0; y <= maxY; y++)
            {
                string row = "";
                for(int x = 0; x <= maxX; x++)
                {
                    if(painting[y, x] is TileId colour)
                    {
                        row += Tile(colour);
                    }
                    else
                    {
                        row += " ";
                    }
                }
                Console.WriteLine(row);
            }
        }
        public static string Tile(TileId tile) => tile switch
        {
            TileId.Empty    => " ",
            TileId.Wall     => "╬",
            TileId.Block    => "░",
            TileId.Paddle   => "─",
            TileId.Ball     => "o",
            _               => " "
        };
    }

    enum TileId
    {
        Empty   = 0,
        Wall    = 1,
        Block   = 2,
        Paddle  = 3,
        Ball    = 4
    }
}
