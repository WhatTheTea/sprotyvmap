using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.WebApp.Services;

public interface IEquipmentCentreProvider
{
    Task<IEnumerable<District>> GetDistrictsAsync();
}
