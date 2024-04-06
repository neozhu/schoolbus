namespace CleanArchitecture.Blazor.Application.Services;

public interface IGeoAddressService
{
    Task<string> GetAddress(double latitude, double longitude);
}