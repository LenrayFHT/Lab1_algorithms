using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class SumVectorAlgorithm : IAlgorithm
    {
        public int Id => 4;
        public string Name => "Сумма элементов";
        public string BigO => "O(n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var array = GenerateArray(n);
            long steps = 0;
            long sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
                steps++;
            }

            LastStepCount = steps;
            LastResult = sum;
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