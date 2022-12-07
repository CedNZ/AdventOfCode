using System.Drawing;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day7 : IDay<string[]>
    {
        public long A(List<string[]> inputs)
        {
            for (int i = 0; i < inputs.Count(); i++)
            {
                var line = inputs[i];

                if (line[0] == "$")
                {
                    if (line[1] == "cd")
                    {
                        if (line[2] == "/")
                        {
                            CurrentDir = _root;
                        }
                        else if (line[2] == "..")
                        {
                            CurrentDir = CurrentDir.Folder;
                        }
                        else
                        {
                            CurrentDir = CurrentDir.Files.Find(f => f.Name == line[2]);
                        }
                    }
                    else if (line[1] == "ls")
                    {
                        i++;
                        while (inputs[i][0] != "$")
                        {
                            if (inputs[i][0] == "dir")
                            {
                                if (CurrentDir.Files.Any(f => f.Name == inputs[i][1]))
                                {
                                    var file = new File
                                    {
                                        Files = new(),
                                        Folder = CurrentDir,
                                        Name = inputs[i][1]
                                    };
                                    CurrentDir.Files.Add(file);
                                }
                            }
                            else
                            {
                                if (CurrentDir.Files.Any(f => f.Name == inputs[i][1]))
                                {
                                    var file = new File
                                    {
                                        Files = null,
                                        Folder = CurrentDir,
                                        Name = inputs[i][1],
                                        Size = int.Parse(inputs[i][0])
                                    };
                                    CurrentDir.Files.Add(file);
                                }
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debugger.Break();
                }
            }

            var limit = 100000;
            return AllFiles.Where(f => f.TotalSize() < limit && f.Size == 0).Sum(f => f.TotalSize());
        }

        static File _root = new File
        {
            Files = new(),
            Name = "root",
            Size = 0,
            Folder = null,
        };

        File CurrentDir { get; set; } = _root;

        List<File> AllFiles { get; set; } = new();

        public long B(List<string[]> inputs)
        {
            return default;
        }

        public List<string[]> SetupInputs(string[] inputs)
        {
            return inputs.Select(x => x.Split(' ')).ToList();
        }

        internal class File
        {
            public string Name { get; set; }
            public int Size { get; set; }

            public File Folder { get; set; }

            public List<File> Files { get; set; }

            public int TotalSize() => Size > 0 ? Size : Files.Sum(f => f.TotalSize());
        }
    }
}
