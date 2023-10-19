using interview.generator.application.Interfaces;
using interview.generator.application.Services;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            return service;
        }
    }
}
