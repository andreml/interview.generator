using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IPerguntaRepositorio
    {
        Task Adicionar(Pergunta entity);
        Task<bool> ExistePorDescricao(string descricao, Guid usuarioId);
        IEnumerable<Pergunta> ObterTodasPorUsuarioId(Guid usuarioId);
    }
}