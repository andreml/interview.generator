using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces
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
