using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;

namespace interview.generator.application.Interfaces
{
    public interface IQuestionarioService
    {
        Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario);
        Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionarioDto);
        Task<ResponseBase> ExcluirQuestionario(Guid usuarioCriacaoId, Guid idQuestionario);
        Task<ResponseBase<ICollection<QuestionarioViewModel>>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome);

    }
}
