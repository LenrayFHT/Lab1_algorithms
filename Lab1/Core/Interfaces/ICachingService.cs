using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Core.Entities;

namespace Lab1.Core.Interfaces
{
    public interface ICachingService
    {
        Task<bool> RequiresCalculationAsync(int algorithmId, int n, bool forceRecalc);
        Task<List<Experiment>> GetCachedDataAsync(int algorithmId, int n);
    }
}