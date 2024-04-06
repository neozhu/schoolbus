using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Common.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Blazor.Application.Services;
public class GeoAddressService : IGeoAddressService
{
    private readonly PrivacySettings _settings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAppCache _cache;
    private const string clientname = "locationservice";
    public GeoAddressService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IAppCache cache)
    {
        _settings = configuration.GetRequiredSection(PrivacySettings.Key).Get<PrivacySettings>();
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }
    public async Task<string> GetAddress(double latitude, double longitude)
    {
        try
        {
            var key = $"{latitude},{longitude}";
            var address = await _cache.GetOrAddAsync(key, async () =>
            {
                try
                {
                    using (var client = _httpClientFactory.CreateClient(clientname))
                    {
                        var uri = $"{latitude},{longitude}?key={_settings.BingMampKey}";
                        var response = await client.GetAsync(uri);
                        var data =await response.Content.ReadFromJsonAsync<GeoLocationResult>();
                        return data.resourceSets.FirstOrDefault()?.resources.FirstOrDefault()?.name ?? "";
                    }
                }catch(Exception e)
                {
                    throw e;
                }
            }, new TimeSpan(0, 3, 0));
            return address;
        }
        catch
        {
            return string.Empty;
        }
    }

    public class Address
    {
        public string addressLine { get; set; }
        public string adminDistrict { get; set; }
        public string adminDistrict2 { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
        public string postalCode { get; set; }
    }

    public class GeocodePoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        public string calculationMethod { get; set; }
        public List<string> usageTypes { get; set; }
    }

    public class Point
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Resource
    {
        public string __type { get; set; }
        public List<double> bbox { get; set; }
        public string name { get; set; }
        public Point point { get; set; }
        public Address address { get; set; }
        public string confidence { get; set; }
        public string entityType { get; set; }
        public List<GeocodePoint> geocodePoints { get; set; }
        public List<string> matchCodes { get; set; }
    }

    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class GeoLocationResult
    {
        public string authenticationResultCode { get; set; }
        public string brandLogoUri { get; set; }
        public string copyright { get; set; }
        public List<ResourceSet> resourceSets { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string traceId { get; set; }
    }
}
