using System.Collections.Generic;

namespace Lab1.Core.Entities
{
    /// <summary>
    /// Представляет исследуемый алгоритм.
    /// </summary>
    public class Algorithm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BigONotation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Навигационное свойство
        public ICollection<ExperimentSession> Sessions { get; set; } = new List<ExperimentSession>();
    }
}