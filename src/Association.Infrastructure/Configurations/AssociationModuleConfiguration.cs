using Association.Application.Configurations;
using Association.Application.Services;
using Common.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Association.Infrastructure.Configurations
{
    public static class AssociationModuleConfiguration
    {
        public static void ConfigureAssociationModule(this IServiceCollection services)
        {
            services.AddTransient<ITagService, TagService>();
            services.RegisterHandlers(typeof(AssociationApplicationConfiguration).Assembly);
        }
    }
}