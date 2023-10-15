using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Features.Buses.Queries.GetAll;
using DocumentFormat.OpenXml.Bibliography;
using Hangfire.Dashboard;
using MemoryPack;
using Stl.Async;
using Stl.Collections;
using Stl.CommandR;
using Stl.CommandR.Commands;
using Stl.CommandR.Configuration;
using Stl.Fusion;
using Unit = System.Reactive.Unit;
namespace CleanArchitecture.Blazor.Application.Services;


public interface ITripMonitor : IComputeService
{
    Task<int> AddOrUpdate(int tripId, CancellationToken cancellationToken = default);
    Task Remove(int tripId, CancellationToken cancellationToken = default);
    [ComputeMethod]
    Task<List<int>> List(CancellationToken cancellationToken = default);
    [ComputeMethod]
    Task<int> Count(CancellationToken cancellationToken = default);
}
public class TripMonitor : ITripMonitor
{
    private ImmutableList<int> _tripruning = ImmutableList<int>.Empty;
    private readonly IMutableState<int> _count;
    public TripMonitor(IStateFactory stateFactory)
        => _count = stateFactory.NewMutable<int>(0);
    public virtual Task<int> AddOrUpdate(int tripId, CancellationToken cancellationToken = default)
    {
        if (Computed.IsInvalidating())
            return null!;
        using var invalidating = Computed.Invalidate();
        _tripruning = _tripruning.RemoveAll(i => i == tripId).Add(tripId);
        _count.Value +=1;
        return Task.FromResult(tripId);
    }
    public virtual async Task Remove(int tripId, CancellationToken cancellationToken = default)
    {
        if (Computed.IsInvalidating())
            return;
        using var invalidating = Computed.Invalidate();
        _tripruning = _tripruning.RemoveAll(i => i == tripId);
        _count.Value -= 1;
    }
    [ComputeMethod]
    public virtual Task<List<int>> List(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_tripruning.ToList());
    }
    public virtual async Task<int> Count(CancellationToken cancellationToken = default)
    {
        return await _count.Use(cancellationToken);
    }
}
