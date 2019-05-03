using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.Api.ServiceBus
{
    public abstract class ServiceBusBase
    {
        protected IQueueClient queueClient { get; }

        public ServiceBusBase(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ServiceBus");
            var queueName = configuration.GetConnectionString("QueueName");

            this.queueClient = new QueueClient(connectionString, queueName);
        }
    }
}
