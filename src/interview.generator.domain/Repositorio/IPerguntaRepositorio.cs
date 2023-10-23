using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IPerguntaRepositorio : ICommonRepository<Pergunta>
    {
        Task<bool> ExistePorDescricao(string descricao, Guid usuarioId);
    }
}