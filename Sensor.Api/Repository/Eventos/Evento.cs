using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public async Task Inserir(Domain.Eventos.Evento evento)
            => await Collection.InsertOneAsync(evento);

        public async Task<IList<Domain.Eventos.Evento>> ListarPorRegiao(string regiao)
            => await Collection.Find(x => x.Tag.Contains(regiao)).ToListAsync();

        public async Task<IList<Domain.Eventos.Evento>> ListarPorSensor(string sensor)
            => await Collection.Find(x => x.Tag.Contains(sensor)).ToListAsync();

        public async Task<IList<Domain.Eventos.Evento>> ListarTodos()
            => await Collection.Find(x => true).ToListAsync();

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
                        totalizadorRegiao.TotalizadorSensores.Add(new TotalizadorSensor { Sensor = $"brasil.{sensorRegiao.Regiao.ToLower()}.{sensorNome} - {countSensoresPorRegiao}", Total = countSensoresPorRegiao });
                }
                totalizadorRegiao.Total = totalizadorRegiao.TotalizadorSensores.Sum(x => x.Total);
                totalizadorRegiao.Regiao = $"brasil.{sensorRegiao.Regiao.ToLower()} - {totalizadorRegiao.Total}";

                totalizadoresRegiao.Add(totalizadorRegiao);
            }
        }

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
    }
}
