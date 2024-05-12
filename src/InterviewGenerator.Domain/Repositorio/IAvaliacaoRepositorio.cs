using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IAvaliacaoRepositorio : ICommonRepository<Avaliacao>
{
    Task<Avaliacao?> ObterPorIdEUsuarioCriacaoQuestionarioAsync(Guid usuarioIdCriacaoQuestionario, Guid avaliacaoId);
    Task<Avaliacao?> ObterPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario);
    Task<Avaliacao?> ObterPorIdECandidatoId(Guid id, Guid candidatoId);
    Task<ICollection<Avaliacao>> ObterPorCandidatoId(Guid candidatoId);
    Task<ICollection<Avaliacao>> ObterPorUsuarioCriacaoEQuestionarioId(Guid usuarioCriacaoId, Guid questionarioId);
}