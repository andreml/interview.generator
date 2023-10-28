using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<ResponseBase> AdicionarAvaliacao(AdicionarAvaliacaoDto entity);
        Task<ResponseBase<ICollection<AvaliacaoViewModel>>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato);
        Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj);
    }
}