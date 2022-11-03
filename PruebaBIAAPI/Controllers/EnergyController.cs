using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaBIAAPI.Entities.DTOs;
using PruebaBIAAPI.Interfaces;

namespace PruebaBIAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyService energyService;

        public EnergyController(IEnergyService energyService)
        {
            this.energyService = energyService;
        }

        [HttpGet("CalculateConsumption")]
        public async Task<ActionResult<List<EnergyConsumptionDTO>>> Get([FromQuery] PeriodDateDTO periodDateDTO)
        {
            try
            {
                var resultado = await this.energyService.CalculateConsumption(periodDateDTO);
                if (resultado.Error == true)
                {
                    return new NotFoundObjectResult(resultado);
                }

                return new OkObjectResult(JsonConvert.DeserializeObject(resultado.Mensaje));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
