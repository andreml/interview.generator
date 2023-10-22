using interview.generator.application.Dto;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IRespostaAvaliacaoService
    {
        Task<ResponseBase<Usuario>> ObterUsuario(Guid id);
        Task<RespostaAvaliacao> ObterRespostaPorPergunta(Guid PerguntaId);
        Task<ResponseBase<IEnumerable<Usuario>>> ObterRespostasAvaliacao();
        Task<ResponseBase> CadastrarRespostaAvaliacao(AdicionarRespostaAvaliacaoDtoValidator usuario);
        Task<ResponseBase> AlterarRespostaAvalicao(AlterarUsuarioDto usuario);
    }
}