using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Shared.Abstractions;

public interface IDataProvider
{
    Task<IEnumerable<District>> GetAllDistrictsAsync();
}