using Microsoft.AspNetCore.Mvc;

using WhatTheTea.SportyvMap.EquipmentCentreService.Services;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController(IEquipmentCentreService service) : ControllerBase
    {
        private readonly IEquipmentCentreService service = service;

        // GET: api/Districts
        [HttpGet]
        public async Task<IEnumerable<District>> Get() => 
            await service.GetDistrictsAsync();



        // GET api/Districts/1/centre/1
        [HttpGet("{district}/centre/{id}")]
        public async Task<EquipmentCentre> GetCentre(int district, int id) =>
            await service.GetEquipmentCentreAsync(int district, int id);
    }
}
