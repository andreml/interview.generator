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
    [HttpGet()]
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
    /// Adiciona as respostas da Avaliação do candidato (Candidato)
    /// </summary>
    [HttpGet("obterParaResponder/{avaliacaoId}")]
    [Authorize(Roles = $"{Perfis.Candidato}")]
    [ProducesResponseType(typeof(IEnumerable<ResponderAvaliacaoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterAvaliacaoParaResponderAsync([FromRoute] Guid avaliacaoId)
    {
        try
        {
            var result = await _avaliacaoService.ObterAvaliacaoParaResponderAsync(ObterUsuarioIdLogado(), avaliacaoId);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao responder avaliação");
        }
    }

    /// <summary>
    /// Adiciona as respostas da Avaliação do candidato (Candidato)
    /// </summary>
    [HttpPut("responder")]
    [Authorize(Roles = $"{Perfis.Candidato}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResponderAvaliacaoAsync(ResponderAvaliacaoDto obj)
    {
        try
        {
            obj.CandidatoId = ObterUsuarioIdLogado();
            var result = await _avaliacaoService.ResponderAvaliacao(obj);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao responder avaliação");
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

    /// <summary>
    /// Obtém Avaliações (pendentes e respondidas) do candidato (Candidato)
    /// </summary>
    [HttpGet("candidato")]
    [Authorize(Roles = $"{Perfis.Candidato}")]
    [ProducesResponseType(typeof(IEnumerable<AvaliacaoCandidatoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterAvaliacoesCandidato()
    {
        try
        {
            var result = await _avaliacaoService.ObterAvaliacoesCandidato(ObterUsuarioIdLogado());

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter avaliações do candidato");
        }
    }

    /// <summary>
    /// Envia uma avaliação para um candidato responder (Avaliador)
    /// </summary>
    [HttpPost("enviar")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EnviarAvaliacaoParaCandidatoAsync([FromBody] EnviarAvaliacaoParaCandidatoDto dto)
    {
        try
        {
            dto.UsuarioId = ObterUsuarioIdLogado();

            var result = await _avaliacaoService.EnviarAvaliacaoParaCandidatoAsync(dto);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter avaliações do candidato");
        }
    }
}
