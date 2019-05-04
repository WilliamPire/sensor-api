using MediatR;
using MongoDB.Bson;
using Sensor.Api.Core;
using System;

namespace Sensor.Api.Domain.Eventos.Commands.Inserir
{
    public class Request : IRequest<Response>
    {
        public long TimeStamp { get; set; }
        public string Tag { get; set; }
        public string Valor { get; set; }
    }
}
