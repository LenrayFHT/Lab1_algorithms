using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class ProductVectorAlgorithm : IAlgorithm
    {
        public int Id => 5;
        public string Name => "Произведение элементов";
        public string BigO => "O(n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var array = GenerateArray(n);
            long steps = 0;
            double product = 1.0; // double, чтобы избежать мгновенного переполнения long

            for (int i = 0; i < array.Length; i++)
            {
                product *= array[i];
                steps++;
            }

            LastStepCount = steps;
            LastResult = product;
        }

        private int[] GenerateArray(int n)
        {
            var rnd = new Random(42);
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rnd.Next(1, 10);
            return arr;
        }
    }
}