using FluentValidation;
using InterviewGenerator.Application.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia;

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
