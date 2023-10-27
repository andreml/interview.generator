using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IQuestionarioRepositorio
    {
        Task Adicionar(Questionario entity);
        Task<Questionario?> ObterPorIdComAvaliacoesEPerguntas(Guid usuarioCriacaoId, Guid id);
        Task<Questionario?> ObterPorNome(Guid usuarioCriacaoId, string nome);
        Task Alterar(Questionario entity);
        Task Excluir(Questionario entity);
        Task<ICollection<Questionario>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome);
    }
}
