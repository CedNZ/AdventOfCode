using AdventOfCodeCore;

namespace AoC_2022
{
    public class Day20 : IDay<(int num, bool moved)>
    {

        public long A(List<(int num, bool moved)> inputs)
        {
            var length = inputs.Count;

            var toMove = FindNextToMove(inputs);
            while (toMove != default)
            {
                var (item, index) = toMove;
                index += (item.num % length);
                while (index <= 0)
                {
                    index = inputs.Count + index;
                    index--;
                }
                if (index >= inputs.Count)
                {
                    index = (item.num % inputs.Count);
                    index--;
                }
                item.moved = true;
                inputs.Insert(index, item);
                toMove = FindNextToMove(inputs);
            }

            var zero = inputs.Find(x => x.num == 0);
            var zeroIndex = inputs.IndexOf(zero);
            var index1 = (zeroIndex + 1000) % length;
            var index2 = (zeroIndex + 2000) % length;
            var index3 = (zeroIndex + 3000) % length;

            return inputs[index1].num + inputs[index2].num + inputs[index3].num;
        }

        public ((int num, bool moved) item, int index) FindNextToMove(List<(int num, bool moved)> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].moved is false)
                {
                    var val = inputs[i];
                    inputs.RemoveAt(i);
                    return (val, i);
                }
            }
            return default;
        }

        public long B(List<(int num, bool moved)> inputs)
        {
            throw new NotImplementedException();
        }

        List<(int num, bool moved)> IDayOut<(int num, bool moved), long>.SetupInputs(string[] inputs)
        {
            return inputs.Select(x => (int.Parse(x), false)).ToList();
        }
    }
}
