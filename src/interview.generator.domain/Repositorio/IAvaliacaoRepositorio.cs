using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAvaliacaoRepositorio
    {
        Task Adicionar(Avaliacao entity);
        Task Alterar(Avaliacao entity);
        Task<Avaliacao?> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId, DateTime? DataAplicacao);
        Task<IEnumerable<Avaliacao>> ObterTodos();
    }
}