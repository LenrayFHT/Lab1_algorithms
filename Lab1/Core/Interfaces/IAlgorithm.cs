namespace Lab1.Core.Interfaces
{
    public interface IAlgorithm
    {
        int Id { get; }
        string Name { get; }
        string BigO { get; }
        long? LastStepCount { get; }
        
        /// <summary>
        /// Сохраняется результат работы алгоритма, 
        /// чтобы JIT-компилятор не вырезал "бесполезные" вычисления.
        /// </summary>
        object? LastResult { get; }

        void Run(int n);
    }
}