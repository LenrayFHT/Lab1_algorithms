using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class PolynomialHornerAlgorithm : IAlgorithm
    {
        public int Id => 7;
        public string Name => "Полином (Горнер)";
        public string BigO => "O(n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var coefficients = GenerateArray(n);
            if (n == 0)
            {
                LastResult = 0;
                LastStepCount = 0;
                return;
            }

            double x = 1.5;
            double result = coefficients[n - 1];
            long steps = 0;

            for (int i = n - 2; i >= 0; i--)
            {
                result = result * x + coefficients[i];
                steps++; 
            }

            LastStepCount = steps;
            LastResult = result;
        }

        private double[] GenerateArray(int n)
        {
            var rnd = new Random(42);
            var arr = new double[n];
            for (int i = 0; i < n; i++) arr[i] = rnd.NextDouble() * 10;
            return arr;
        }
    }
}