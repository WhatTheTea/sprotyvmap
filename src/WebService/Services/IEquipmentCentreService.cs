using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.WebService.Services;

public interface IEquipmentCentreService
{
    Task<IEnumerable<District>> GetDistrictsAsync();
    Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId);
}
