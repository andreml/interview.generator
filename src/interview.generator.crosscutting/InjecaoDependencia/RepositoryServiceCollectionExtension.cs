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
            service.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            service.AddScoped<IPerguntaRepositorio, PerguntaRepositorio>();
            service.AddScoped<IAreaConhecimentoRepositorio, AreaConhecimentoRepositorio>();
            service.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
            service.AddScoped<IQuestionarioRepositorio, QuestionarioRepositorio>();


            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IPerguntaService, PerguntaService>();
            service.AddScoped<IAreaConhecimentoService, AreaConhecimentoService>();
            service.AddScoped<IQuestionarioService, QuestionarioService>();
          
            service.AddScoped<IAvaliacaoService, AvaliacaoService>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IPerguntaService, PerguntaService>();
            service.AddScoped<IAreaConhecimentoService, AreaConhecimentoService>();


            return service;
        }
    }
}
