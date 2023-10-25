using interview.generator.application.Dto;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Interfaces
{
    public interface IQuestionarioService
    {
        Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario, Guid usuarioId);
        Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionario, Guid usuarioId);
        Task<ResponseBase> ExcluirQuestionario(Guid idQuestionario);
        Task<ResponseBase<QuestionarioViewModel>> ObterQuestionarioPorCandidato(Guid idCandidato);
        Task<ResponseBase<QuestionarioViewModel>> ObterPorId(Guid id);
        Task<ResponseBase<QuestionarioViewModel>> ObterQuestionariosPorDescricao(string descricao);

    }
}
