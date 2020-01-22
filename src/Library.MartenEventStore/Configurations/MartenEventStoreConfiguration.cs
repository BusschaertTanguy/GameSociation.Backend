using Account.Domain.Repositories;
using Association.Domain.Repositories;
using Common.Application.Queries;
using Library.MartenEventStore.Projections;
using Library.MartenEventStore.Queries;
using Library.MartenEventStore.Repositories;
using Marten;
using Marten.Services.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.MartenEventStore.Configurations
{
    public static class MartenEventStoreConfiguration
    {
        public static void ConfigureMartenEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => CreateStore(configuration.GetConnectionString("GameSociationDB")));
            services.AddScoped(provider => provider.GetService<IDocumentStore>().QuerySession());
            services.AddScoped(provider => provider.GetService<IDocumentStore>().LightweightSession());

            services.AddTransient<IQueryProcessor, MartenEventStoreQueryProcessor>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAssociateRepository, AssociateRepository>();
            services.AddTransient<IAssociationRepository, AssociationRepository>();
        }

        private static IDocumentStore CreateStore(string connectionString)
        {
            var store = DocumentStore.For(_ =>
            {
                _.Connection(connectionString);
                _.Events.UseAggregatorLookup(AggregationLookupStrategy.UsePrivateApply);

                _.Events.InlineProjections.Add<AccountProjection>();
                _.Events.InlineProjections.Add<AssociateProjection>();
                _.Events.InlineProjections.Add<AssociationProjection>();
            });

            return store;
        }
    }
}