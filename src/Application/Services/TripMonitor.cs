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
        using var invalidating = Computed.Invalidate();
        _ = Count(cancellationToken);
        _ = List(cancellationToken);
        
        return Task.CompletedTask;
    }
    public virtual  Task Remove(int tripId, CancellationToken cancellationToken = default)
    {
        _tripruning = _tripruning.RemoveAll(i => i == tripId);
        using var invalidating = Computed.Invalidate();
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
