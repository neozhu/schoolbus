using System.Net.Http.Headers;
using Polly;

namespace CleanArchitecture.Blazor.Infrastructure.Extensions;
public static class HttpClientServiceCollectionExtensions
{
    public static void AddHttpClientService(this IServiceCollection services)
    {
        services.AddHttpClient("ocr", c =>
        {
            c.BaseAddress = new Uri("https://paddleocr.blazorserver.com/uploadocr");
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30)));

        services.AddHttpClient("locationservice", c =>
        {
            c.BaseAddress = new Uri("https://dev.virtualearth.net/REST/v1/Locations/");
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(5)));
    }

    
}
