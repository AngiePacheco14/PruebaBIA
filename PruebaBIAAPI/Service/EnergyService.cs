using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaBIAAPI.DataModelCore.Context;
using PruebaBIAAPI.Entities.DTOs;
using PruebaBIAAPI.Interfaces;

namespace PruebaBIAAPI.Service
{
    public class EnergyService: IEnergyService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EnergyService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Servicio para calcular el consumo según fecha y periodo
        /// </summary>
        /// <param name="periodDate"></param>
        /// <returns></returns>
        public async Task<Response> CalculateConsumption(PeriodDateDTO periodDate)
        {
            try
            {
                List<EnergyConsumptionDTO> energyValues = new List<EnergyConsumptionDTO>();
                EnergyConsumptionDTO energyConsumption;

                if (periodDate.Period == "daily")
                {
                    DateTime nextDay = periodDate.Date.AddDays(1);

                    List<EnergyConsumption> dataValues = await context.EnergyConsumption.
                    Where(T => T.Meter_date < nextDay).OrderBy(T => T.Meter_date).ToListAsync();

                    DateTime startHour = periodDate.Date;
                    DateTime endHour = startHour.AddHours(1);

                    energyValues = GetListConsumption(dataValues, startHour, endHour, nextDay, PeriodType.daily);
                }
                else if (periodDate.Period == "weekly")
                {
                    var dayOfWeek = (int)periodDate.Date.DayOfWeek == 0 ? 7: (int)periodDate.Date.DayOfWeek;
                    int dayStart = dayOfWeek - 1; //Desde que fecha empieza
                    int dayAdd = 7 - dayOfWeek; //Agregar a la fecha

                    DateTime startDate = periodDate.Date.AddDays(-dayStart);
                    DateTime endDate = periodDate.Date.AddDays(dayAdd);
                    DateTime lastDay = endDate.AddDays(1);

                    DateTime nextDate = startDate.AddDays(1);

                    List<EnergyConsumption> dataValues = await context.EnergyConsumption.
                    Where(T => T.Meter_date >= startDate && T.Meter_date < lastDay).OrderBy(T => T.Meter_date).ToListAsync();
          
                    energyValues = GetListConsumption(dataValues, startDate, nextDate, lastDay, PeriodType.weekly);
                }
                else if (periodDate.Period == "monthly")
                {
                    DateTime startDate = new DateTime(periodDate.Date.Year, periodDate.Date.Month, 1);
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                    DateTime lastDate = endDate.AddDays(1);

                    DateTime nextDate = startDate.AddDays(1);

                    List<EnergyConsumption> dataValues = await context.EnergyConsumption.
                    Where(T => T.Meter_date >= startDate && T.Meter_date < lastDate).OrderBy(T => T.Meter_date).ToListAsync();

                    energyValues = GetListConsumption(dataValues, startDate, nextDate, lastDate, PeriodType.monthly);
                }
                else
                {
                    return new Response(true, $"El periodo buscado {periodDate.Period} no existe");
                }

                return new Response(false, JsonConvert.SerializeObject(energyValues));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtener la lista de los calculos según el periodo buscado
        /// </summary>
        /// <param name="dataValues">Lista de datos</param>
        /// <param name="startDate">Fecha y hora en la que inicia</param>
        /// <param name="endDate">Fecha y hora en la que finaliza</param>
        /// <param name="lastDay">Día despues del final</param>
        /// <param name="periodType">Periodo buscado</param>
        /// <returns></returns>
        private static List<EnergyConsumptionDTO> GetListConsumption(List<EnergyConsumption> dataValues, DateTime startDate, DateTime endDate, DateTime lastDay, PeriodType periodType)
        {
            List<EnergyConsumptionDTO> energyValues = new List<EnergyConsumptionDTO>();
            EnergyConsumptionDTO energyConsumption;
            do
            {
                energyConsumption = new EnergyConsumptionDTO();
                energyConsumption.Meter_Date = startDate;

                var energyVa = dataValues.Where(T => T.Meter_date >= startDate && T.Meter_date < endDate).OrderBy(T => T.Meter_date).ToList();
                if (energyVa != null & energyVa.Count > 0)
                {
                    var firstData = energyVa.FirstOrDefault();
                    var lastData = energyVa.LastOrDefault();
                    var value = lastData.Active_energy - firstData.Active_energy;
                    energyConsumption.Value = value;
                }
                else
                {
                    energyConsumption.Value = 0;
                }

                energyValues.Add(energyConsumption);
                startDate = endDate;
                endDate = PeriodType.daily == periodType ? startDate.AddHours(1) : startDate.AddDays(1);
            }
            while (endDate <= lastDay);

            return energyValues;
        }
    }
}
