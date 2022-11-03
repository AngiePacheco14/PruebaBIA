using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBIAAPI.Entities.Extensions
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
