using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class PolynomialNaiveAlgorithm : IAlgorithm
    {
        public int Id => 6;
        public string Name => "Полином (в лоб)";
        public string BigO => "O(n^2)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            var coefficients = GenerateArray(n);
            double x = 1.5;
            double result = 0;
            long steps = 0;

            for (int i = 0; i < n; i++)
            {
                double power = 1;
                for (int j = 0; j < i; j++)
                {
                    power *= x;
                    steps++; // Умножение для степени
                }
                result += coefficients[i] * power;
                steps++; // Сложение и умножение на коэффициент
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