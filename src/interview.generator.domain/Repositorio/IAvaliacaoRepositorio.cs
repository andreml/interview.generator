using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAvaliacaoRepositorio : ICommonRepository<Avaliacao>
    {
        Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid? CandidatoId, Guid? QuestionarioId);
        Task<Avaliacao?> ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(Guid id, Guid usuarioIdCriacaoQuestionario);
    }
}