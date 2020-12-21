using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public interface IDayOut<Tin, Tout>
    {
        public List<Tin> SetupInputs(string[] inputs);
        public Tout A(List<Tin> inputs);
        public Tout B(List<Tin> inputs);
    }
}
