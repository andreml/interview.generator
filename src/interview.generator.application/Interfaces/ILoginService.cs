using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseBase<LoginViewModel>> BuscarTokenUsuario(GeraTokenUsuario usuario);
    }
}
