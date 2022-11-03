using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBIAAPI.DataModelCore.Context
{
    public class EnergyConsumption
    {
        public int Id { get; set; }
        public double Active_energy { get; set; }
        public DateTime Meter_date { get; set; }
    }
}
