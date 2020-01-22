using Account.Domain.Repositories;
using Association.Domain.Repositories;
using Library.EntityFramework.Contexts;
using Library.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.EntityFramework.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameSociationContext>(options => options.UseSqlServer(configuration.GetConnectionString("GameSociationContext")));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAssociateRepository, AssociateRepository>();
            services.AddTransient<IAssociationRepository, AssociationRepository>();
        }
    }
}
