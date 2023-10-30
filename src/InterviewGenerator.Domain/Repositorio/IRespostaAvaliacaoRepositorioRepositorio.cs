using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio
{
    public interface IRespostaAvaliacaoRepositorio
    {
        Task<RespostaAvaliacao> ObterRespostaPorPergunta(Guid PerguntaId);
        Task<IEnumerable<RespostaAvaliacao>> ObterTodos();
        Task<RespostaAvaliacao?> ObterPorId(Guid id);
        Task Excluir(Guid id);
        Task Adicionar(RespostaAvaliacao entity);
        Task Alterar(RespostaAvaliacao entity);
    }
}
