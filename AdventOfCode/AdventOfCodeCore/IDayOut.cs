namespace AdventOfCodeCore
{
    public interface IDayOut<TIn, TOut>
    {
        public List<TIn> SetupInputs(string[] inputs);
        public TOut? A(List<TIn> inputs)
        {
            return default;
        }
        public TOut? B(List<TIn> inputs)
        {
            return default;
        }
    }
}
