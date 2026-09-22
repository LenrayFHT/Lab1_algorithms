using System;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class MatrixMultiplicationAlgorithm : IAlgorithm
    {
        public int Id => 9;
        public string Name => "Умножение матриц";
        public string BigO => "O(n^3)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        // Дополнительные параметры для неквадратных матриц
        public int? CustomM { get; set; }
        public int? CustomP { get; set; }

        public void Run(int n)
        {
            // Если кастомные размеры не заданы, используем матрицы N x N
            int m = CustomM ?? n;
            int p = CustomP ?? n;

            var rnd = new Random(42);
            double[,] A = new double[n, m];
            double[,] B = new double[m, p];

            // Генерация матриц
            for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++) A[i, j] = rnd.NextDouble();

            for (int i = 0; i < m; i++)
            for (int j = 0; j < p; j++) B[i, j] = rnd.NextDouble();

            double[,] C = new double[n, p];
            long steps = 0;

            // Умножение C = A * B
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += A[i, k] * B[k, j];
                        steps++; // Считаем только базовые операции умножения
                    }
                    C[i, j] = sum;
                }
            }

            LastStepCount = steps;
            LastResult = C;
        }
    }
}