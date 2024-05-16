using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using InterviewGenerator.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers;

/// <summary>
/// Controller responsável pela exibição de dados gerais para o dashboard
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DashController : BaseController
{
    private readonly IDashService _dashService;

    public DashController(IDashService dashService)
    {
        _dashService = dashService;
    }

    /// <summary>
    /// Obtém dados gerais (Avaliador)
    /// </summary>
    [HttpGet]
    [Authorize(Roles = $"{Perfis.Avaliador}")]
    [ProducesResponseType(typeof(DashViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterDadosDashAsync()
    {
        try
        {
            var result = await _dashService.ObterDadosDashAsync(ObterUsuarioIdLogado());

            return Response(result);
        }
        catch (Exception e)
        {
            return ResponseErro(e.Message, "Erro ao obter dados do dash");
        }
    }
}
