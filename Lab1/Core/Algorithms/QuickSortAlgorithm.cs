using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class QuickSortAlgorithm : IAlgorithm
    {
        public int Id => 2;
        public string Name => "Быстрая сортировка";
        public string BigO => "O(n log n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }
        
        private long _currentSteps;

        public void Run(int n)
        {
            int[] array = GenerateArray(n);
            _currentSteps = 0;
            
            if (n > 0)
                QuickSort(array, 0, n - 1);
            
            LastStepCount = _currentSteps;
            LastResult = array;
        }

        private void QuickSort(int[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];

            while (i <= j)
            {
                while (array[i] < pivot) { i++; _currentSteps++; }
                while (array[j] > pivot) { j--; _currentSteps++; }
                if (i <= j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    _currentSteps++;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j) QuickSort(array, leftIndex, j);
            if (i < rightIndex) QuickSort(array, i, rightIndex);
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