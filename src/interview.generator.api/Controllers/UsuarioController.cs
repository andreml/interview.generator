using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interview.generator.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UsuarioController : BaseController
    {
        readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtém usuário (Avaliador | Candidato)
        /// </summary>
        [Authorize]
        [HttpGet("ObterUsuario")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterPorId()
        {
            try
            {
                var result = await _usuarioService.ObterUsuario(ObterUsuarioIdLogado());
                if (result != null && result.HasError) return ResponseErro(result.StatusCode, result.GetErrors());
                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao realizar a consulta");
            }
        }

        /// <summary>
        /// Adicionar um novo usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost("AdicionarUsuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioDto usuario)
        {
            try
            {
                var result = await _usuarioService.CadastrarUsuario(usuario);
                if (result != null && result.HasError) return ResponseErro(result.StatusCode, result.GetErrors());
                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar usuário");
            }
        }

        /// <summary>
        /// Altera um usuário existente
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("AlterarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarUsuario(AlterarUsuarioDto usuario)
        {
            try
            {
                usuario.Id = ObterUsuarioIdLogado();
                var result = await _usuarioService.AlterarUsuario(usuario);
                if (result != null && result.HasError) return ResponseErro(result.StatusCode, result.GetErrors());
                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir o usuário");
            }
        }
    }
}
