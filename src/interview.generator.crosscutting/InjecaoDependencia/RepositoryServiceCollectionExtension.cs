using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.crosscutting.InjecaoDependencia
{
    public class RepositoryServiceCollectionExtension
    {
        public IServiceCollection AddRepository(IServiceCollection service)
        {
            return service;
        }
    }
}
