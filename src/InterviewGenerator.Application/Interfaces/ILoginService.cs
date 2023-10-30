using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseBase<LoginViewModel>> BuscarTokenUsuario(GerarTokenUsuarioDto usuario);
    }
}
