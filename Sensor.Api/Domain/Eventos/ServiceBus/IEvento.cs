using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos.ServiceBus
{
    public interface IEventoWrite
    {
        Task SendMessagesAsync(Evento evento);
    }
}
