using interview.generator.application.Interfaces;
using interview.generator.application.Services;
using interview.generator.domain.Repositorio;
using interview.generator.infraestructure.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        { 
            service.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
            service.AddSingleton<IPerguntaRepositorio, PerguntaRepositorio>();
            service.AddSingleton<IAreaConhecimentoRepositorio, AreaConhecimentoRepositorio>();
            service.AddSingleton<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
            service.AddSingleton<IQuestionarioRepositorio, QuestionarioRepositorio>();


            service.AddSingleton<ILoginService, LoginService>();
            service.AddSingleton<IUsuarioService, UsuarioService>();
            service.AddSingleton<IPerguntaService, PerguntaService>();
            service.AddSingleton<IAreaConhecimentoService, AreaConhecimentoService>();
            service.AddSingleton<IQuestionarioService, QuestionarioService>();
          
            service.AddScoped<IAvaliacaoService, AvaliacaoService>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IPerguntaService, PerguntaService>();
            service.AddScoped<IAreaConhecimentoService, AreaConhecimentoService>();


            return service;
        }
    }
}
