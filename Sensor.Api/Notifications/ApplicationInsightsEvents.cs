using MediatR;
using Microsoft.ApplicationInsights;
using Sensor.Api.Domain.Eventos.Commands.Inserir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Api.Notifications
{
    public class ApplicationInsightsEvents : INotificationHandler<Domain.Eventos.Commands.Inserir.Notification>
    {
        private readonly TelemetryClient telemetry = new TelemetryClient();
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
                {
                    //DEIXEI COMENTADO PARA NÃO ENVIAR EVENTOS PARA POIS TEREMOS MUITAS REQUISIÇÕES
                    //ESTOU COM POUCOS CRÉDITOS EM MINHA CONTA PARA UTILIZAR O SERVIÇO
                    
                    //telemetry.TrackEvent("Evento.Inserir", new Dictionary<string, string>
                    //{
                    //    ["TimeStamp"] = notification.TimeStamp.ToString(),
                    //    ["Tag"] = notification.Tag,
                    //    ["Valor"] = notification.Valor,
                    //});
                });
        }
    }
}
