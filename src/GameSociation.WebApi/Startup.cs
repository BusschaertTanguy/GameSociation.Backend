using Account.Infrastructure.Configurations;
using Association.Infrastructure.Configurations;
using Common.Infrastructure.Configurations;
using GameSociation.WebApi.Configurations;
using GameSociation.WebApi.Exceptions;
using Library.MartenEventStore.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameSociation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureWebApi(Configuration);
            services.ConfigureCommonModule();
            services.ConfigureMartenEventStore(Configuration);
            services.ConfigureAccountModule(Configuration);
            services.ConfigureAssociationModule();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionMiddleware();
            app.UseWebApi();
        }
    }
}