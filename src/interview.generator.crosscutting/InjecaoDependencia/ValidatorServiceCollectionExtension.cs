using FluentValidation;
using interview.generator.application.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class ValidatorServiceCollectionExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection service)
        {
            service.AddScoped<IValidator<GerarTokenUsuarioDto>, GerarTokenUsuarioDtoValidator>();

            service.AddScoped<IValidator<AdicionarUsuarioDto>, AdicionarUsuarioDtoValidator>();
            service.AddScoped<IValidator<AlterarUsuarioDto>, AlterarUsuarioDtoValidator>();

            service.AddScoped<IValidator<AdicionarPerguntaDto>, AdicionarPerguntaDtoValidator>();
            service.AddScoped<IValidator<AlterarPerguntaDto>, AlterarPerguntaDtoValidator>();

            service.AddScoped<IValidator<AdicionarAreaConhecimentoDto>, AdicionarAreaConhecimentoDtoValidator>();
            service.AddScoped<IValidator<AlterarAreaConhecimentoDto>, AlterarAreaConhecimentoDtoValidator>();

            service.AddScoped<IValidator<AdicionarQuestionarioDto>, AdicionarQuestionarioDtoValidator>();
            service.AddScoped<IValidator<AlterarQuestionarioDto>, AlterarQuestionarioDtoValidator>();

            service.AddScoped<IValidator<AdicionarAvaliacaoDto>, AdicionarAvaliacaoDtoValidator>();
            service.AddScoped<IValidator<AdicionarObservacaoAvaliadorDto>, AdicionarObservacaoAvaliadorDtoValidator>();

            return service;
        }
    }
}
