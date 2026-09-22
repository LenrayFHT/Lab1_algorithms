using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;

namespace Lab1.Core.AppServices
{
    public class ApproximationService : IApproximationService
    {
        // Список кандидатов f(n) для аппроксимации
        private readonly List<(string Name, Func<int, double> Func)> _functions = new()
        {
            ("O(1)", n => 1.0),
            ("O(n)", n => n),
            ("O(n log n)", n => n > 1 ? n * Math.Log2(n) : 0), // Защита от нуля в логарифме
            ("O(n^2)", n => Math.Pow(n, 2)),
            ("O(n^3)", n => Math.Pow(n, 3))
        };

        public ApproximationResult Approximate(IEnumerable<(int n, double t)> points)
        {
            var data = points.ToList();
            if (!data.Any())
                throw new ArgumentException("Нет данных для аппроксимации");

            ApproximationResult? bestResult = null;
            double minMse = double.MaxValue;

            foreach (var (name, func) in _functions)
            {
                double numerator = 0;
                double denominator = 0;

                // Вычисление коэффициента C
                foreach (var (n, t) in data)
                {
                    double fn = func(n);
                    numerator += t * fn;
                    denominator += fn * fn;
                }

                // Защита от деления на ноль, если f(n) дает нули
                if (denominator == 0) continue; 

                double c = numerator / denominator;
                double mse = 0;

                // Вычисление MSE
                foreach (var (n, t) in data)
                {
                    double fn = func(n);
                    double diff = t - (c * fn);
                    mse += diff * diff;
                }
                mse /= data.Count;

                if (bestResult == null || mse < minMse)
                {
                    minMse = mse;
                    
                    var curve = data.Select(p => (p.n, expected: c * func(p.n))).ToList();
                    
                    bestResult = new ApproximationResult
                    {
                        Function = name,
                        C = c,
                        MSE = mse,
                        Curve = curve
                    };
                }
            }

            return bestResult!;
        }
    }
}