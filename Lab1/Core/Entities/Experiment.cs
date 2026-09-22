using System;

namespace Lab1.Core.Entities
{
    /// <summary>
    /// Конкретный замер времени выполнения алгоритма.
    /// </summary>
    public class Experiment
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public int? SessionId { get; set; } // Привязка к сессии
        public int N { get; set; }
        public int RunNumber { get; set; }
        public double ElapsedMs { get; set; }
        public long Steps { get; set; } // Количество базовых операций (опционально)
        public DateTime ExperimentDate { get; set; }

        // Навигационные свойства
        public Algorithm? Algorithm { get; set; }
        public ExperimentSession? Session { get; set; }
    }
}