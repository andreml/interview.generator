using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IPerguntaRepositorio
    {
        Task Adicionar(Pergunta entity);
        Task<bool> ExistePorDescricao(string descricao, Guid usuarioId);
        IEnumerable<Pergunta> ObterPerguntas(Guid usuarioId, Guid perguntaId, string? areaConhecimento, string? descricao);
        Task<Pergunta?> ObterPerguntaPorId(Guid usuarioId, Guid perguntaId);
        Task Alterar(Pergunta entity);
        Task Excluir(Pergunta entity);
    }
}