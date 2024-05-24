using WhatTheTea.SprotyvMap.Data.Primitives;

namespace WhatTheTea.SprotyvMap.Data.Abstractions;

public interface IDataProvider
{
    Task<IEnumerable<District>> GetAllDistrictsAsync();
}