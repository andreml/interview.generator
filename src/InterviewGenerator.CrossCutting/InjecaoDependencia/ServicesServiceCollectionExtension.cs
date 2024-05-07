using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia;

public static class ServicesServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IQuestionarioService, QuestionarioService>();         
        service.AddScoped<IAvaliacaoService, AvaliacaoService>();
        service.AddScoped<IUsuarioService, UsuarioService>();
        service.AddScoped<IPerguntaService, PerguntaService>();
        service.AddScoped<IAreaConhecimentoService, AreaConhecimentoService>();
        service.AddScoped<IImportacaoPerguntasService, ImportacaoPerguntaService>();
        service.AddScoped<IMassTransitService, MassTransitService>();

        return service;
    }
}
