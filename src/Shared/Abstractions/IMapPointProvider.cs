using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Shared.Abstractions;

public interface IMapPointProvider
{
    Task<MapPoint> GetPoint(string address);
}