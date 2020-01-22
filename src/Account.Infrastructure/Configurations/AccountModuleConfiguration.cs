using Account.Application.Configurations;
using Account.Application.Services;
using Common.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure.Configurations
{
    public static class AccountModuleConfiguration
    {
        public static void ConfigureAccountModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(cfg =>
            {
                cfg.Secret = configuration.GetSection("Token:Secret").Value;
                cfg.Expiration = int.Parse(configuration.GetSection("Token:Expiration").Value);
            });

            services.AddTransient<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>();
            services.RegisterHandlers(typeof(AccountApplicationConfiguration).Assembly);
        }
    }
}