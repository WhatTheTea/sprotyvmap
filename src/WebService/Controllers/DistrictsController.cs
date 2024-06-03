using Microsoft.AspNetCore.Mvc;

using System.Net;

using WhatTheTea.SprotyvMap.Shared.Primitives;
using WhatTheTea.SprotyvMap.WebService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatTheTea.SprotyvMap.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController(IEquipmentCentreService service) : ControllerBase
    {
        private readonly IEquipmentCentreService service = service;

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> Get()
        {
            try
            {
                var districts = await service.GetDistrictsAsync();
                return districts.ToArray();
            }
            catch (ArgumentOutOfRangeException)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
        }

        // GET: api/Districts/1/centre/1
        [HttpGet("{district}/centre/{id}")]
        public async Task<ActionResult<EquipmentCentre>> GetCentre(int district, int id)
        {
            try
            {
                return await service.GetEquipmentCentreAsync(district, id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
        }
    }
}
