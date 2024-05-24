using WhatTheTea.SprotyvMap.Data.Primitives;

namespace WhatTheTea.SprotyvMap.Data.Abstractions;

public interface IMapPointProvider
{
    Task<MapPoint> GetPoint(string address);
}