using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class ConstantFunctionAlgorithm : IAlgorithm
    {
        public int Id => 3;
        public string Name => "Константная функция";
        public string BigO => "O(1)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var array = GenerateArray(n);
            LastStepCount = 1;
            LastResult = array.Length > 0 ? array[0] : 0;
        }

        private int[] GenerateArray(int n)
        {
            var rnd = new Random(42);
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rnd.Next(1, 100);
            return arr;
        }
    }
}