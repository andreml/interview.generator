using interview.generator.application.Dto;
using interview.generator.application.Interfaces;
using interview.generator.domain.Entidade;
using interview.generator.domain.Entidade.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterUsuariosAsync()
        {
            try
            {
                var result = await _usuarioService.ListarUsuarios();

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = ""
                });
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var result = await _usuarioService.ObterUsuario(id);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = ""
                });
            }
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario(AddUsuarioDto usuario)
        {
            try
            {
                var result = await _usuarioService.CadastrarUsuario(usuario);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = ""
                });
            }
        }

        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario(Usuario usuario)
        {
            try
            {
                var result = await _usuarioService.AlterarUsuario(usuario);

                return Response(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                {
                    Codigo = (int)HttpStatusCode.BadRequest,
                    Mensagem = e.Message,
                    Excecao = ""
                });
            }
        }
    }
}
