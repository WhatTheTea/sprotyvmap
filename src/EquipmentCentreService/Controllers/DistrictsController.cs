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

        // GET: api/<EquipmentCentres>
        [HttpGet]
        public async Task<IEnumerable<District>> Get()
        {
            var data = (await dataProvider.GetAllDistrictsAsync()).ToList();
            return await Task.WhenAll(data.Select(async x =>
            {
                var centres = x.EquipmentCentres.Select(async y =>
                {
                    var mapPoint = await mapPointProvider.GetPoint(y.Information);
                    return y with { Point = mapPoint };
                });
                return x with { EquipmentCentres = await Task.WhenAll(centres) };
            }));
        }

        // GET api/EquipmentCentres/district/1/centre/1
        [HttpGet("district/{district}/centre/{id}")]
        public async Task<EquipmentCentre> Get(int district, int id)
        {
            var data = await dataProvider.GetEquipmentCentreAsync(district, id);
            var mapPoint = await mapPointProvider.GetPoint(data.Information);

            return data with { Point = mapPoint };
        }
    }
}
