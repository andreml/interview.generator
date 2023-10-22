using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IRespostaAvaliacaoRepositorioRepositorio : ICommonRepository<RespostaAvaliacao>
    {
        Task<RespostaAvaliacao> ObterRespostaPorPergunta(Guid PerguntaId);
    }
}
