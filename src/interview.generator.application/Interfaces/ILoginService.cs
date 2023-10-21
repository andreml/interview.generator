using interview.generator.application.Dto;
using interview.generator.domain.Entidade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseBase<object>> BuscaTokenUsuario(GeraTokenUsuario usuario);
    }
}
