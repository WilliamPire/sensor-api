using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Sensor.Api.Core;
using Sensor.Api.Domain.Eventos;
using Sensor.Api.Domain.Eventos.Models;
using Sensor.Api.Domain.Eventos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Repository.Eventos
{
    public class Evento : RepositoryBase<Domain.Eventos.Evento>, IEventoWrite, IEventoRead
    {
        public Evento(IConfiguration configuration)
            : base(configuration) { }

        public async Task<Page<Domain.Eventos.Evento>> ListarPaginado(string sort, string order, int page, int pagesize)
        {
            var result = new Page<Domain.Eventos.Evento>();
            var registros = Collection.Find(x => true);

            result.TotalRegistros = await registros.CountDocumentsAsync();
            result.Items = await registros.Skip((page - 1) * pagesize)
                                          .Limit(pagesize)
                                          .Sort(order == "desc" ? Builders<Domain.Eventos.Evento>.Sort.Descending(sort) : Builders<Domain.Eventos.Evento>.Sort.Ascending(sort))
                                          .ToListAsync();
            return result;
        }

        public async Task Inserir(Domain.Eventos.Evento evento)
            => await Collection.InsertOneAsync(evento);

        public async Task<IList<Domain.Eventos.Evento>> ListarTodos()
            => await Collection.Find(x => true).ToListAsync();

        public GraficoEvento ListaGraficos()
        {
            var result = Collection.Find(x => true).ToList();

            var agrupados = result.GroupBy(p => p.Status, (key, g) => new { Status = key, Count = g.Count() });

            GraficoEvento graficos = new GraficoEvento();


            foreach (var item in agrupados)
            {
                graficos.Labels.Add(item.Status);
                graficos.Data.Add(item.Count);
            }

            return graficos;
        }

        public IList<TotalizadorRegiao> ListaConsololidadoSesores()
        {
            var regioeos = new List<string>() { "Norte", "Nordeste", "Sul", "Sudeste" };
            var result = Collection.Find(x => true).ToList();

            List<TotalizadorRegiao> totalizadoresRegiao = new List<TotalizadorRegiao>();
            List<SensorRegiao> sensoresRegiao = new List<SensorRegiao>();

            ObterSensoresPorRegiao(regioeos, result, sensoresRegiao);
            ObterModelSensoresRegiao(result, totalizadoresRegiao, sensoresRegiao);

            return totalizadoresRegiao;
        }

        #region MÉTODOS PRIVADOS

        private static void ObterSensoresPorRegiao(List<string> regioeos, List<Domain.Eventos.Evento> result, List<SensorRegiao> sennn)
        {
            foreach (var regiao in regioeos)
            {
                var registrosPorRegiao = result.Where(x => x.Tag.ToLower().Contains(regiao.ToLower()));
                SensorRegiao sensorRegiao = new SensorRegiao(regiao);

                foreach (var porRegiao in registrosPorRegiao)
                {
                    var sensorNome = porRegiao.Tag.Split(".").SingleOrDefault(x => x.ToLower().Contains("sensor"));
                    if (sensorNome != null && !sensorRegiao.Sensores.Any(x => x == sensorNome))
                        sensorRegiao.Sensores.Add(sensorNome);
                }

                sennn.Add(sensorRegiao);
            }
        }

        private static void ObterModelSensoresRegiao(List<Domain.Eventos.Evento> result, List<TotalizadorRegiao> totalizadoresRegiao, List<SensorRegiao> sensoresRegiao)
        {
            foreach (var sensorRegiao in sensoresRegiao)
            {
                var totalizadorRegiao = new TotalizadorRegiao();
                foreach (var sensorNome in sensorRegiao.Sensores)
                {
                    var countSensoresPorRegiao = result.Count(x => x.Tag.ToLower().Contains(sensorNome.ToLower()) && x.Tag.ToLower().Contains(sensorRegiao.Regiao.ToLower()));
                    if (countSensoresPorRegiao > 0)
                        totalizadorRegiao.TotalizadorSensores.Add(new TotalizadorSensor { Sensor = $"brasil.{sensorRegiao.Regiao.ToLower()}.{sensorNome}", Total = countSensoresPorRegiao });
                }
                totalizadorRegiao.Total = totalizadorRegiao.TotalizadorSensores.Sum(x => x.Total);
                totalizadorRegiao.Regiao = $"brasil.{sensorRegiao.Regiao.ToLower()}";

                totalizadoresRegiao.Add(totalizadorRegiao);
            }
        }

        #endregion
    }
}
