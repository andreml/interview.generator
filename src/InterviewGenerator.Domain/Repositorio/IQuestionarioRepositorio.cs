using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IQuestionarioRepositorio
{
    Task Adicionar(Questionario entity);
    Task<Questionario?> ObterPorIdComAvaliacoesEPerguntas(Guid usuarioCriacaoId, Guid id);
    Task<Questionario?> ObterPorNome(Guid usuarioCriacaoId, string nome);
    Task<Questionario?> ObterPorIdEUsuarioCriacao(Guid usuarioCriacaoId, Guid questionarioId);
    Task<Questionario?> ObterPorIdEUsuarioCandidato(Guid questionarioId, Guid candidatoId);
    Task Alterar(Questionario entity);
    Task Excluir(Questionario entity);
    Task<ICollection<Questionario>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome);
}
