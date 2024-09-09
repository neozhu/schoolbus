// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

public static class TripReportCacheKey
{
    private static readonly TimeSpan refreshInterval = TimeSpan.FromHours(3);
    public const string GetAllCacheKey = "all-TripReports";
    public static string GetPaginationCacheKey(string parameters) {
        return $"TripReportCacheKey:TripReportsWithPaginationQuery,{parameters}";
    }
    public static string GetTotalTripCacheKey(string parameters)
    {
        return $"TripReportCacheKey:GetTotalTripCacheKey,{parameters}";
    }
    public static string GetByNameCacheKey(string parameters) {
        return $"TripReportCacheKey:GetByNameCacheKey,{parameters}";
    }
    public static string GetByUserCacheKey(string parameters)
    {
        return $"TripReportCacheKey:GetByUserCacheKey,{parameters}";
    }
    public static string GetByIdCacheKey(string parameters) {
        return $"TripReportCacheKey:GetByIdCacheKey,{parameters}";
    }
    public static string GetOnBoardTripLogsCacheKey(string parameters)
    {
        return $"TripReportCacheKey:GetOnBoardTripLogsCacheKey,{parameters}";
    }

    public static string GetTripLogsCacheKey(string parameters)
    {
        return $"TripReportCacheKey:GetTripLogsCacheKey,{parameters}";
    }
    public static string GetTripAccidentsCacheKey(string parameters)
    {
        return $"TripReportCacheKey:GetTripAccidentsCacheKey,{parameters}";
    }
    
    static TripReportCacheKey()
    {
        _tokensource = new CancellationTokenSource(refreshInterval);
    }
    private static CancellationTokenSource _tokensource;
    public static CancellationTokenSource SharedExpiryTokenSource()
    {
        if (_tokensource.IsCancellationRequested)
        {
            _tokensource = new CancellationTokenSource(refreshInterval);
        }
        return _tokensource;
    }
    public static void Refresh() => SharedExpiryTokenSource().Cancel();
    public static MemoryCacheEntryOptions MemoryCacheEntryOptions => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(SharedExpiryTokenSource().Token));
}

