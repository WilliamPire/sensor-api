using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Sensor.Api.Domain.Eventos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sensor.Api
{
    public static class Setup
    {
        public static void ConfigureCookie(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.MeasureTime<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Pipelines.FailFastRequestBehavior<,>));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEventoWrite), typeof(Repository.Eventos.Evento));
            services.AddTransient(typeof(IEventoRead), typeof(Repository.Eventos.Evento));
            services.AddTransient(typeof(Domain.Eventos.ServiceBus.IEventoWrite), typeof(ServiceBus.Eventos.Evento));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Sensores de Eventos", Version = " v1" });
            });
        }
    }
}
