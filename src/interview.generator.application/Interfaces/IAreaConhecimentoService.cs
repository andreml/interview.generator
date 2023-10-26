using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAreaConhecimentoService
    {
        Task<ResponseBase<IEnumerable<AreaConhecimentoViewModel>>> ListarAreasConhecimento(Guid usuarioCriacaoId, Guid areaConhecimentoId, string? descricao);
        Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento);
        Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento);
        Task<ResponseBase> ExcluirAreaConhecimento(Guid usuarioCriacaoId, Guid id);
        Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioCriacaoId, string descricao);
    }
}
