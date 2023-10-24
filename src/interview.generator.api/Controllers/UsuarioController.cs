using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.application.ViewModels;
using interview.generator.domain.Enum;
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
        /// Obtém todos os usuários (Necessário estar autenticado com usuário Avaliador)
        /// </summary>
        [HttpGet("ObterTodos")]
        [Authorize(Roles = $"{Perfis.Avaliador}")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterUsuariosAsync()
        {
            try
            {
                var result = await _usuarioService.ListarUsuarios();

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao obter os usuários");
            }
        }

        /// <summary>
        /// Obtém usuário por Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpGet("ObterPorId/{id}")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var result = await _usuarioService.ObterUsuario(id);

                return Response(result);
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

                return Response(result);
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
                if (ObterUsuarioIdLogado() != usuario.Id)
                    return Unauthorized();

                var result = await _usuarioService.AlterarUsuario(usuario);

                return Response(result);
            }
            catch (Exception e)
            {
                return ResponseErro(e.Message, "Erro ao excluir o usuário");
            }
        }
    }
}
