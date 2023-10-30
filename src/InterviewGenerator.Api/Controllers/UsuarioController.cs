using InterviewGenerator.Application.Dto;
using InterviewGenerator.Application.Interfaces;
using InterviewGenerator.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewGenerator.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento do Usuário
    /// </summary>
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
        /// Obtém Usuário (Avaliador | Candidato)
        /// </summary>
        [Authorize]
        [HttpGet("Obter")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterPorId()
        {
            try
            {
                var result = await _usuarioService.ObterUsuario(ObterUsuarioIdLogado());

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao realizar a consulta");
            }
        }

        /// <summary>
        /// Adiciona Usuário (Avaliador | Candidato)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioDto usuario)
        {
            try
            {
                var result = await _usuarioService.CadastrarUsuario(usuario);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao adicionar usuário");
            }
        }

        /// <summary>
        /// Altera Usuário (Avaliador | Candidato)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Alterar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarUsuario(AlterarUsuarioDto usuario)
        {
            try
            {
                usuario.Id = ObterUsuarioIdLogado();
                var result = await _usuarioService.AlterarUsuario(usuario);

                return Response(result!);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir o usuário");
            }
        }
    }
}
