using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de Avaliações (Respostas dos questionários)
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AvaliacaoController : BaseController
{
    readonly IAvaliacaoService _avaliacaoService;
    public AvaliacaoController(IAvaliacaoService service) { _avaliacaoService = service; }

    /// <summary>
    /// Obtém Avaliações feitas pelos Candidatos (Avaliador)
    /// </summary>
    /// <param name="QuestionarioId">Id do questionário (opcional)</param>
    /// <param name="nomeQuestionario">Nome do questionário (opcional)</param>
    /// <param name="nomeCandidato">Nome do candidato (opcional)</param>
    [HttpGet]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(IEnumerable<AvaliacaoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterAvaliacoesPorFiltroAsync([FromQuery] Guid QuestionarioId, [FromQuery] string? nomeQuestionario, [FromQuery] string? nomeCandidato)
    {
        try
        {
            var result = await _avaliacaoService.ObterAvaliacoesPorFiltro(ObterUsuarioIdLogado(), QuestionarioId, nomeQuestionario, nomeCandidato);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter avaliações");
        }
    }

    /// <summary>
    /// Adiciona a Avaliação do candidato (Candidato)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = $"{Perfis.Candidato}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarAvaliacaoAsync(AdicionarAvaliacaoDto obj)
    {
        try
        {
            obj.CandidatoId = ObterUsuarioIdLogado();
            var result = await _avaliacaoService.AdicionarAvaliacao(obj);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao adicionar avaliação");
        }
    }

    /// <summary>
    /// Adiciona uma observação na Avaliação do candidato (Avaliador)
    /// </summary>
    [HttpPut("AdicionarObservacao")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarObservacaoAvaliacaoAsync(AdicionarObservacaoAvaliadorDto obj)
    {
        try
        {
            obj.UsuarioIdCriacaoQuestionario = ObterUsuarioIdLogado();
            var result = await _avaliacaoService.AdicionarObservacaoAvaliacao(obj);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao adicionar observação");
        }
    }
}
