using Microsoft.Extensions.Caching.Memory;

using WhatTheTea.SportyvMap.EquipmentCentreService.Workers;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Services;

public class CachedEquipmentCentreService(
    IMemoryCache cache,
    CacheSignal<District[]> cacheSignal,
    ILogger<CachedEquipmentCentreService> logger)
    : IEquipmentCentreService
{
    public async Task<IEnumerable<District>> GetDistrictsAsync()
    {
        try
        {
            await cacheSignal.WaitAsync();

            District[] districts = await cache.GetOrCreateAsync("Districts", 
                _ => Task.FromResult(Array.Empty<District>())) 
                ?? [];

            return districts;
        }
        finally
        {
            cacheSignal.Release();
        }
    }

    public async Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId)
    {
        var districts = await GetDistrictsAsync();

        var district = districts.FirstOrDefault(x => x.Id == districtId)
            ?? throw new ArgumentOutOfRangeException(nameof(districtId), districtId, null);
        var centre = district.EquipmentCentres.ElementAtOrDefault(centreId - 1)
            ?? throw new ArgumentOutOfRangeException(nameof(centreId), centreId, null); ;

        return centre;
    }
}
