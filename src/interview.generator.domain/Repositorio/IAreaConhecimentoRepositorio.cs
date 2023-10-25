using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAreaConhecimentoRepositorio : ICommonRepository<AreaConhecimento>
    {
        Task Excluir(AreaConhecimento entity);
        Task<AreaConhecimento?> ObterPorIdComPerguntas(Guid id, Guid usuarioId);
        Task<AreaConhecimento?> ObterPorDescricaoEUsuarioId(string descricao, Guid usuarioId);
        Task<AreaConhecimento?> ObterPorDescricao(string descricao);
        Task<IEnumerable<AreaConhecimento>> ObterAreaConhecimentoComPerguntas(Guid usuarioId, Guid areaConhecimentoId, string? descricao);
    }
}