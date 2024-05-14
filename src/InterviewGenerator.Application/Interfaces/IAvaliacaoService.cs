using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces;

public interface IAvaliacaoService
{
    Task<ResponseBase> ResponderAvaliacao(ResponderAvaliacaoDto entity);
    Task<ResponseBase<AvaliacaoDetalheViewModel>> ObterDetalheAvaliacaoAsync(Guid usuarioIdCriacaoQuestionario, Guid avaliacaoId);
    Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj);
    Task<ResponseBase<ICollection<AvaliacaoCandidatoViewModel>>> ObterAvaliacoesCandidato(Guid candidatoId);
    Task<ResponseBase> EnviarAvaliacaoParaCandidatoAsync(EnviarAvaliacaoParaCandidatoDto dto);
    Task<ResponseBase<ResponderAvaliacaoViewModel>> ObterAvaliacaoParaResponderAsync(Guid candidatoId, Guid avaliacaoId);
    Task<ResponseBase<AvaliacoesQuestionarioViewModel>> ObterAvaliacoesEnviadasDeUmQuestionarioAsync(Guid usuarioAvaliadorId, Guid questionarioId);
}