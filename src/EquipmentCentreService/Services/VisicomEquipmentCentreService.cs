using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Services
{
    public class VisicomEquipmentCentreService(IDataProvider dataProvider, IMapPointProvider mapPointProvider)
        : IEquipmentCentreService
    {
        private readonly IDataProvider dataProvider = dataProvider;
        private readonly IMapPointProvider mapPointProvider = mapPointProvider;

        public async Task<IEnumerable<District>> GetDistrictsAsync()
        {
            var districts = (await dataProvider.GetAllDistrictsAsync()).ToList();
            return await GeocodeDistricts(districts).ToArrayAsync();
        }
        private async IAsyncEnumerable<District> GeocodeDistricts(IEnumerable<District> districts)
        {
            foreach (var district in districts.ToArray())
            {
                var centres = await GeocodeCentres(district.EquipmentCentres).ToArrayAsync();
                yield return district with { EquipmentCentres = centres };
            }
        }

        private async IAsyncEnumerable<EquipmentCentre> GeocodeCentres(IEnumerable<EquipmentCentre> centres)
        {
            foreach (var centre in centres)
            {
                var mapPoint = await mapPointProvider.GetPoint(centre.Information);
                yield return centre with { Point = mapPoint };
            }
        }

        public async Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId)
        {
            var data = await dataProvider.GetEquipmentCentreAsync(districtId, centreId);
            var mapPoint = await mapPointProvider.GetPoint(data.Information);

            return data with { Point = mapPoint };
        }
    }
}
