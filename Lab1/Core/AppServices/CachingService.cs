using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Core.Entities;
using Lab1.Core.Interfaces;

namespace Lab1.Core.AppServices // Измененный namespace
{
    public class CachingService : ICachingService
    {
        private readonly IExperimentRepository _repository;

        public CachingService(IExperimentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RequiresCalculationAsync(int algorithmId, int n, bool forceRecalc)
        {
            if (forceRecalc) return true;
            
            bool exists = await _repository.ExistsAsync(algorithmId, n);
            return !exists;
        }

        public async Task<List<Experiment>> GetCachedDataAsync(int algorithmId, int n)
        {
            var allData = await _repository.GetByAlgorithmAsync(algorithmId);
            return allData.Where(e => e.N == n).ToList();
        }
    }
}