using System.Runtime.Versioning;
using CleanArchitecture.Blazor.Application.Common.Security;
using CleanArchitecture.Blazor.Application.Services;
using CleanArchitecture.Blazor.Application.Services.PaddleOCR;


namespace CleanArchitecture.Blazor.Infrastructure.Extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ExceptionHandlingMiddleware>()
            .AddScoped<IDateTime, DateTimeService>()
            .AddScoped<IExcelService, ExcelService>()
            .AddScoped<IUploadService, UploadService>()
            .AddScoped<IGeoAddressService, GeoAddressService>()
            .AddScoped<IPDFService, PDFService>();
    }
}