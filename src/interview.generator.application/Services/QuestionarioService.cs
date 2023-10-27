using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
{
    public class QuestionarioService : IQuestionarioService
    {
        private readonly IQuestionarioRepositorio _questionarioRepositorio;
        private readonly IPerguntaRepositorio _perguntaRepositorio;

        public QuestionarioService(IQuestionarioRepositorio questionarioRepositorio, IPerguntaRepositorio perguntaRepositorio)
        {
            _questionarioRepositorio = questionarioRepositorio;
            _perguntaRepositorio = perguntaRepositorio;
        }

        public async Task<ResponseBase> AlterarQuestionario(AlterarQuestionarioDto questionarioDto)
        {
            var response = new ResponseBase();


            var questionario = await _questionarioRepositorio.ObterPorIdComAvaliacoesEPerguntas(questionarioDto.UsuarioId, questionarioDto.QuestionarioId);
            if(questionario == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            if(questionario.Avaliacoes != null && questionario.Avaliacoes.Count > 0)
            {
                response.AddErro("Não é possível alterar o questionário, existem avaliações feitas");
                return response;
            }

            if(questionarioDto.Nome != questionario.Nome)
            {
                var questionarioPorNome = await _questionarioRepositorio.ObterPorNome(questionarioDto.UsuarioId, questionarioDto.Nome);
                if(questionarioPorNome != null && questionarioPorNome.Id != questionarioDto.QuestionarioId)
                {
                    response.AddErro("Já existe um questionário com este nome");
                    return response;
                }
            }

            questionario.RemoverPerguntas();
            questionario.Nome = questionarioDto.Nome;

            foreach (var pergunta in questionarioDto.Perguntas)
            {
                var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionarioDto.UsuarioId, pergunta.PerguntaId);
                if (perguntaExistente == null)
                {
                    response.AddErro($"Pergunta {pergunta.PerguntaId} não encontrada");
                    return response;
                }

                questionario.AdicionarPergunta(new PerguntaQuestionario
                {
                    OrdemApresentacao = pergunta.OrdemApresentacao,
                    Pergunta = perguntaExistente
                });
            }

            await _questionarioRepositorio.Alterar(questionario);

            response.AddData("Questionario alterado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> CadastrarQuestionario(AdicionarQuestionarioDto questionario)
        {
            var response = new ResponseBase();

            var quesitonarioDuplicado = await _questionarioRepositorio.ObterPorNome(questionario.UsuarioId, questionario.Nome);
            if (quesitonarioDuplicado != null)
            {
                response.AddErro("Questionário com esse nome já cadastrado");
                return response;
            }

            var novoQuestionario = new Questionario { 
                Nome = questionario.Nome,
                UsuarioCriacaoId = questionario.UsuarioId
            };

            foreach (var pergunta in questionario.Perguntas)
            {
                var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionario.UsuarioId, pergunta.PerguntaId);
                if(perguntaExistente == null)
                {
                    response.AddErro($"Pergunta {pergunta.PerguntaId} não encontrada");
                    return response;
                }

                novoQuestionario.AdicionarPergunta(new PerguntaQuestionario
                {
                    OrdemApresentacao = pergunta.OrdemApresentacao,
                    Pergunta = perguntaExistente
                });
            }

            await _questionarioRepositorio.Adicionar(novoQuestionario);

            response.AddData("Questionario adicionado com sucesso!");
            return response;
        }

        public async Task<ResponseBase> ExcluirQuestionario(Guid usuarioCriacaoId, Guid idQuestionario)
        {
            var response = new ResponseBase();

            var questionario = await _questionarioRepositorio.ObterPorIdComAvaliacoesEPerguntas(usuarioCriacaoId, idQuestionario);
            if (questionario == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            if(questionario.Avaliacoes != null && questionario.Avaliacoes.Count > 0)
            {
                response.AddErro("Não é possível excluir o questionário, existem avaliações feitas");
                return response;
            }

            await _questionarioRepositorio.Excluir(questionario);

            response.AddData("Questionario excluído com sucesso!");
            return response;
        }

        public async Task<ResponseBase<ICollection<QuestionarioViewModel>>> ObterQuestionarios(Guid usuarioCriacaoId, Guid questionarioId, string? nome)
        {
            var response = new ResponseBase<ICollection<QuestionarioViewModel>>();

            var questionarios = await _questionarioRepositorio.ObterQuestionarios(usuarioCriacaoId, questionarioId, nome);
            if (questionarios == null)
                return response;

            var questionariosViewModel = questionarios.Select(x => new QuestionarioViewModel()
            {
                Id = x.Id,
                DataCriacao = x.DataCriacao,
                Nome = x.Nome,
                AvaliacoesRespondidas = x.Avaliacoes.Count,
                Perguntas = x.PerguntasQuestionario.Select(y => new PerguntaQuestionarioViewModel(
                                                                                        y.Pergunta.Id,
                                                                                        y.OrdemApresentacao,
                                                                                        y.Pergunta.Descricao))
                                                                                        .OrderBy(z => z.OrdemApresentacao)
                                                                                        .ToList()
            }).ToList();

            response.AddData(questionariosViewModel);

            return response;
        }
    }
}
