using interview.generator.application.Dto;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IRespostaAvaliacaoService
    {
        Task<ResponseBase<RespostaAvaliacao>> ObterRespostaPorPergunta(Guid PerguntaId);
        Task<ResponseBase<RespostaAvaliacao>> ObterRespostasPorAvaliacao(Guid AvaliacaoId);
        Task<ResponseBase<IEnumerable<RespostaAvaliacao>>> ObterTodos();
        Task<ResponseBase> AdicionarRespostaAvaliacao(AdicionarRespostaAvaliacaoDto entity);
        Task<ResponseBase> AlterarRespostaAvaliacao(AlterarRespostaAvaliacaoDto entity);
    }
}