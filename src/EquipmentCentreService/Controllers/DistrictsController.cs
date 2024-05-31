using Microsoft.AspNetCore.Mvc;

using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController(IDataProvider dataProvider, IMapPointProvider mapPointProvider) : ControllerBase
    {
        private readonly IDataProvider dataProvider = dataProvider;
        private readonly IMapPointProvider mapPointProvider = mapPointProvider;

        // GET: api/Districts
        [HttpGet]
        public async Task<IEnumerable<District>> Get()
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

        // GET api/Districts/1/centre/1
        [HttpGet("{district}/centre/{id}")]
        public async Task<EquipmentCentre> GetCentre(int district, int id)
        {
            var data = await dataProvider.GetEquipmentCentreAsync(district, id);
            var mapPoint = await mapPointProvider.GetPoint(data.Information);

            return data with { Point = mapPoint };
        }
    }
}
