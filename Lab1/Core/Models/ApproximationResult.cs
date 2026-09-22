using System.Collections.Generic;

namespace Lab1.Core.Models
{
    public class ApproximationResult
    {
        public string Function { get; set; } = string.Empty;
        public double C { get; set; }
        public double MSE { get; set; }
        // Точки идеальной кривой для построения графика: (n, C * f(n))
        public List<(int n, double expectedTime)> Curve { get; set; } = new();
    }
}