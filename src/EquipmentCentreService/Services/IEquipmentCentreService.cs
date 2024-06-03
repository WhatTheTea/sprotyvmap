using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Services;

public interface IEquipmentCentreService
{
    Task<IEnumerable<District>> GetDistrictsAsync();
    Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId);
}
