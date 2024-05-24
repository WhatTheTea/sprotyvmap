using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Shared.Abstractions;

public interface IDataProvider
{
    Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId);
    Task<IEnumerable<District>> GetAllDistrictsAsync();
}