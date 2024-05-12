using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IAvaliacaoRepositorio : ICommonRepository<Avaliacao>
{
    Task<ICollection<Avaliacao>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato);
    Task<Avaliacao?> ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario);
    Task<Avaliacao?> ObterAvaliacaoPorIdECandidato(Guid id, Guid candidatoId);
    Task<ICollection<Avaliacao>> ObterAvaliacaoPorCandidatoId(Guid candidatoId);
}