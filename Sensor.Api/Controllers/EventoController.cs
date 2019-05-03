using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sensor.Api.Core;
using Sensor.Api.Domain.Eventos.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEventoRead _repositorio;

        public EventoController(IMediator mediator, IEventoRead repositorio)
        {
            _mediator = mediator;
            _repositorio = repositorio;
        }

        // GET: api/Evento
        [ProducesResponseType(200, Type = typeof(IList<Domain.Eventos.Evento>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Domain.Eventos.Evento> eventos =
               await _repositorio.ListarTodos();

            return Ok(eventos);
        }

        [ProducesResponseType(200, Type = typeof(Page<Domain.Eventos.Evento>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("Sensor/Paginado")]
        public async Task<IActionResult> GetPage(string sort, string order, int page, int pagesize)
        {
            Page<Domain.Eventos.Evento> eventos =
               await _repositorio.ListarPaginado(sort,order, page, pagesize);

            return Ok(eventos);
        }

        // GET: api/Evento/Sensor/Consolidado
        [ProducesResponseType(200, Type = typeof(IList<Domain.Eventos.Models.TotalizadorRegiao>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("Sensor/Consolidado")]
        public IActionResult PosicaoConsolidada()
        {   
            IList<Domain.Eventos.Models.TotalizadorRegiao> consolidado =
              _repositorio.ListaConsololidadoSesores();

            return Ok(consolidado);
        }

        // GET: api/Evento/Sensor/Consolidado
        [ProducesResponseType(200, Type = typeof(IList<Domain.Eventos.Models.GraficoEvento>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("Sensor/Grafico")]
        public IActionResult GraficoPorStatus()
        {
            Domain.Eventos.Models.GraficoEvento graficoStatus =
              _repositorio.ListaGraficos();

            return Ok(graficoStatus);
        }

        // POST: api/Evento
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Eventos.Commands.Inserir.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Errors.Any())
                return BadRequest(response.Errors);

            return Ok(response.Result);
        }

        // GET: api/Evento
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("Sensor/Teste")]
        public IActionResult TesteFunction()
        {
            return Ok("API sensor respondendo com sucesso.");
        }
    }
}
