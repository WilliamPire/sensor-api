using System.Collections.Generic;

namespace Sensor.Api.Domain.Eventos.Models
{
    public class GraficoEvento
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }

        public GraficoEvento()
        {
            this.Labels = new List<string>();
            this.Data = new List<int>();
        }
    }
}
