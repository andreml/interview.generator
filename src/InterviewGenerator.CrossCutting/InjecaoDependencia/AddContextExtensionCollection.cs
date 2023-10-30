using InterviewGenerator.Infra.Context;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia
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
