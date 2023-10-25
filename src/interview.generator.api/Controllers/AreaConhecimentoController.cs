using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AreaConhecimentoController : BaseController
    {
        readonly IAreaConhecimentoService _areaConhecimentoService;

        public AreaConhecimentoController(IAreaConhecimentoService areaConhecimentoService)
        {
            _areaConhecimentoService = areaConhecimentoService;
        }

        [HttpPost("AdicionarAreaConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
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
        /// Obtém areas de conhecimento cadastradas
        /// </summary>
        /// <param name="areaConhecimentoId">Id da Area de Conhecimento (opcional)</param>
        /// <param name="descricao">Descrição da Area de Conhecimento (opcional)</param>
        /// <returns></returns>
        [HttpGet("ObterAreasConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterAreasConhecimentoAsync([FromQuery] Guid areaConhecimentoId, [FromQuery] string? descricao)
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _areaConhecimentoService.ListarAreasConhecimento(usuarioId, areaConhecimentoId, descricao);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter áreas de conhecimento");
            }
        }  

        [Authorize]
        [HttpPut("AlterarAreaConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
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

        [Authorize]
        [HttpDelete("ExcluirAreaConhecimento/{id}")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ExcluirAreaConhecimento(Guid id)
        {
            try
            {
                var result = await _areaConhecimentoService.ExcluirAreaConhecimento(id, ObterUsuarioIdLogado());

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir a área de conhecimento");
            }
        }

    }
}
