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
                    if ()
                }
                else
                {

                }
            }
        }

        File root = new File
        {
            Files = new(),
            Name = "root",
            Size = 0,
            Folder = null,
        };

        public long B(List<string> inputs)
        {
            throw new NotImplementedException();
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
        }
    }
}
