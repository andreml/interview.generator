using InterviewGenerator.Domain.Entidade;

namespace InterviewGenerator.Domain.Repositorio;

public interface IAreaConhecimentoRepositorio : ICommonRepository<AreaConhecimento>
{
    Task Excluir(AreaConhecimento entity);
    Task<AreaConhecimento?> ObterPorIdComPerguntas(Guid usuarioCriacaoId, Guid id);
    Task<AreaConhecimento?> ObterPorIdEUsuarioId(Guid usuarioCriacaoId, Guid id);
    Task<AreaConhecimento?> ObterPorDescricaoEUsuarioId(Guid usuarioCriacaoId, string descricao);
    Task<IEnumerable<AreaConhecimento>> ObterAreaConhecimentoComPerguntas(Guid usuarioCriacaoId, Guid areaConhecimentoId, string? descricao);
}