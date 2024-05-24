using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public class VisicomMapPointProvider : IMapPointProvider
{
    public Task<MapPoint> GetPoint(string address)
    {
        throw new NotImplementedException();
    }
}