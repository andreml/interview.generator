using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio
{
    public interface IUsuarioRepositorio : ICommonRepository<Usuario>
    {
        Task<Usuario?> ObterPorId(Guid id);
        Task<bool> ExisteUsuarioPorCpf(string cpf);
        Task<bool> ExisteUsuarioPorLogin(string login);
        Task<Usuario?> ObterUsuarioPorLoginESenha(string nome, string senha);

    }
}