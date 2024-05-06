using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Entidade.Common;
using InterviewGenerator.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento das Areas de Conhecimento (Categoria de perguntas)
/// </summary>
[Route("[controller]")]
[ApiController]
public class AreaConhecimentoController : BaseController
{
    readonly IAreaConhecimentoService _areaConhecimentoService;

    public AreaConhecimentoController(IAreaConhecimentoService areaConhecimentoService)
    {
        _areaConhecimentoService = areaConhecimentoService;
    }

    /// <summary>
    /// Adiciona uma Area de Conhecimento (Avaliador)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarAreaConhecimento(AdicionarAreaConhecimentoDto areaConhecimento)
    {
        try
        {
            areaConhecimento.UsuarioId = ObterUsuarioIdLogado();

            var result = await _areaConhecimentoService.CadastrarAreaConhecimento(areaConhecimento);

            return Response(result!);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
        }
    }

    /// <summary>
    /// Obtém Areas de Conhecimento (Avaliador)
    /// </summary>
    /// <param name="areaConhecimentoId">Id da Area de Conhecimento (opcional)</param>
    /// <param name="descricao">Descrição da Area de Conhecimento (opcional)</param>
    [HttpGet]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(IEnumerable<AreaConhecimentoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterAreasConhecimentoAsync([FromQuery] Guid areaConhecimentoId, [FromQuery] string? descricao)
    {
        try
        {
            var result = await _areaConhecimentoService.ListarAreasConhecimento(ObterUsuarioIdLogado(), areaConhecimentoId, descricao);

            return Response(result!);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter áreas de conhecimento");
        }
    }

    /// <summary>
    /// Altera uma Area de Conhecimento (Avaliador)
    /// </summary>
    [HttpPut]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento)
    {
        try
        {
            areaConhecimento.UsuarioId = ObterUsuarioIdLogado();

            var result = await _areaConhecimentoService.AlterarAreaConhecimento(areaConhecimento);

            return Response(result!);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao alterar a área de conhecimento");
        }
    }

    /// <summary>
    /// Exclui uma area de conhecimento (Avaliador)
    /// </summary>
    /// <param name="id">Id da Area de Conhecimento</param>
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExcluirAreaConhecimento(Guid id)
    {
        try
        {
            var result = await _areaConhecimentoService.ExcluirAreaConhecimento(ObterUsuarioIdLogado(), id);

            return Response(result!);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao excluir a área de conhecimento");
        }
    }

}
