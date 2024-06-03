using Microsoft.Extensions.Caching.Memory;

using WhatTheTea.SprotyvMap.Shared.Primitives;
using WhatTheTea.SprotyvMap.WebService.Services;

namespace WhatTheTea.SprotyvMap.WebService.Workers;

public sealed class DistrictsCacheWorker(
    IEquipmentCentreService service,
    CacheSignal<District[]> cacheSignal,
    IMemoryCache memoryCache,
    ILogger<DistrictsCacheWorker> logger)
    : BackgroundService
{
    private readonly IEquipmentCentreService service = service;
    private readonly CacheSignal<District[]> cacheSignal = cacheSignal;
    private readonly IMemoryCache memoryCache = memoryCache;
    private readonly ILogger<DistrictsCacheWorker> logger = logger;

    private readonly TimeSpan updateInterval = TimeSpan.FromDays(7);
    private bool isCacheInitialized = false;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await cacheSignal.WaitAsync();
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await UpdateCache();
            }
            finally
            {
                if (!isCacheInitialized)
                {
                    cacheSignal.Release();
                    isCacheInitialized = true;
                }
            }

            try
            {
                await Task.Delay(updateInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                logger.LogWarning("Caching was cancelled");
                break;
            }
        }
    }

    private async Task UpdateCache()
    {
        District[] districts = (await service.GetDistrictsAsync()).ToArray();

        if (districts.Length == 0)
        {
            logger.LogError("Unable to update districts cache");
        }
        else
        {
            memoryCache.Set("Districts", districts);
            logger.LogInformation("Cache has been updated");
        }
    }
}
