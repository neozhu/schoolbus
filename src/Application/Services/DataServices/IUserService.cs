using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Identity.Dto;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface IUserService
{
    List<ApplicationUserDto> DataSource { get; }

    event Action? OnChange;

    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}