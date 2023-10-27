using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAvaliacaoRepositorio : ICommonRepository<Avaliacao>
    {
        Task<ICollection<Avaliacao>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato);
        Task<Avaliacao?> ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario);
    }
}