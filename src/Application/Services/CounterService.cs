using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stl.Fusion;

namespace CleanArchitecture.Blazor.Application.Services;
public class CounterService : IComputeService
{
    private readonly IMutableState<int> _count;
    public CounterService(IStateFactory stateFactory)
        => _count = stateFactory.NewMutable<int>(100);
    [ComputeMethod]
    public virtual async Task<int> Get(CancellationToken cancellationToken) => await _count.Use(cancellationToken);
    

    public Task Increment()
    {
        _count.Value += 1;
        return Task.CompletedTask;
    }
}
