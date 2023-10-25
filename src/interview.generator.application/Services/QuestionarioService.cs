using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        private readonly IQuestionarioRepositorio _questionarioRepositorio;

        public QuestionarioService(IQuestionarioRepositorio questionarioRepositorio)
        {
            _questionarioRepositorio = questionarioRepositorio;
        }

        public Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionario, Guid usuarioId)
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario, Guid usuarioId)
        {
            var response = new ResponseBase();

            var quesitonarioDuplicado = await _questionarioRepositorio.ObterPorNome(questionario.Nome);
            if (quesitonarioDuplicado != null)
            {
                response.AddErro("Questionário com esse nome já cadastrado");
                return response;
            }

            var novaPergunta = new Questionario { 
                Nome = questionario.Nome,
                DataCriacao = questionario.DataCriacao,
                UsuarioCriacaoId = usuarioId,
                TipoQuestionarioId = questionario.TipoQuestionarioId
            };

            foreach (var pergunta in questionario.Perguntas)
                novaPergunta.AdicionarPergunta(new PerguntaQuestionario { OrdemApresentacao = pergunta.OrdemApresentacao, 
                                                                          Peso = pergunta.Peso,
                                                                          PerguntaId = pergunta.PerguntaId });

            await _questionarioRepositorio.Adicionar(novaPergunta);

            response.AddData("Pergunta adicionada com sucesso!");
            return response;
        }

        public Task<ResponseBase> ExcluirQuestionario(Guid idQuestionario)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<QuestionarioViewModel>> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<QuestionarioViewModel>> ObterQuestionarioPorCandidato(Guid idCandidadto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase<QuestionarioViewModel>> ObterQuestionariosPorDescricao(string descricao)
        {
            throw new NotImplementedException();
        }

    }
}
