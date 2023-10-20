using interview.generator.infraestructure.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class AddContextExtensionCollection
    {
        public static IServiceCollection AddContextConfig(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Singleton);
            return services;
        }
    }
}
