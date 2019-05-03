using MediatR;
using Sensor.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos.Commands.Inserir
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IMediator _mediator;
        private readonly Repository.IEventoWrite _eventoWrite;
        private readonly ServiceBus.IEventoWrite _eventoWriteBus;

        public Handler(IMediator mediator,
            Repository.IEventoWrite eventoWrite,
            ServiceBus.IEventoWrite eventoWriteBus)
        {
            _mediator = mediator;
            _eventoWrite = eventoWrite;
            _eventoWriteBus = eventoWriteBus;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                //COMENTADO O USO DA MENSAGERIA POR CONTA DO TEMPO NÃO CONSEGUI PUBLICAR O COMO SERVICO NO DOCKER
                //O PROJETO CONSOLE (SENSOR PROCESSAMENTO) RESPONSÁVEL POR BAIXAR TODAS AS MENSAGENS DA FILA.
                //PARA TESTE LOCAL SOMENTE DESCOMENTAR A LILNHA E DEIXAR RODANDO O PROJETO CONSOLE.

                //await _eventoWriteBus.SendMessagesAsync(new Evento(request.TimeStamp, request.Tag, request.Valor));
                await _eventoWrite.Inserir(new Evento(request.TimeStamp, request.Tag, request.Valor, EnumMethods.GetDescription(Status.Processado)));
                await _mediator.Publish(new Notification
                {
                    TimeStamp = request.TimeStamp,
                    Tag = request.Tag,
                    Valor = request.Valor,
                    Status = EnumMethods.GetDescription(Status.Processado)
                }, cancellationToken);

                return new Response("Evento criado com sucesso.");
            }
            catch
            {
                var evento = new Evento(request.TimeStamp, request.Tag, request.Valor, EnumMethods.GetDescription(Status.Erro));
                await _eventoWrite.Inserir(evento);
                return new Response("Erro ao criar evento.");
            }
        }
    }
}
