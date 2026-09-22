using System.Threading;
using System.Threading.Tasks;
using Lab1.Core.Models;

namespace Lab1.Core.Interfaces
{
    public interface IExperimentService
    {
        Task<ExperimentResult> RunExperimentAsync(
            int algorithmId, 
            int nMax, 
            int step, 
            int runsPerPoint, 
            bool forceRecalc, 
            CancellationToken ct);
    }
}