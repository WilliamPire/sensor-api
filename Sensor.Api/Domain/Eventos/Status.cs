using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.Domain.Eventos
{
    public enum Status
    {
        [Description("Processado")]
        Processado,
        [Description("Erro")]
        Erro
    }
}
