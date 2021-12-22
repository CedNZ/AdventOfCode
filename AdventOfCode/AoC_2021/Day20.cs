using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day20 : IDay<string>
    {
        string _imageEnhancement = "";
        List<Pixel> _litPixels;
        bool evenPass;

        public long A(List<string> inputs)
        {
            _imageEnhancement = inputs[0];
            _litPixels = new();

            for (int i = 2; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[i].Count(); j++)
                {
                    if (inputs[i][j] == '#')
                    {
                        _litPixels.Add(new Pixel(j, i - 2));
                    }
                }
            }
            
            _litPixels = Enhance();
            _litPixels = Enhance();

            return _litPixels.Count();
        }

        public List<Pixel> Enhance()
        {
            for (int i = 0; i < _litPixels.Count(); i++)
            {
                _litPixels[i].X += 2;
                _litPixels[i].Y += 2;
            }

            var buffer = new List<Pixel>();

            var maxX = _litPixels.Max(p => p.X) + 2;
            var maxY = _litPixels.Max(p => p.Y) + 2;

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (IsLit((x, y), maxX, maxY))
                    {
                        buffer.Add(new Pixel(x, y));
                    }
                }
            }

            evenPass = !evenPass;

            return buffer;
        }

        public bool IsLit((int X, int Y) pixel, int maxX, int maxY)
        {
            var num = 0;
            var position = 8;
            for (int i = pixel.Y - 1; i <= pixel.Y + 1; i++)
            {
                for (int j = pixel.X - 1; j <= pixel.X + 1; j++)
                {
                    if (_litPixels.Any(p => p.X == j && p.Y == i)) //another flag here for pass 2
                    {
                        num += 1 << position;
                    }
                    else if (evenPass && _imageEnhancement[0] == '#'  && ((i < 2 || j < 2) || (i > maxY - 2 || j > maxX - 2)))
                    {
                        num += 1 << position;
                    }
                    position--;
                }
            }

            return _imageEnhancement[num] == '#';
        }

        public void Draw()
        {
            var maxX = _litPixels.Max(p => p.X) + 2;
            var maxY = _litPixels.Max(p => p.Y) + 2;

            StringBuilder sb = new ();

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (_litPixels.Any(p => p.X == x && p.Y == y))
                    {
                        sb.Append('#');
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                sb.AppendLine(Environment.NewLine);
            }

            Console.WriteLine(sb.ToString());
        }

        public long B(List<string> inputs)
        {
            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }

    public class Pixel
    {
        public int X;
        public int Y;
        public Pixel(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
