using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class BubbleSortAlgorithm : IAlgorithm
    {
        public int Id => 1;
        public string Name => "Сортировка пузырьком";
        public string BigO => "O(n^2)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            int[] array = GenerateArray(n);
            long steps = 0;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    steps++; 
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        steps++;
                    }
                }
            }

            LastStepCount = steps;
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