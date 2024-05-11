using System.Collections.Immutable;
using ActualLab.Fusion;
namespace CleanArchitecture.Blazor.Application.Services;


public interface ITripMonitor : IComputeService
{
    Task AddOrUpdate(int tripId, CancellationToken cancellationToken = default);
    Task Remove(int tripId, CancellationToken cancellationToken = default);
    [ComputeMethod]
    Task<List<int>> List(CancellationToken cancellationToken = default);
    [ComputeMethod]
    Task<int> Count(CancellationToken cancellationToken = default);
}
public class TripMonitor : ITripMonitor
{
    private ImmutableList<int> _tripruning = ImmutableList<int>.Empty;
 
    public virtual Task AddOrUpdate(int tripId, CancellationToken cancellationToken = default)
    {
        _tripruning = _tripruning.RemoveAll(i => i == tripId).Add(tripId);
        using var invalidating = Invalidation.Begin();
        _ = Count(cancellationToken);
        _ = List(cancellationToken);
        
        return Task.CompletedTask;
    }
    public virtual  Task Remove(int tripId, CancellationToken cancellationToken = default)
    {
        _tripruning = _tripruning.RemoveAll(i => i == tripId);
        using var invalidating = Invalidation.Begin();
        _ = Count(cancellationToken);
        _ = List(cancellationToken);

        return Task.CompletedTask;
    }
    public virtual Task<List<int>> List(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_tripruning.ToList());
    }
    public virtual Task<int> Count(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_tripruning.Count());
    }
}
