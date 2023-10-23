using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IAreaConhecimentoService
    {
        Task<ResponseBase<AreaConhecimentoViewModel>> ObterAreaConhecimento(Guid id);
        Task<ResponseBase<IEnumerable<AreaConhecimentoViewModel>>> ListarAreasConhecimento(Guid usuarioId);
        Task<ResponseBase> CadastrarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento, Guid usuarioId);
        Task<ResponseBase> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento, Guid usuarioId);
        Task<ResponseBase> ExcluirAreaConhecimento(Guid id);
        Task<ResponseBase<AreaConhecimentoViewModel>> ObterAreaConhecimentoPorDescricao(string descricao);
        Task<AreaConhecimento> ObterOuCriarAreaConhecimento(Guid usuarioId, string descricao);
    }
}
