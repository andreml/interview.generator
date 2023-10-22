using interview.generator.application.Dto;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseBase<Usuario>> ObterUsuario(Guid id);
        Task<ResponseBase<IEnumerable<Usuario>>> ListarUsuarios();
        Task<ResponseBase> CadastrarUsuario(AdicionarUsuarioDto usuario);
        Task<ResponseBase> AlterarUsuario(AlterarUsuarioDto usuario);
        Task<ResponseBase> ExcluirUsuario(Guid id);
    }
}