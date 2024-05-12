using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using InterviewGenerator.Domain.Repositorio;
using InterviewGenerator.Domain.Utils;

namespace InterviewGenerator.Application.Services;

public class AvaliacaoService : IAvaliacaoService
{
    private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IQuestionarioRepositorio _questionarioRepositorio;

    public AvaliacaoService(
                IAvaliacaoRepositorio avaliacaoRepositorio,
                IUsuarioRepositorio usuarioRepositorio,
                IQuestionarioRepositorio questionarioRepositorio)
    {
        _avaliacaoRepositorio = avaliacaoRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
        _questionarioRepositorio = questionarioRepositorio;
    }

    public async Task<ResponseBase> ResponderAvaliacao(ResponderAvaliacaoDto dto)
    {
        var response = new ResponseBase();

        var avaliacao = await _avaliacaoRepositorio.ObterAvaliacaoPorIdECandidato(dto.AvaliacaoId, dto.CandidatoId);
        if (avaliacao == null)
        {
            response.AddErro("Avaliação não encontrada");
            return response;
        }

        if (avaliacao.Respondida)
        {
            response.AddErro("Candidato já respondeu essa avaliação");
            return response;
        }

        var respostas = new List<RespostaAvaliacao>();

        foreach (var perguntaQuestionario in avaliacao.Questionario.Perguntas)
        {
            var respostaAvaliaco = dto.Respostas.FirstOrDefault(r => r.PerguntaId == perguntaQuestionario.Id);

            if (respostaAvaliaco == null)
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

        await _avaliacaoRepositorio.Alterar(avaliacao);

        response.AddData("Avaliação respondida com sucesso!");

        return response;
    }

    public async Task<ResponseBase> AdicionarObservacaoAvaliacao(AdicionarObservacaoAvaliadorDto obj)
    {
        var response = new ResponseBase();

        var avaliacao = await _avaliacaoRepositorio.ObterAvaliacaoPorIdEUsuarioCriacaoQuestionario(obj.AvaliacaoId, obj.UsuarioIdCriacaoQuestionario);

        if (avaliacao == null)
        {
            response.AddErro("Avaliação não existe");
            return response;
        }

        avaliacao.AdicionarObservacao(obj.ObservacaoAvaliador);

        await _avaliacaoRepositorio.Alterar(avaliacao);

        response.AddData("Observação adicionada com sucesso!");

        return response;
    }

    public async Task<ResponseBase<ICollection<AvaliacaoViewModel>>> ObterAvaliacoesPorFiltro(Guid usuarioIdCriacaoQuestionario, Guid QuestionarioId, string? nomeQuestionario, string? nomeCandidato)
    {
        var response = new ResponseBase<ICollection<AvaliacaoViewModel>>();

        var avaliacoes = await _avaliacaoRepositorio.ObterAvaliacoesPorFiltro(usuarioIdCriacaoQuestionario, QuestionarioId, nomeQuestionario, nomeCandidato);

        if (!avaliacoes.Any())
            return response;

        var avaliacoesViewModel = avaliacoes.Select(a => new AvaliacaoViewModel()
        {
            Id = a.Id,
            Candidato = a.Candidato.Nome,
            NomeQuestionario = a.Questionario.Nome,
            QuestionarioId = a.Questionario.Id,
            DataEnvio = a.DataEnvio,
            DataResposta = a.DataResposta,
            ObservacaoAvaliador = a.ObservacaoAplicador,
            Nota = a.Nota,
            Respostas = a.Respostas?.Select(r => new RespostaAvaliacaoViewModel()
            {
                Pergunta = r.Pergunta.Descricao,
                RespostaEscolhida = r.AlternativaEscolhida.Descricao,
                Correta = r.AlternativaEscolhida.Correta
            }).ToList()
        }).ToList();

        response.AddData(avaliacoesViewModel);

        return response;
    }

    public async Task<ResponseBase<ICollection<AvaliacaoCandidatoViewModel>>> ObterAvaliacoesCandidato(Guid candidatoId)
    {
        var response = new ResponseBase<ICollection<AvaliacaoCandidatoViewModel>>();

        var avaliacoes = await _avaliacaoRepositorio.ObterAvaliacaoPorCandidatoId(candidatoId);

        if (!avaliacoes.Any())
            return response;

        var avaliacoesCandidatoViewModel = avaliacoes.Select(a => new AvaliacaoCandidatoViewModel
        {
            Id = a.Id,
            NomeQuestionario = a.Questionario.Nome,
            DataEnvio = a.DataEnvio,
            DataResposta = a.DataResposta,
            Nota = a.Nota
        }).ToList();

        response.AddData(avaliacoesCandidatoViewModel);

        return response;
    }

    public async Task<ResponseBase> EnviarAvaliacaoParaCandidatoAsync(EnviarAvaliacaoParaCandidatoDto dto)
    {
        var response = new ResponseBase();

        var questionario = await _questionarioRepositorio.ObterPorIdEUsuarioCriacao(dto.UsuarioId, dto.QuestionarioId);
        if (questionario == null)
        {
            response.AddErro("Questionário não encontrado");
            return response;
        }

        var usuarioCandidato = await _usuarioRepositorio.ObterPorLogin(dto.LoginCandidato);
        if (usuarioCandidato == null)
        {
            response.AddErro("Candidato não encontrado");
            return response;
        }
        if (usuarioCandidato.Perfil == Perfil.Avaliador)
        {
            response.AddErro("Login não pertence a um usuário Candidato");
            return response;
        }

        var avaliacoes = await _avaliacaoRepositorio.ObterAvaliacaoPorCandidatoId(usuarioCandidato.Id);
        if (avaliacoes != null && avaliacoes.Any(a => a.Questionario.Id == dto.QuestionarioId))
        {
            response.AddErro("Avaliação já enviada a esse candidato");
            return response;
        }

        var avaliacao = new Avaliacao
        {
            Questionario = questionario,
            Candidato = usuarioCandidato,
            DataEnvio = DateTime.Now,
            Respondida = false,
            ObservacaoAplicador = string.Empty
        };

        await _avaliacaoRepositorio.Adicionar(avaliacao);

        response.AddData(avaliacao.Id);

        return response;
    }

    public async Task<ResponseBase<ResponderAvaliacaoViewModel>> ObterAvaliacaoParaResponderAsync(Guid candidatoId, Guid avaliacaoId)
    {
        var response = new ResponseBase<ResponderAvaliacaoViewModel>();

        var avaliacao = await _avaliacaoRepositorio.ObterAvaliacaoPorIdECandidato(avaliacaoId, candidatoId);

        if (avaliacao == null)
            return response;

        if(avaliacao.Respondida)
        {
            response.AddErro("Candidato já respondeu esta avaliação");
            return response;
        }

        var avaliacaoViewModel = new ResponderAvaliacaoViewModel()
        {
            AvaliacaoId = avaliacao.Id,
            NomeQuestionario = avaliacao.Questionario.Nome,
            Perguntas = avaliacao.Questionario.Perguntas.Select(p => new PerguntaQuestionarioAvaliacao()
            {
                Id = p.Id,
                Descricao = p.Descricao,
                Alternativas = p.Alternativas.Select(a => new AlternativaPerguntaQuestionarioAvaliacao()
                {
                    Id = a.Id,
                    Descricao = a.Descricao
                }).ToList().Randomizar()
            }).ToList().Randomizar()
        };

        return response;
    }
}
