using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IUsuarioRepositorio : ICommonRepository<Usuario>
    {
        Task<Usuario?> ObterUsuarioPorCpf(string cpf);
    }
}