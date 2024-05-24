using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public class VisicomMapPointProvider : IMapPointProvider
{
    private readonly HttpClient _httpClient;

    public VisicomMapPointProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<MapPoint> GetPoint(string address)
    {
        throw new NotImplementedException();
    }
}