using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PruebaBIAAPI.Entities.DTOs
{
    public class Response
    {
        public Response(bool error, string mensaje)
        {
            Error = error;
            Mensaje = mensaje;
        }

        [JsonIgnore]
        public bool Error { get; set; }

        public string Mensaje { get; set; }
    }
}
