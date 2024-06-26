﻿using InterviewGenerator.Application.Dto;
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
}
