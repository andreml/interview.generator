using Microsoft.Extensions.DependencyInjection;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            return service;
        }
    }
}
