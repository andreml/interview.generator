using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAvaliacaoRepositorio
    {
        Task Adicionar(Avaliacao entity);
        Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId);
        Task AdicionarObservacaoAvaliacao(Guid? CandidatoId, Guid? QuestionarioId, string Observacao);
    }
}