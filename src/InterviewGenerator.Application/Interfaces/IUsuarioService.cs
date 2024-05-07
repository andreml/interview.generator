using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces;

public interface IUsuarioService
{
    Task<ResponseBase<UsuarioViewModel>> ObterUsuario(Guid id);
    Task<ResponseBase> CadastrarUsuario(AdicionarUsuarioDto usuario);
    Task<ResponseBase> AlterarUsuario(AlterarUsuarioDto usuario);
    Task<ResponseBase<LoginViewModel>> BuscarTokenUsuario(GerarTokenUsuarioDto usuario);
}