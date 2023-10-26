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

        public async Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionarioDto, Guid usuarioId)
        {
            var response = new ResponseBase();

            var questionario = await _questionarioRepositorio.ObterPorNome(questionarioDto.Nome);

            if (questionario == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            //TODO: Validar se existem questionários com a pergunta

            questionario.Nome = questionarioDto.Nome;
            questionario.TipoQuestionarioId = questionarioDto.TipoQuestionarioId;

            questionario.RemoverPerguntas();
            await _questionarioRepositorio.Alterar(questionario);

            foreach (var pergunta in questionarioDto.Perguntas)
                questionario.AdicionarPergunta(new PerguntaQuestionario { PerguntaId = pergunta.PerguntaId, Peso = pergunta.Peso, OrdemApresentacao = pergunta.OrdemApresentacao });

            await _questionarioRepositorio.Alterar(questionario);

            response.AddData("Questionario alterado com sucesso!");
            return response;
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

            response.AddData("Questionario adicionado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> ExcluirQuestionario(Guid idQuestionario)
        {
            var response = new ResponseBase();

            var questionario = await _questionarioRepositorio.ObterPorId(idQuestionario);
            if (questionario == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            //TODO: Adicionar validação de questionários cadastrados com essa pergunta
            //Se tiver, impedir a exclusão

            await _questionarioRepositorio.Excluir(questionario);

            response.AddData("Pergunta excluída com sucesso!");
            return response;
        }

        public ResponseBase<QuestionarioViewModel> ObterPorId(Guid id)
        {
            var response = new ResponseBase<QuestionarioViewModel>();

            var perguntas = _questionarioRepositorio.ObterPorId(id);

            if (perguntas == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            var questionarioViewModel = new QuestionarioViewModel
            {
                DataCriacao = perguntas.Result.DataCriacao,
                Nome = perguntas.Result.Nome,
                TipoQuestionarioId = perguntas.Result.TipoQuestionarioId,
                UsuarioCriacaoId = perguntas.Result.UsuarioCriacaoId,
            };

            foreach (var item in perguntas.Result.PerguntasQuestionario)
                questionarioViewModel.Perguntas.Add(new PerguntaQuestionarioViewModel(item.Id.ToString(),
                                                                                      item.PerguntaId.ToString(),
                                                                                      item.QuestionarioId.ToString(),
                                                                                      item.OrdemApresentacao, item.Peso));
            
            response.AddData(questionarioViewModel);

            return response;
        }

        public ResponseBase<QuestionarioViewModel> ObterQuestionarioPorCandidato(Guid idCandidato)
        {
            var response = new ResponseBase<QuestionarioViewModel>();

            var perguntas = _questionarioRepositorio.ObterPorCandidato(idCandidato);

            if (perguntas == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            var questionarioViewModel = new QuestionarioViewModel
            {
                DataCriacao = perguntas.Result.DataCriacao,
                Nome = perguntas.Result.Nome,
                TipoQuestionarioId = perguntas.Result.TipoQuestionarioId,
                UsuarioCriacaoId = perguntas.Result.UsuarioCriacaoId
            };

            foreach (var item in perguntas.Result.PerguntasQuestionario)
                questionarioViewModel.Perguntas.Add(new PerguntaQuestionarioViewModel(item.Id.ToString(),
                                                                                      item.PerguntaId.ToString(),
                                                                                      item.QuestionarioId.ToString(),
                                                                                      item.OrdemApresentacao, item.Peso));

            response.AddData(questionarioViewModel);

            return response;
        }

        public ResponseBase<QuestionarioViewModel> ObterQuestionariosPorDescricao(string descricao)
        {
            var response = new ResponseBase<QuestionarioViewModel>();

            var perguntas = _questionarioRepositorio.ObterPorNome(descricao);

            if (perguntas == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            var questionarioViewModel = new QuestionarioViewModel
            {
                DataCriacao = perguntas.Result.DataCriacao,
                Nome = perguntas.Result.Nome,
                TipoQuestionarioId = perguntas.Result.TipoQuestionarioId,
                UsuarioCriacaoId = perguntas.Result.UsuarioCriacaoId
            };

            foreach (var item in perguntas.Result.PerguntasQuestionario)
                questionarioViewModel.Perguntas.Add(new PerguntaQuestionarioViewModel(item.Id.ToString(),
                                                                                      item.PerguntaId.ToString(),
                                                                                      item.QuestionarioId.ToString(),
                                                                                      item.OrdemApresentacao, item.Peso));

            response.AddData(questionarioViewModel);

            return response;
        }

    }
}
