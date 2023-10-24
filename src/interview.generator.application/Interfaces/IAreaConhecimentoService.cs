using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAreaConhecimentoService
    {
        Task<ResponseBase<IEnumerable<AreaConhecimentoViewModel>>> ListarAreasConhecimento(Guid usuarioId, Guid areaConhecimentoId, string? descricao);
        Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento);
        Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento);
        Task<ResponseBase> ExcluirAreaConhecimento(Guid id, Guid usuarioId);
        Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioId, string descricao);
    }
}
