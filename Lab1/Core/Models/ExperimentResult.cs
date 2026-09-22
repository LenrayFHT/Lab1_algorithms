using System.Collections.Generic;

namespace Lab1.Core.Models
{
    public class ExperimentPoint
    {
        public int N { get; set; }
        public double AverageTimeMs { get; set; }
        public long AverageSteps { get; set; }
    }

    public class ExperimentResult
    {
        public int AlgorithmId { get; set; }
        public List<ExperimentPoint> Points { get; set; } = new();
    }
}