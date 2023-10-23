using interview.generator.application.Dto;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAreaConhecimentoService
    {
        Task<ResponseBase<AreaConhecimento>> ObterAreaConhecimento(Guid id);
        Task<ResponseBase<IEnumerable<AreaConhecimento>>> ListarAreasConhecimento();
        Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento, Guid usuarioId);
        Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento, Guid usuarioId);
        Task<ResponseBase> ExcluirAreaConhecimento(Guid id);
        Task<ResponseBase<AreaConhecimento>> ObterAreaConhecimentoPorDescricao(string descricao);
        Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioId, string descricao);
    }
}
