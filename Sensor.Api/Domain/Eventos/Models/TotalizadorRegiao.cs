using System.Collections.Generic;

namespace Sensor.Api.Domain.Eventos.Models
{
    public class TotalizadorRegiao
    {
        public string Regiao { get; set; }
        public int Total { get; set; }
        public List<TotalizadorSensor> TotalizadorSensores { get; set; }

        public TotalizadorRegiao()
        {
            this.TotalizadorSensores = new List<TotalizadorSensor>();
        }
    }

    public class TotalizadorSensor
    {
        public string Sensor { get; set; }
        public int Total { get; set; }
    }
}
