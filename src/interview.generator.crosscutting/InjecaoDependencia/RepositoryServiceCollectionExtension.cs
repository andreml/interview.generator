using FluentValidation;
using interview.generator.application.Interfaces;
using interview.generator.application.Services;
using interview.generator.domain.Entidade;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddSingleton<IUsuarioService, UsuarioService>();
            service.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
            service.AddScoped<IValidator<Usuario>, UsuarioValidator>();

            return service;
        }
    }
}
