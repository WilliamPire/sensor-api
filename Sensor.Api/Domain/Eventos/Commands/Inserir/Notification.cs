using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos.Commands.Inserir
{
    public class Notification : INotification
    {
        public long TimeStamp { get; set; }
        public string Tag { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }

        public override string ToString()
           => $"Novo evento cadastrado {Tag} com valor {Valor}.";
    }
}
