using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        public Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionario)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> ExcluirQuestionario(Guid idQuestionario)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<QuestionarioViewModel>> ObterQuestionarioPorCandidato(Guid idCandidadto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<QuestionarioViewModel>> ObterQuestionariosPorFiltro(string descricao)
        {
            throw new NotImplementedException();
        }
    }
}
