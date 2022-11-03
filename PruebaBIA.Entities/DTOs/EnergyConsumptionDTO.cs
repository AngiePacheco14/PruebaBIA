using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaBIAAPI.Entities.Extensions;

namespace PruebaBIAAPI.Entities.DTOs
{
    public class EnergyConsumptionDTO
    {
        [JsonConverter(typeof(Extensions.DateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        
        public DateTime Meter_Date { get; set; }
        public double Value { get; set; }
    }
}
