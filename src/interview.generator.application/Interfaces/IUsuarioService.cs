using interview.generator.domain.Entidade;

namespace interview.generator.application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> ObterUsuario(string cpf);
        Task<IEnumerable<Usuario>> ListarUsuarios();
    }
}