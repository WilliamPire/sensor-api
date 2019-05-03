using Sensor.Api.Core;
using Sensor.Api.Domain.Eventos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos.Repository
{
    public interface IEventoWrite
    {
        Task Inserir(Evento evento);
    }

    public interface IEventoRead
    {
        Task<IList<Evento>> ListarTodos();
        Task<Page<Evento>> ListarPaginado(string sort, string order, int page, int pagesize);
        GraficoEvento ListaGraficos();
        IList<TotalizadorRegiao> ListaConsololidadoSesores();
    }
}
