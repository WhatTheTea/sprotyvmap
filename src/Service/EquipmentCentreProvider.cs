using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public class EquipmentCentreProvider : IEquipmentCentreProvider
{
    private readonly IDataProvider _dataProvider;
    private readonly IMapPointProvider _mapPointProvider;

    public EquipmentCentreProvider(IDataProvider dataProvider, IMapPointProvider mapPointProvider)
    {
        _dataProvider = dataProvider;
        _mapPointProvider = mapPointProvider;
    }

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

    public async Task<EquipmentCentre> GetEquipmentCentreAsync(int district, int id)
    {
        var data = await _dataProvider.GetEquipmentCentreAsync(district, id);
        var mapPoint = await _mapPointProvider.GetPoint(data.Location);

        return data with { Point = mapPoint };
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