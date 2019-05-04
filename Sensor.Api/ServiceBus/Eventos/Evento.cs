using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Sensor.Api.Domain.Eventos;
using Sensor.Api.Domain.Eventos.ServiceBus;

namespace Sensor.Api.ServiceBus.Eventos
{
    public class Evento : ServiceBusBase, IEventoWrite
    {
        public Evento(IConfiguration configuration)
            : base(configuration) { }

        public async Task SendMessagesAsync(Domain.Eventos.Evento evento)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(evento);
            var message = new Message(Encoding.UTF8.GetBytes(json));

            await queueClient.SendAsync(message);
        }
    }
}
