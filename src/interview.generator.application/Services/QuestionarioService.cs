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
            if (questionario == null)
            {
                response.AddErro("Questionario não encontrado");
                return response;
            }

            if (questionario.Avaliacoes != null && questionario.Avaliacoes.Count > 0)
            {
                response.AddErro("Não é possível alterar o questionário, existem avaliações feitas");
                return response;
            }

            if (questionarioDto.Nome != questionario.Nome)
            {
                var questionarioPorNome = await _questionarioRepositorio.ObterPorNome(questionarioDto.UsuarioId, questionarioDto.Nome);
                if (questionarioPorNome != null && questionarioPorNome.Id != questionarioDto.QuestionarioId)
                {
                    response.AddErro("Já existe um questionário com este nome");
                    return response;
                }
            }

            questionario.RemoverPerguntas();
            questionario.Nome = questionarioDto.Nome;

            foreach (var pergunta in questionarioDto.Perguntas)
            {
                var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionarioDto.UsuarioId, pergunta);
                if (perguntaExistente == null)
                {
                    response.AddErro($"Pergunta {pergunta} não encontrada");
                    return response;
                }

                questionario.AdicionarPergunta(perguntaExistente);
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

            var novoQuestionario = new Questionario
            {
                Nome = questionario.Nome,
                UsuarioCriacaoId = questionario.UsuarioId
            };

            foreach (var pergunta in questionario.Perguntas)
            {
                var perguntaExistente = await _perguntaRepositorio.ObterPerguntaPorId(questionario.UsuarioId, pergunta);
                if (perguntaExistente == null)
                {
                    response.AddErro($"Pergunta {pergunta} não encontrada");
                    return response;
                }

                novoQuestionario.AdicionarPergunta(perguntaExistente);
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

            if (questionario.Avaliacoes != null && questionario.Avaliacoes.Count > 0)
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
                Perguntas = x.Perguntas.Select(y => new PerguntaQuestionarioViewModel(
                                                                                        y.Id,
                                                                                        1,
                                                                                        y.Descricao))
                                                                                        .OrderBy(z => z.OrdemApresentacao)
                                                                                        .ToList()
            }).ToList();

            response.AddData(questionariosViewModel);

            return response;
        }

        public async Task<ResponseBase<QuestionarioEstatisticasViewModel>> ObterEstatisticasQuestionario(Guid usuarioCriacaoId, Guid questionarioId)
        {
            var response = new ResponseBase<QuestionarioEstatisticasViewModel>();

            var questionario = await _questionarioRepositorio.ObterPorIdComAvaliacoesEPerguntas(usuarioCriacaoId, questionarioId);
            if (questionario == null)
                return response;

            var estatisticas = new QuestionarioEstatisticasViewModel()
            {
                Id = questionario.Id,
                Nome = questionario.Nome,
                AvaliacoesRespondidas = questionario.Avaliacoes.Count
            };

            if(estatisticas.AvaliacoesRespondidas > 0)
            {
                estatisticas.MediaNota = questionario.Avaliacoes.Select(a => a.Nota).Average();

                var maiorNota = questionario.Avaliacoes.Select(a => a.Nota).Max();

                estatisticas.MaiorNota = new MaiorNotaViewModel()
                {
                    Nota = maiorNota,
                    Candidatos = questionario.Avaliacoes
                                                .Where(a => a.Nota == maiorNota)
                                                .Select(a => a.Candidato.Nome)
                                                .ToList()
                };
            }

            response.AddData(estatisticas);
            return response;
        }
    }
}
