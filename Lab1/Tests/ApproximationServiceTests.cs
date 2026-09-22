using System.Collections.Generic;
using Xunit;
using Lab1.Core.AppServices;

namespace Lab1.Tests
{
    public class ApproximationServiceTests
    {
        private readonly ApproximationService _service = new();

        [Fact]
        public void Approximate_LinearData_ReturnsLinearFunction()
        {
            // Подготовка: идеальная линейная зависимость T(n) = 5 * n
            var points = new List<(int n, double t)>
            {
                (10, 50),
                (20, 100),
                (30, 150),
                (40, 200)
            };

            // Действие
            var result = _service.Approximate(points);

            // Проверка
            Assert.Equal("O(n)", result.Function);
            Assert.Equal(5.0, result.C, 4); // 4 - точность знаков после запятой
            Assert.True(result.MSE < 1e-5);
        }

        [Fact]
        public void Approximate_QuadraticData_ReturnsQuadraticFunction()
        {
            // Подготовка: идеальная квадратичная зависимость T(n) = 3 * n^2
            var points = new List<(int n, double t)>
            {
                (10, 300),   // 3 * 10^2
                (20, 1200),  // 3 * 20^2
                (30, 2700)   // 3 * 30^2
            };

            // Действие
            var result = _service.Approximate(points);

            // Проверка
            Assert.Equal("O(n^2)", result.Function);
            Assert.Equal(3.0, result.C, 4);
            Assert.True(result.MSE < 1e-5);
        }
    }
}