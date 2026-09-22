using System;
using System.Collections.Generic;

namespace Lab1.Core.Entities
{
    /// <summary>
    /// Серия (сессия) экспериментов для конкретного алгоритма с заданными настройками.
    /// </summary>
    public class ExperimentSession
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public int NMax { get; set; }
        public int Step { get; set; }
        public int RunsPerPoint { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public Algorithm? Algorithm { get; set; }
        public ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();
    }
}