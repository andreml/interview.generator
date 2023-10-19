using interview.generator.domain.Entidade;

namespace interview.generator.application.Interfaces
{
    public interface IPerfilService
    {
        Task<Perfil> ObterPerfil(Guid id);
        Task<IEnumerable<Perfil>> ListarPerfils();
        Task CadastrarPerfil(Perfil perfil);
        Task AlterarPerfil(Perfil perfil);
        Task ExcluirPerfil(Guid id);
    }
}
