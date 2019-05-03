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
        Task<IList<Evento>> ListarPorRegiao(string regiao);
        Task<IList<Evento>> ListarPorSensor(string regiao);
        GraficoEvento ListaGraficos();
        IList<TotalizadorRegiao> ListaConsololidadoSesores();
    }
}
