using MongoDB.Bson;
using Sensor.Api.Core;
using System;

namespace Sensor.Api.Domain.Eventos
{
    public class Evento
    {
        public Guid Id { get; set; } 
        public DateTimeOffset TimeStamp { get; set; }
        public string Tag { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }

        public Evento(DateTimeOffset timeStamp, string tag, string valor, string status = null)
        {
            this.Id = Guid.NewGuid();
            this.TimeStamp = timeStamp;
            this.Tag = valor;
            this.Valor = tag;
            this.Status = status;
        }
    }
}
