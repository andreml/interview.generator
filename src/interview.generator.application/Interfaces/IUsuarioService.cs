using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseBase<UsuarioViewModel>> ObterUsuario(Guid id);
        Task<ResponseBase<IEnumerable<UsuarioViewModel>>> ListarUsuarios();
        Task<ResponseBase> CadastrarUsuario(AdicionarUsuarioDto usuario);
        Task<ResponseBase> AlterarUsuario(AlterarUsuarioDto usuario);
        Task<ResponseBase> ExcluirUsuario(Guid id);
    }
}