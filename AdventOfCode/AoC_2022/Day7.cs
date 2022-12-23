using System.Drawing;
using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day7 : IDay<string[]>
    {
        public long A(List<string[]> inputs)
        {
            BuildFS(inputs);

            var limit = 100000;
            return AllFiles.Where(f => f.TotalSize() < limit && f.Size == 0).Sum(f => f.TotalSize());
        }

        internal void BuildFS(List<string[]> inputs)
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
                        while (i < inputs.Count - 1 && inputs[i + 1][0] != "$")
                        {
                            i++;
                            if (inputs[i][0] == "dir")
                            {
                                if (CurrentDir.Files.All(f => f.Name != inputs[i][1]))
                                {
                                    var file = new File
                                    {
                                        Files = new(),
                                        Folder = CurrentDir,
                                        Name = inputs[i][1]
                                    };
                                    CurrentDir.Files.Add(file);
                                    AllFiles.Add(file);
                                }
                            }
                            else
                            {
                                if (CurrentDir.Files.All(f => f.Name != inputs[i][1]))
                                {
                                    var file = new File
                                    {
                                        Files = null,
                                        Folder = CurrentDir,
                                        Name = inputs[i][1],
                                        Size = int.Parse(inputs[i][0])
                                    };
                                    CurrentDir.Files.Add(file);
                                    AllFiles.Add(file);
                                }
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
        }

        static File _root = new File
        {
            Files = new(),
            Name = "root",
            Size = 0,
            Folder = null,
        };

        File CurrentDir { get; set; } = _root;

        private static List<File> _allFiles;
        List<File> AllFiles 
        { 
            get => _allFiles ??= new List<File>(); 
            set
            {
                if (value is not null)
                {
                    _allFiles = value;
                }
            }
        }

        public long B(List<string[]> inputs)
        {
            if (AllFiles.Count == 0)
            {
                BuildFS(inputs);
            }

            var totalAvailable = 70_000_000;
            var need = 30_000_000;

            var taken = _root.TotalSize();
            var unused = totalAvailable - taken;

            var missing = need - unused;

            var toDelete = AllFiles
                .Where(x => x.Files != null)
                .OrderBy(x => x.TotalSize())
                .FirstOrDefault(x => x.TotalSize() > missing);

            return toDelete.TotalSize();
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

            public override string ToString()
            {
                if (Files is null)
                {
                    return $"{Size} {Name}";
                }
                return $"dir {Name} {TotalSize()}";
            }
        }
    }
}
