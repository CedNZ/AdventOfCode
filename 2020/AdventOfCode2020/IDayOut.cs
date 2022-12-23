using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public interface IDayOut<TIn, TOut>
    {
        public List<TIn> SetupInputs(string[] inputs);
        public TOut A(List<TIn> inputs);
        public TOut B(List<TIn> inputs);
    }
}
