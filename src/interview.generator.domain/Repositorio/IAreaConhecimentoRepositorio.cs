using interview.generator.domain.Entidade;

namespace interview.generator.domain.Repositorio
{
    public interface IAreaConhecimentoRepositorio : ICommonRepository<AreaConhecimento>
    {
        Task<AreaConhecimento?> ObterPorIdEUsuarioId(Guid id, Guid usuarioId);
        Task<AreaConhecimento?> ObterPorDescricao(string descricao);
    }
}