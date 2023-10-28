using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Repositorio;

namespace interview.generator.application.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepositorio _repositorio;
        private readonly IQuestionarioRepositorio _questionarioRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AvaliacaoService(IAvaliacaoRepositorio repositorio, IQuestionarioRepositorio questionarioRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _repositorio = repositorio;
            _questionarioRepositorio = questionarioRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ResponseBase> AdicionarAvaliacao(AdicionarAvaliacaoDto entity)
        {
            var response = new ResponseBase();

            var questionario = await _questionarioRepositorio.ObterPorId(entity.QuestionarioId);
            if (questionario == null)
            {
                response.AddErro("Questionário não encontrado");
                return response;
            }

            if (questionario.Avaliacoes.Select(a => a.Candidato.Id == entity.CandidatoId).Any())
            {
                response.AddErro("Candidato já respondeu este questionário");
                return response;
            }

            var candidato = await _usuarioRepositorio.ObterPorId(entity.CandidatoId);

            var avaliacao = new Avaliacao()
            {
                Candidato = candidato!,
                Questionario = questionario,
                ObservacaoAplicador = string.Empty
            };

            var respostas = new List<RespostaAvaliacao>();

            foreach(var perguntaQuestionario in questionario.Perguntas)
            {
                var respostaAvaliaco = entity.Respostas.FirstOrDefault(r => r.PerguntaId == perguntaQuestionario.Id);

                if(respostaAvaliaco == null)
                {
                    response.AddErro("Uma ou mais perguntas não foram respondidas");
                    return response;
                }

                var alternativaEscolhida = perguntaQuestionario.Alternativas.FirstOrDefault(a => a.Id == respostaAvaliaco.AlternativaId);

                if (alternativaEscolhida == null)
                {
                    response.AddErro("Uma ou mais perguntas estão com respostas inválidas");
                    return response;
                }

                respostas.Add(new RespostaAvaliacao(perguntaQuestionario, alternativaEscolhida));
            }

            avaliacao.Respostas = respostas;

            avaliacao.CalcularNota();

            await _repositorio.Adicionar(avaliacao);

            response.AddData("Avaliação adicionada com sucesso!");

            return response;
        }

        public async Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj)
        {
            var response = new ResponseBase();

            var avaliacao = await _repositorio.ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(obj.AvaliacaoId, obj.UsuarioIdCriacaoQuestionario);

            if (avaliacao == null)
            {
                response.AddErro("Avaliação não existe");
                return response;
            }

            avaliacao.AdicionarObservacao(obj.ObservacaoAvaliador);

            await _repositorio.Alterar(avaliacao);

            response.AddData("Observação adicionada com sucesso!");

            return response;
        }

        public async Task<ResponseBase<ICollection<AvaliacaoViewModel>>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato)
        {
            var response = new ResponseBase<ICollection<AvaliacaoViewModel>>();

            var avaliacoes = await _repositorio.ObterAvaliacoesPorFiltro(usuarioIdCriacaoQuestionario, QuestionarioId, nomeQuestionario, nomeCandidato);

            if (avaliacoes.Count == 0)
                return response;

            var avaliacoesViewModel = avaliacoes.Select(a => new AvaliacaoViewModel()
            {
                Id = a.Id,
                Candidato = a.Candidato.Nome,
                NomeQuestionario = a.Questionario.Nome,
                QuestionarioId = a.Questionario.Id,
                DataAplicacao = a.DataAplicacao,
                ObservacaoAvaliador = a.ObservacaoAplicador,
                Nota = a.Nota,
                Respostas = a.Respostas.Select(r => new RespostaAvaliacaoViewModel()
                {
                    Pergunta = r.Pergunta.Descricao,
                    RespostaEscolhida = r.AlternativaEscolhida.Descricao,
                    Correta = r.AlternativaEscolhida.Correta
                }).ToList()
            }).ToList();

            response.AddData(avaliacoesViewModel);

            return response;
        }
    }
}
