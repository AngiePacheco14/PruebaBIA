using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBIAAPI.Entities.DTOs
{
    public class PeriodDateDTO
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Period { get; set; }
    }
}
