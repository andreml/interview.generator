using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
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
        /// Adiciona uma Area de Conhecimento
        /// </summary>
        [HttpPost("AdicionarAreaConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Obtém Areas de Conhecimento
        /// </summary>
        /// <param name="areaConhecimentoId">Id da Area de Conhecimento (opcional)</param>
        /// <param name="descricao">Descrição da Area de Conhecimento (opcional)</param>
        [HttpGet("ObterAreasConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(typeof(IEnumerable<AreaConhecimentoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErro), StatusCodes.Status400BadRequest)]
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
        /// Altera uma Area de Conhecimento
        /// </summary>
        [HttpPut("AlterarAreaConhecimento")]
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
        /// Exclui uma area de conhecimento
        /// </summary>
        /// <param name="id">Id da Area de Conhecimento</param>
        [HttpDelete("ExcluirAreaConhecimento/{id}")]
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
}
