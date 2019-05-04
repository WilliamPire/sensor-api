using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos.Models
{
    public class SensorRegiao
    {
        public string Regiao { get; set; }
        public List<string> Sensores { get; set; }

        public SensorRegiao(string regiao)
        {
            this.Sensores = new List<string>();
            this.Regiao = regiao;
        }
    }
}
