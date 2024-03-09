using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Infra.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewGenerator.CrossCutting.InjecaoDependencia
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        { 
            service.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            service.AddScoped<IPerguntaRepositorio, PerguntaRepositorio>();
            service.AddScoped<IAreaConhecimentoRepositorio, AreaConhecimentoRepositorio>();
            service.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
            service.AddScoped<IQuestionarioRepositorio, QuestionarioRepositorio>();
            service.AddScoped<IControleImportacaoPerguntasRepositorio, ControleImportacaoRepositorio>();
            service.AddScoped<ILinhasArquivoRepositorio, LinhasArquivoRepositorio>();

            return service;
        }
    }
}
