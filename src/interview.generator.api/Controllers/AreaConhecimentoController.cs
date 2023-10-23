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
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _areaConhecimentoService.CadastrarAreaConhecimento(areaConhecimento, usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao incluir area do conhecimento");
            }
        }

        [HttpGet("ObterTodos")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> ObterAreasConhecimentoAsync()
        {
            try
            {
                var usuarioId = ObterUsuarioIdLogado();

                var result = await _areaConhecimentoService.ListarAreasConhecimento(usuarioId);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter áreas de conhecimento");
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var result = await _areaConhecimentoService.ObterAreaConhecimento(id);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao realizar a consulta");
            }
        }

        [Authorize]
        [HttpPut("AlterarAreaConhecimento")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        public async Task<IActionResult> AlterarAreaConhecimento(AlterarAreaConhecimentoDto areaConhecimento)
        {
            try
            {
                var usuarioLogado = ObterUsuarioIdLogado();

                var result = await _areaConhecimentoService.AlterarAreaConhecimento(areaConhecimento, usuarioLogado);

                return Response(result);
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
                var result = await _areaConhecimentoService.ExcluirAreaConhecimento(id);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir a área de conhecimento");
            }
        }

    }
}
