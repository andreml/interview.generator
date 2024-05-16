using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Repositorio;

namespace InterviewGenerator.Application.Services;

public class DashService : IDashService
{
    private readonly IPerguntaRepositorio _perguntaRepositorio;
    private readonly IQuestionarioRepositorio _questionarioRepositorio;
    private readonly IAreaConhecimentoRepositorio _areaConhecimentoRepositorio;

    public DashService(
                IPerguntaRepositorio perguntaRepositorio, 
                IQuestionarioRepositorio questionarioRepositorio, 
                IAreaConhecimentoRepositorio areaConhecimentoRepositorio)
    {
        _perguntaRepositorio = perguntaRepositorio;
        _questionarioRepositorio = questionarioRepositorio;
        _areaConhecimentoRepositorio = areaConhecimentoRepositorio;
    }

    public async Task<ResponseBase<DashViewModel>> ObterDadosDashAsync(Guid usuarioAvaliadorId)
    {
        var response = new ResponseBase<DashViewModel>();

        var questionarios = await _questionarioRepositorio.ObterQuestionarios(usuarioAvaliadorId, Guid.Empty, string.Empty);

        var dashViewModel = new DashViewModel
        {
            Perguntas = await _perguntaRepositorio.ObterCountAsync(usuarioAvaliadorId),
            AreasConhecimento = await _areaConhecimentoRepositorio.ObterCountAsync(usuarioAvaliadorId),
            Questionarios = questionarios.Count,
            AvaliacoesRespondidas = questionarios.Sum(q => q.Avaliacoes.Count(a => a.Respondida)),
            AvaliacoesPendentes = questionarios.Sum(q => q.Avaliacoes.Count(a => !a.Respondida))
        };

        response.AddData(dashViewModel);
        return response;
    }
}
