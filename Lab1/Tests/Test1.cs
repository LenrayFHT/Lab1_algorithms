using System;
using Xunit;
using Lab1.Core.Algorithms;

namespace Lab1.Tests
{
    public class PowerAlgorithmsTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void PowerNaive_StepCount_MatchesTheory_O_n(int n)
        {
            var algo = new PowerNaiveAlgorithm();
            algo.Run(n);
            
            Assert.Equal(n, algo.LastStepCount);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void PowerRecursive_StepCount_MatchesTheory_O_n(int n)
        {
            var algo = new PowerRecursiveAlgorithm();
            algo.Run(n);
            
            Assert.Equal(n, algo.LastStepCount);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void PowerBinary_StepCount_MatchesTheory_O_log_n(int n)
        {
            var algo = new PowerBinaryAlgorithm();
            algo.Run(n);
            
            // Теоретическая оценка O(log n): максимальное число операций не превышает 2 * log2(n)
            double maxExpectedSteps = 2 * Math.Log2(n);
            
            Assert.True(algo.LastStepCount <= maxExpectedSteps, 
                $"Шагов {algo.LastStepCount} превышает теор. предел {maxExpectedSteps}");
            Assert.True(algo.LastStepCount > 0, "Количество шагов должно быть больше 0");
        }
    }
}