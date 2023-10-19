using interview.generator.application.DTO;
using interview.generator.domain.Entidade;

namespace interview.generator.application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> ObterUsuario(Guid id);
        Task<IEnumerable<Usuario>> ListarUsuarios();
        Task CadastrarUsuario(AdicionarUsuarioDTO usuario);
        Task AlterarUsuario(Usuario usuario);
        Task ExcluirUsuario(Guid id);
    }
}