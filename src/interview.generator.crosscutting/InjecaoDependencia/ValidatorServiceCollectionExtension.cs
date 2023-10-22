using FluentValidation;
using interview.generator.application.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class ValidatorServiceCollectionExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection service)
        {
            service.AddScoped<IValidator<AdicionarUsuarioDto>, AdicionarUsuarioDtoValidator>();
            service.AddScoped<IValidator<AlterarUsuarioDto>, AlterarUsuarioDtoValidator>();
            service.AddScoped<IValidator<AdicionarPerguntaDto>, AdicionarPerguntaDtoValidator>();

            return service;
        }
    }
}
