using interview.generator.application.DTO;
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
    public class UsuarioController : ControllerBase
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
                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<IEnumerable<Usuario>>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Consulta realizado com sucesso",
                    Data = await _usuarioService.ListarUsuarios()
                });
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
                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<Usuario>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Consulta realizado com sucesso",
                    Data = await _usuarioService.ObterUsuario(id)
                });
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
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioDTO usuario)
        {
            try
            {
                var validation = new AdicionarUsuarioDTOValidator().Validate(usuario);
                if (!validation.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ResponseErro()
                    {
                        Codigo = (int)HttpStatusCode.BadRequest,
                        Mensagem = string.Join("; ", validation.Errors.Select(x => x.ErrorMessage))
                    });
                }

                await _usuarioService.CadastrarUsuario(usuario);

                return StatusCode((int)HttpStatusCode.Created, new ResponseSucesso<AdicionarUsuarioDTO>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Usuário adicionado com sucesso!"
                });
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
                await _usuarioService.AlterarUsuario(usuario);

                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<Usuario>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Consulta realizado com sucesso"
                });
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
