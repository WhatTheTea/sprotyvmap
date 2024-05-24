using WhatTheTea.SprotyvMap.Data.Abstractions;

namespace WhatTheTea.SprotyvMap.Data;

public class EquipmentCentreProvider : IEquipmentCentreProvider
{
    public EquipmentCentre GetEquipmentCentre(int district, int id)
        => GetEquipmentCentreAsync(district, id)
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public List<EquipmentCentre> GetEquipmentCentresByDistrict(int district)
        => GetEquipmentCentresByDistrictAsync(district).ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public List<EquipmentCentre> GetAllEquipmentCentres()
        => GetAllEquipmentCentresAsync().ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public Task<EquipmentCentre> GetEquipmentCentreAsync(int district, int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<EquipmentCentre>> GetEquipmentCentresByDistrictAsync(int district)
    {
        throw new NotImplementedException();
    }

    public Task<List<EquipmentCentre>> GetAllEquipmentCentresAsync()
    {
        throw new NotImplementedException();
    }
}