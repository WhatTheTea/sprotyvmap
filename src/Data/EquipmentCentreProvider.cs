using WhatTheTea.SprotyvMap.Data.Abstractions;

namespace WhatTheTea.SprotyvMap.Data;

public class EquipmentCentreProvider : IEquipmentCentreProvider
{
    public EquipmentCentre GetPoint(int district, int id)
        => GetPointAsync(district, id)
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public List<EquipmentCentre> GetPointsByDistrict(int district)
        => GetPointsByDistrictAsync(district).ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public List<EquipmentCentre> GetAllPoints()
        => GetAllPointsAsync().ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public Task<EquipmentCentre> GetPointAsync(int district, int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<EquipmentCentre>> GetPointsByDistrictAsync(int district)
    {
        throw new NotImplementedException();
    }

    public Task<List<EquipmentCentre>> GetAllPointsAsync()
    {
        throw new NotImplementedException();
    }
}