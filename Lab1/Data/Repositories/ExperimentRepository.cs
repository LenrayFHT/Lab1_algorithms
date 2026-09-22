using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Core.Entities;
using Lab1.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с экспериментами через EF Core.
    /// </summary>
    public class ExperimentRepository : IExperimentRepository
    {
        private readonly AppDbContext _context;

        public ExperimentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int algorithmId, int n)
        {
            // Проверяем, делали ли мы уже замер для данного алгоритма и размера N
            return await _context.Experiments
                .AnyAsync(e => e.AlgorithmId == algorithmId && e.N == n);
        }

        public async Task AddRangeAsync(IEnumerable<Experiment> items)
        {
            await _context.Experiments.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Experiment>> GetByAlgorithmAsync(int algorithmId)
        {
            return await _context.Experiments
                .AsNoTracking() // Отключаем трекинг для Read-only запросов (ускоряет работу)
                .Where(e => e.AlgorithmId == algorithmId)
                .OrderBy(e => e.N)
                .ToListAsync();
        }

        public async Task<List<ExperimentSession>> GetSessionsAsync()
        {
            return await _context.ExperimentSessions
                .Include(s => s.Algorithm) // Подгружаем связанный алгоритм (Eager Loading)
                .AsNoTracking()
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task AddSessionAsync(ExperimentSession session)
        {
            await _context.ExperimentSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }
    }
}