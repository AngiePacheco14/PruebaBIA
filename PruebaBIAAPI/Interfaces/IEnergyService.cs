

using PruebaBIAAPI.Entities.DTOs;

namespace PruebaBIAAPI.Interfaces
{
    public interface IEnergyService
    {
        Task<Response> CalculateConsumption(PeriodDateDTO periodDate);
    }
}
