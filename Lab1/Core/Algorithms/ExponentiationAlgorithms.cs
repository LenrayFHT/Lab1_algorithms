using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    public class PowerNaiveAlgorithm : IAlgorithm
    {
        public int Id => 10;
        public string Name => "Степень (Наивный)";
        public string BigO => "O(n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        public void Run(int n)
        {
            double x = 1.001; // Используем число, близкое к 1, чтобы избежать переполнения (Infinity)
            double result = 1.0;
            long steps = 0;

            for (int i = 0; i < n; i++)
            {
                result *= x;
                steps++; // Одно умножение за итерацию
            }

            LastStepCount = steps;
            LastResult = result;
        }
    }

    public class PowerRecursiveAlgorithm : IAlgorithm
    {
        public int Id => 11;
        public string Name => "Степень (Рекурсивный)";
        public string BigO => "O(n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        private long _steps;

        public void Run(int n)
        {
            _steps = 0;
            LastResult = Power(1.001, n);
            LastStepCount = _steps;
        }

        private double Power(double x, int n)
        {
            if (n == 0) return 1.0;
            _steps++; // Одно умножение при возврате
            return x * Power(x, n - 1);
        }
    }

    public class PowerBinaryAlgorithm : IAlgorithm
    {
        public int Id => 12;
        public string Name => "Степень (Быстрый/Бинарный)";
        public string BigO => "O(log n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }

        private long _steps;

        public void Run(int n)
        {
            _steps = 0;
            LastResult = Power(1.001, n);
            LastStepCount = _steps;
        }

        private double Power(double x, int n)
        {
            if (n == 0) return 1.0;
            
            if (n % 2 == 0)
            {
                double half = Power(x, n / 2);
                _steps++; // Одно умножение: half * half
                return half * half;
            }
            else
            {
                double half = Power(x, n - 1);
                _steps++; // Одно умножение: x * half
                return x * half;
            }
        }
    }
}