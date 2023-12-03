using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day3 : IDay<Line>
    {
        public long A(List<Line> inputs)
        {
            List<Part> parts = [];
            for (int i = 0; i < inputs.Count; i++)
            {
                foreach (var symbol in inputs[i].Symbols)
                {
                    if (i > 0)
                    {
                        parts.AddRange(inputs[i - 1].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    }
                    parts.AddRange(inputs[i].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    if (i + 1 < inputs.Count)
                    {
                        parts.AddRange(inputs[i + 1].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    }
                }
            }

            parts = parts.DistinctBy(x => new
            {
                x.Number,
                x.Line,
            }).ToList();

            return parts.Select(p => p.Number).Sum();
        }

        public long B(List<Line> inputs)
        {
            long gearSum = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                foreach (var symbol in inputs[i].Symbols.Where(s => s.Character == '*'))
                {
                    List<Part> parts = [];
                    if (i > 0)
                    {
                        parts.AddRange(inputs[i - 1].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    }
                    parts.AddRange(inputs[i].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    if (i + 1 < inputs.Count)
                    {
                        parts.AddRange(inputs[i + 1].Parts.Where(p => p.Indexes.Contains(symbol.Index)));
                    }
                    if (parts.Count == 2)
                    {
                        gearSum += parts[0].Number * parts[1].Number;
                    }
                }
            }
            return gearSum;
        }

        public List<Line> SetupInputs(string[] inputs)
        {
            return inputs.Select((l, i) =>
            {
                var line = new Line();

                var index = -1;
                var parsingInt = false;
                var num = "";
                List<int> numIndexes = [];
                while (l.Length > 0 && index++ < l.Length)
                {
                    if (index == l.Length)
                    {
                        if (parsingInt)
                        {
                            numIndexes.Add(index);
                            line.Parts.Add(new Part
                            {
                                Number = int.Parse(num),
                                Indexes = [.. numIndexes],
                                Line = i,
                            });
                            numIndexes = [];
                            num = "";
                            parsingInt = false;
                        }
                        continue;
                    }

                    char c = '\0';
                    try
                    {
                        c = l[index];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                    if (c == '.' && parsingInt is false)
                    {
                        continue;
                    }
                    else if (char.IsDigit(c))
                    {
                        parsingInt = true;
                        num += c;
                        numIndexes.Add(index - 1);
                        numIndexes.Add(index);
                        numIndexes = numIndexes.Distinct().Where(x => x >= 0).ToList();
                        continue;
                    }
                    else
                    {
                        if (parsingInt)
                        {
                            numIndexes.Add(index);
                            line.Parts.Add(new Part
                            {
                                Number = int.Parse(num),
                                Indexes = [.. numIndexes],
                                Line = i,
                            });
                            numIndexes = [];
                            num = "";
                            parsingInt = false;
                        }
                        if (c == '.')
                        {
                            continue;
                        }
                        else
                        {
                            line.Symbols.Add(new Symbol
                            {
                                Character = c,
                                Index = index,
                            });
                        }
                    }
                }

                return line;
            }).ToList();
        }
    }

    public record Line
    {
        public List<Part> Parts { get; init; } = [];
        public List<Symbol> Symbols { get; init; } = [];
    }

    public record Part
    {
        public int Number { get; init; }
        public int Line { get; init; }
        public int[] Indexes { get; init; } = [];
    }

    public record Symbol
    {
        public char Character { get; init; }
        public int Index { get; init; }
    }
}
