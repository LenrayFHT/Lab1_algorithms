using System;
using Xunit;
using Lab1.Core.Algorithms;

namespace Lab1.Tests
{
    public class RadixSortAlgorithmTests
    {
        [Fact]
        public void RadixSort_CorrectlySortsArray()
        {
            var algo = new RadixSortAlgorithm();
            algo.Run(100);
            
            var result = algo.LastResult as int[];
            Assert.NotNull(result);
            
            for (int i = 0; i < result.Length - 1; i++)
            {
                Assert.True(result[i] <= result[i + 1], "Массив не отсортирован по возрастанию.");
            }
        }

        [Fact]
        public void RadixSort_StepCount_GrowsLinearly()
        {
            var algo = new RadixSortAlgorithm();
            
            algo.Run(1000);
            long steps1000 = algo.LastStepCount ?? 0;
            
            algo.Run(10000);
            long steps10000 = algo.LastStepCount ?? 0;
            
            // Проверка линейности: при увеличении N в 10 раз,
            // количество операций должно увеличиться примерно в 10 раз (диапазон 8-12)
            double ratio = (double)steps10000 / steps1000;
            
            Assert.True(ratio >= 8.0 && ratio <= 12.0, 
                $"Отношение шагов ({ratio:F2}) выходит за пределы линейной оценки O(d*n).");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RadixSort_EdgeCases_HandlesEmptyOrSingleElement(int n)
        {
            var algo = new RadixSortAlgorithm();
            algo.Run(n);
            
            var result = algo.LastResult as int[];
            
            Assert.NotNull(result);
            Assert.Equal(n, result.Length);
            Assert.True(algo.LastStepCount == 0, "Для массивов размером <= 1 шаги сортировки не должны выполняться.");
        }
        
        [Fact]
        public void Constructor_ThrowsArgumentException_OnInvalidBase()
        {
            Assert.Throws<ArgumentException>(() => new RadixSortAlgorithm(1));
        }
    }
}