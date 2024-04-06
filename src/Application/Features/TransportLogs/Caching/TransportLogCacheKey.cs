// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;

public static class TransportLogCacheKey
{
    private static readonly TimeSpan refreshInterval = TimeSpan.FromHours(3);
    public const string GetAllCacheKey = "all-TransportLogs";
    public static string GetPaginationCacheKey(string parameters) {
        return $"TransportLogCacheKey:TransportLogsWithPaginationQuery,{parameters}";
    }
    public static string GetByNameCacheKey(string parameters) {
        return $"TransportLogCacheKey:GetByNameCacheKey,{parameters}";
    }
    public static string GetByIdCacheKey(string parameters) {
        return $"TransportLogCacheKey:GetByIdCacheKey,{parameters}";
    }
    public static string GetByStudentIdCacheKey(string parameters)
    {
        return $"TransportLogCacheKey:GetByStudentIdCacheKey,{parameters}";
    }
    public static string GetOnBoardCacheKey(string parameters)
    {
        return $"TransportLogCacheKey:GetOnBoardCacheKey,{parameters}";
    }
    static TransportLogCacheKey()
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

