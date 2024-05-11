
using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Services.DataServices;

namespace Blazor.Server.UI.Components.Common;

public class PickBusAutocomplete : MudAutocomplete<int?>
{
    [Parameter]
    public string TenantId { get; set; }=string.Empty;
    [Inject]
    private IBusService DataProvider { get; set; } = default!;

    private List<BusDto>? _busList;
   public PickBusAutocomplete()
    {
        SearchFuncWithCancel = SearchKeyValues;
        ToStringFunc = ToString;
    }

    public override  Task SetParametersAsync(ParameterView parameters)
    {
      
        Clearable = true;
        Dense = true;
        ResetValueOnEmptyText = true;
        ShowProgressIndicator = true;
        MaxItems = 50;
        return base.SetParametersAsync(parameters);
      
    }
    private Task<IEnumerable<int?>> SearchKeyValues(string value, CancellationToken cancellation)
    {
        _busList = DataProvider.DataSource.Where(x => x.TenantId == TenantId).ToList();
        var result= new List<int?>();
        if (_busList is not null && string.IsNullOrEmpty(value))
        {
            result= _busList.Select(x=>x.Id as int?).ToList();
        }
        else if(_busList is not null)
        {
            result = _busList.Where(x => $"{x.PlatNumber} - {x.Type} - {x.Make} - {x.Model}".Contains(value, StringComparison.OrdinalIgnoreCase)).Select(x => x.Id as int?).ToList();
        }
        return Task.FromResult(result.AsEnumerable());
    }

    private string ToString(int? str)
    {
        if (str>0)
        {
            var userDto = DataProvider.DataSource.Find(x => x.Id==str);
            return $"{userDto?.PlatNumber} - {userDto?.Type} - {userDto?.Make} - {userDto?.Model}";
        }
        return string.Empty;
    }

}



