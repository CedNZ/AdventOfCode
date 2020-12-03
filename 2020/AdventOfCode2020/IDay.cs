using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public interface IDay<T>
    {
        public List<T> SetupInputs(string[] inputs);
        public long A(List<T> inputs);
        public long B(List<T> inputs);
    }
}
