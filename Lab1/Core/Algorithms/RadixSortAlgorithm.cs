using System;
using System.Diagnostics;
using Lab1.Core.Interfaces;

namespace Lab1.Core.Algorithms
{
    /// <summary>
    /// Поразрядная сортировка (LSD - Least Significant Digit).
    /// </summary>
    public class RadixSortAlgorithm : IAlgorithm
    {
        private readonly int _radixBase;
        private long _currentStepCount;
        public int Id => 13; 

        public string Name => $"Radix Sort (LSD, base {_radixBase})";
        public string BigO => "O(d·n)";
        public long? LastStepCount { get; private set; }
        public object? LastResult { get; private set; }
        public long LastElapsedMs { get; private set; }

        /// <summary>
        /// Конструктор с настройкой основания системы счисления.
        /// </summary>
        /// <param name="radixBase">Основание системы (по умолчанию 10).</param>
        /// <exception cref="ArgumentException">Выбрасывается, если основание меньше 2.</exception>
        public RadixSortAlgorithm(int radixBase = 10)
        {
            if (radixBase < 2)
                throw new ArgumentException("Основание системы счисления должно быть >= 2.", nameof(radixBase));
            
            _radixBase = radixBase;
        }

        public void Run(int n)
        {
            int[] array = GenerateRandomArray(n);
            _currentStepCount = 0;

            var sw = Stopwatch.StartNew();
            
            if (n > 1)
            {
                Sort(array);
            }
            
            sw.Stop();

            LastStepCount = _currentStepCount;
            LastElapsedMs = sw.ElapsedMilliseconds;
            LastResult = array;
        }

        /// <summary>
        /// Основной метод сортировки (LSD).
        /// </summary>
        private void Sort(int[] array)
        {
            int max = GetMax(array);

            // Проходим по каждому разряду. exp = 1, 10, 100, ...
            for (int exp = 1; max / exp > 0; exp *= _radixBase)
            {
                CountingSortByDigit(array, exp);
            }
        }

        /// <summary>
        /// Устойчивая сортировка подсчётом по конкретному разряду.
        /// </summary>
        private void CountingSortByDigit(int[] array, int exp)
        {
            int n = array.Length;
            int[] output = new int[n];
            int[] count = new int[_radixBase];

            // 1. Подсчёт количества вхождений каждой цифры
            for (int i = 0; i < n; i++)
            {
                int digit = (array[i] / exp) % _radixBase;
                count[digit]++;
                _currentStepCount++; 
            }

            // 2. Вычисление накопительных сумм для определения позиций
            for (int i = 1; i < _radixBase; i++)
            {
                count[i] += count[i - 1];
                _currentStepCount++;
            }

            // 3. Сборка выходного массива (идем с конца для устойчивости)
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (array[i] / exp) % _radixBase;
                output[count[digit] - 1] = array[i];
                count[digit]--;
                _currentStepCount++; // Сдвиг/запись в промежуточный массив
            }

            // 4. Копирование отсортированных данных обратно в исходный массив
            for (int i = 0; i < n; i++)
            {
                array[i] = output[i];
                _currentStepCount++; // Запись в оригинальный массив
            }
        }

        /// <summary>
        /// Поиск максимального элемента для определения количества разрядов.
        /// </summary>
        private int GetMax(int[] array)
        {
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
                _currentStepCount++;
            }
            return max;
        }

        /// <summary>
        /// Генерация массива с фиксированным seed.
        /// </summary>
        private int[] GenerateRandomArray(int n)
        {
            var rnd = new Random(42);
            var arr = new int[n];
            for (int i = 0; i < n; i++) 
            {
                arr[i] = rnd.Next(0, 1000000); // Ограничиваем числа для адекватного числа разрядов
            }
            return arr;
        }
    }
}