using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IPerguntaRepositorio : ICommonRepository<Pergunta>
{
    Task<bool> ExistePorDescricao(Guid usuarioCriacaoId, string descricao);
    IEnumerable<Pergunta> ObterPerguntas(Guid usuarioCriacaoId, Guid perguntaId, string? areaConhecimento, string? descricao);
    Task<Pergunta?> ObterPerguntaPorId(Guid usuarioCriacaoId, Guid perguntaId);
    Task Excluir(Pergunta entity);
}