using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    /// <summary>
    /// Обёртка над встроенной сортировкой .NET (внутри использует гибридные алгоритмы, близкие к O(n log n)).
    /// </summary>
    public class BuiltInSortAlgorithm : IAlgorithm
    {
        public int Id => 8;
        public string Name => "Встроенная сортировка (Array.Sort)";
        public string BigO => "O(n log n)";
        
        // Для Array.Sort мы не можем посчитать шаги напрямую внутри чужого кода, 
        // поэтому оставляем null (или можно возвращать эмпирическую константу, если требуют)
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var array = GenerateArray(n);
            
            Array.Sort(array);

            LastStepCount = null; 
            LastResult = array;
        }

        private int[] GenerateArray(int n)
        {
            var rnd = new Random(42);
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rnd.Next(1, 10000);
            return arr;
        }
    }
}