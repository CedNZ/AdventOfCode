using AdventOfCodeCore;

namespace AoC_2024
{
    public class Day9 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            var text = inputs.Single();

            Dictionary<int, (int start, int size)> files = [];
            Queue<(int start, int size)> free = [];

            var space = false;
            var fileIndex = 0;
            var diskIndex = 0;
            foreach (var c in text)
            {
                var n = c - '0';
                if (!space)
                {
                    files[fileIndex++] = (diskIndex, n);
                }
                else
                {
                    free.Enqueue((diskIndex, n));
                }
                space = !space;
                diskIndex += n;
            }

            List<long> result = [];
            var newIndex = 0;
            while (free.Any())
            {
                var f = free.Dequeue();
                while (newIndex < f.start)
                {
                    var file = files.FirstOrDefault(x => x.Value.start >= newIndex || (x.Value.start < newIndex && (x.Value.start + x.Value.size) > newIndex));
                    result.Add(file.Key);
                    newIndex++;
                }
                while (newIndex < f.start + f.size && files.Any())
                {
                    var file = files.LastOrDefault();
                    result.Add(file.Key);
                    var remaining = file.Value.size - 1;
                    if (remaining == 0)
                    {
                        files.Remove(file.Key);
                    }
                    else
                    {
                        files[file.Key] = (file.Value.start, remaining);
                    }
                    newIndex++;
                    free = new Queue<(int start, int size)>(free.Where(x => x.start <= file.Value.start + remaining).ToList());
                }
            }

            var remainingFiles = files.FirstOrDefault(x => x.Value.start >= newIndex || (x.Value.start < newIndex && (x.Value.start + x.Value.size) > newIndex));
            while (newIndex < remainingFiles.Value.start + remainingFiles.Value.size)
            {
                var remaining = remainingFiles.Value.size - 1;
                if (remaining == 0)
                {
                    files.Remove(remainingFiles.Key);
                }
                else
                {
                    files[remainingFiles.Key] = (remainingFiles.Value.start, remaining);
                }
                result.Add(remainingFiles.Key);
                newIndex++;
            }
            return result.Select((x, i) => x * i).Sum();
        }

        public long B(List<string> inputs)
        {
            var text = inputs.Single();

            Dictionary<int, (int start, int size)> files = [];
            Dictionary<int, (int start, int size)> free = [];
            List<long?> result = [];

            var space = false;
            var fileIndex = 0;
            var freeIndex = 0;
            var diskIndex = 0;
            foreach (var c in text)
            {
                var n = c - '0';
                if (!space)
                {
                    files[fileIndex] = (diskIndex, n);
                    for (int i = 0; i < n; i++)
                    {
                        result.Add(fileIndex);
                    }
                    fileIndex++;
                }
                else
                {
                    free[freeIndex++] = (diskIndex, n);
                    for (int i = 0; i < n; i++)
                    {
                        result.Add(null);
                    }
                }
                space = !space;
                diskIndex += n;
            }

            var lastFileIndex = files.Last().Key;
            while (lastFileIndex >= 0)
            {
                var file = files[lastFileIndex];
                var f = free.FirstOrDefault(x => x.Value.size >= file.size && x.Value.start < file.start);
                var empty = f.Value;

                if (f.Key is 0 && f.Value is (0, 0))
                {

                }
                else
                {
                    for (int s = 0; s < file.size; s++)
                    {
                        result[f.Value.start + s] = lastFileIndex;
                        var index = result.LastIndexOf(lastFileIndex);
                        result[index] = null;
                        empty = (empty.start + 1, empty.size - 1);
                    }
                    free[f.Key] = empty;
                }
                lastFileIndex--;
            }

            return result.Select((x, i) => x * i).Where(x => x.HasValue).Select(x => x!.Value).Sum();
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }
}
