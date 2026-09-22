using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Core.Entities;

namespace Lab1.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для доступа к данным экспериментов.
    /// </summary>
    public interface IExperimentRepository
    {
        Task<bool> ExistsAsync(int algorithmId, int n);
        Task AddRangeAsync(IEnumerable<Experiment> items);
        Task<List<Experiment>> GetByAlgorithmAsync(int algorithmId);
        Task<List<ExperimentSession>> GetSessionsAsync();
        Task AddSessionAsync(ExperimentSession session); // Полезно для сохранения серии
    }
}