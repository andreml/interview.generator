using interview.generator.application.Dto;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<ResponseBase> AdicionarAvaliacao(AdicionarAvaliacaoDto entity);
        Task<ResponseBase> AlterarAvaliacao(AlterarAvaliacaoDto entity);
        Task<ResponseBase<Avaliacao>> ObterAvaliacaoPorFiltro(Guid? CandidatoId, Guid? QuestionarioId, DateTime? DataAplicacao);
        Task<ResponseBase<IEnumerable<Avaliacao>>> ObterTodos();
    }
}