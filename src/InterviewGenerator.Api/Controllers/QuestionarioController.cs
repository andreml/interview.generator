using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de Questionários (Conjunto de perguntas)
/// </summary>
[Route("[controller]")]
[ApiController]
public class QuestionarioController : BaseController
{
    readonly IQuestionarioService _questionarioService;

    public QuestionarioController(IQuestionarioService questionarioService)
    {
        _questionarioService = questionarioService;
    }

    /// <summary>
    /// Adiciona um Questionário (Avaliador)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarQuestionario(AdicionarQuestionarioDto questionario)
    {
        try
        {
            questionario.UsuarioId = ObterUsuarioIdLogado();
            var result = await _questionarioService.CadastrarQuestionario(questionario);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao incluir questionário");
        }
    }


    /// <summary>
    /// Altera um Questionário (Avaliador)
    /// </summary>
    [HttpPut]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterarQuestionario(AlterarQuestionarioDto questionario)
    {
        try
        {
            questionario.UsuarioId = ObterUsuarioIdLogado();
            var result = await _questionarioService.AlterarQuestionario(questionario);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao alterar questionário");
        }
    }

    /// <summary>
    /// Exclui um Questionário (Avaliador)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExcluirQuestionario(Guid id)
    {
        try
        {
            var result = await _questionarioService.ExcluirQuestionario(ObterUsuarioIdLogado(), id);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao excluir questionario");
        }
    }

    /// <summary>
    /// Obém Questionários (Avaliador)
    /// </summary>
    /// <param name="questionarioId">Id do questionário (opcional)</param>
    /// <param name="nome">Nome do questionário (opcional)</param>
    [HttpGet]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(ICollection<QuestionarioViewModelAvaliador>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterQuestionariosPorFiltro([FromQuery] Guid questionarioId, [FromQuery] string? nome)
    {
        try
        {
            var result = await _questionarioService.ObterQuestionarios(ObterUsuarioIdLogado(), questionarioId, nome);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter questionários");
        }
    }

    /// <summary>
    /// Obém Questionário para realizar avaliação (Candidato)
    /// </summary>
    [HttpGet("ObterParaPreenchimento/{id}")]
    [Authorize(Roles = $"{Perfis.Candidato}")]
    [ProducesResponseType(typeof(QuestionarioViewModelCandidato), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterQuestionarioParaPreenchimento([FromRoute] Guid id)
    {
        try
        {
            var result = await _questionarioService.ObterQuestionarioParaPreenchimento(ObterUsuarioIdLogado(), id);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter questionário");
        }
    }

    /// <summary>
    /// Obém estatísticas de um Questionário (Avaliador)
    /// </summary>
    /// <param name="id">Id do Questionário</param>
    [HttpGet("Estatisticas/{id}")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(QuestionarioEstatisticasViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterEstatisticasQuestionario([FromRoute] Guid id)
    {
        try
        {
            var result = await _questionarioService.ObterEstatisticasQuestionario(ObterUsuarioIdLogado(), id);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter estatísticas do questionário");
        }
    }

    /// <summary>
    /// Obém notas de candidatos de um Questionario (Avaliador)
    /// </summary>
    /// <param name="id">Id do Questionário</param>
    [HttpGet("Notas/{id}")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(NotasQuestionariosViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterNotasQuestionario([FromRoute] Guid id)
    {
        try
        {
            var result = await _questionarioService.ObterNotasQuestionario(ObterUsuarioIdLogado(), id);

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter notas do questionário");
        }
    }
}
