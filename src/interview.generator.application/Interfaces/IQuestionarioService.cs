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
        Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario);
        Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionario);
        Task<ResponseBase> ExcluirQuestionario(Guid idQuestionario);
        Task<ResponseBase<QuestionarioViewModel>> ObterQuestionarioPorCandidato(Guid idCandidadto);
        Task<ResponseBase<QuestionarioViewModel>> ObterQuestionariosPorFiltro(string descricao);

    }
}
