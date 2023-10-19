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
    public class PerfilController : ControllerBase
    {
        readonly IPerfilService _perfilService;
        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterPerfilsAsync()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<IEnumerable<Perfil>>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Consulta realizado com sucesso",
                    Data = await _perfilService.ListarPerfils()
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
                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<Perfil>()
                {
                    Codigo = (int)HttpStatusCode.OK,
                    Mensagem = "Consulta realizado com sucesso",
                    Data = await _perfilService.ObterPerfil(id)
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

        [HttpPost("AdicionarPerfil")]
        public async Task<IActionResult> AdicionarPerfil(Perfil Perfil)
        {
            try
            {
                await _perfilService.CadastrarPerfil(Perfil);

                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<Perfil>()
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

        [HttpPut("AlterarPerfil")]
        public async Task<IActionResult> AlterarPerfil(Perfil Perfil)
        {
            try
            {
                await _perfilService.AlterarPerfil(Perfil);

                return StatusCode((int)HttpStatusCode.OK, new ResponseSucesso<Perfil>()
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
