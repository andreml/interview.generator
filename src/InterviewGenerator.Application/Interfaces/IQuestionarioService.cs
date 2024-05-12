using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Application.Interfaces;

public interface IQuestionarioService
{
    Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario);
    Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionarioDto);
    Task<ResponseBase> ExcluirQuestionario(Guid usuarioCriacaoId, Guid idQuestionario);
    Task<ResponseBase<ICollection<QuestionarioViewModelAvaliador>>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome);
}
