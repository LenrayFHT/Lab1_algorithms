using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lab1.Core.Entities;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;

namespace Lab1.Core.AppServices // Измененный namespace
{
    public class ExperimentService : IExperimentService
    {
        private readonly IExperimentRepository _repository;
        private readonly ICachingService _cachingService;
        private readonly IEnumerable<IAlgorithm> _algorithms;

        public ExperimentService(
            IExperimentRepository repository,
            ICachingService cachingService,
            IEnumerable<IAlgorithm> algorithms)
        {
            _repository = repository;
            _cachingService = cachingService;
            _algorithms = algorithms;
        }

        public async Task<ExperimentResult> RunExperimentAsync(
            int algorithmId, int nMax, int step, int runsPerPoint, bool forceRecalc, CancellationToken ct)
        {
            var targetAlgo = _algorithms.FirstOrDefault(a => a.Id == algorithmId)
                ?? throw new ArgumentException($"Алгоритм с ID {algorithmId} не найден.");

            var result = new ExperimentResult { AlgorithmId = algorithmId };
            
            var session = new ExperimentSession
            {
                AlgorithmId = algorithmId,
                NMax = nMax,
                Step = step,
                RunsPerPoint = runsPerPoint,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.AddSessionAsync(session);

            var newExperimentsToSave = new List<Experiment>();

            for (int n = step; n <= nMax; n += step)
            {
                ct.ThrowIfCancellationRequested();

                if (await _cachingService.RequiresCalculationAsync(algorithmId, n, forceRecalc))
                {
                    var point = RunMeasurements(targetAlgo, session.Id, n, runsPerPoint, newExperimentsToSave);
                    result.Points.Add(point);
                }
                else
                {
                    var cachedRecords = await _cachingService.GetCachedDataAsync(algorithmId, n);
                    if (cachedRecords.Any())
                    {
                        result.Points.Add(new ExperimentPoint
                        {
                            N = n,
                            AverageTimeMs = cachedRecords.Average(e => e.ElapsedMs),
                            AverageSteps = (long)cachedRecords.Average(e => e.Steps)
                        });
                    }
                }
            }

            if (newExperimentsToSave.Any())
            {
                await _repository.AddRangeAsync(newExperimentsToSave);
            }

            return result;
        }

        private ExperimentPoint RunMeasurements(
            IAlgorithm algo, int sessionId, int n, int runs, List<Experiment> toSave)
        {
            double totalTimeMs = 0;
            long totalSteps = 0;

            algo.Run(Math.Min(n, 10));

            for (int r = 1; r <= runs; r++)
            {
                var sw = Stopwatch.StartNew();
                algo.Run(n);
                sw.Stop();

                double elapsedMs = sw.Elapsed.TotalMilliseconds;
                long steps = algo.LastStepCount ?? 0;

                if (elapsedMs < 1.0)
                {
                    int internalRuns = 50;
                    sw.Restart();
                    for (int i = 0; i < internalRuns; i++)
                    {
                        algo.Run(n);
                        steps += algo.LastStepCount ?? 0;
                    }
                    sw.Stop();
                    elapsedMs = sw.Elapsed.TotalMilliseconds / internalRuns;
                    steps /= internalRuns;
                }

                totalTimeMs += elapsedMs;
                totalSteps += steps;

                toSave.Add(new Experiment
                {
                    AlgorithmId = algo.Id,
                    SessionId = sessionId,
                    N = n,
                    RunNumber = r,
                    ElapsedMs = elapsedMs,
                    Steps = steps,
                    ExperimentDate = DateTime.UtcNow
                });
            }

            return new ExperimentPoint
            {
                N = n,
                AverageTimeMs = totalTimeMs / runs,
                AverageSteps = totalSteps / runs
            };
        }
    }
}