using System.Reflection;
using Common.Application.Commands;
using Common.Application.Events;
using Common.Application.Queries;
using Common.Infrastructure.Commands;
using Common.Infrastructure.Events;
using Common.Infrastructure.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Configurations
{
    public static class CommonModuleConfiguration
    {
        public static void ConfigureCommonModule(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, EventBus>();
            services.AddTransient<ICommandBus, CommandBus>();
            services.AddTransient<IQueryBus, QueryBus>();
        }

        public static void RegisterHandlers(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
        }
    }
}