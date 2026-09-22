using System.Collections.Generic;
using Lab1.Core.Models;

namespace Lab1.Core.Interfaces
{
    public interface IApproximationService
    {
        ApproximationResult Approximate(IEnumerable<(int n, double t)> points);
    }
}