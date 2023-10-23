using interview.generator.domain.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.Config
{
    public static class ConfigOptionsExtension
    {
        public static IServiceCollection AddConfigOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("DatabaseConfig"));
            return services;
        }
    }
}
